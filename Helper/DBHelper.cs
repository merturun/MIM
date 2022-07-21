using MIM.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace MIM.Models
{
    public class DBHelper
    {
        MIMDBContext db = new MIMDBContext();

        public DBHelper()
        {
            db = new MIMDBContext();
        }
            
        public bool UpdateGroups(int user_id,int[] group_ids)
        {
            if (user_id == 0 || group_ids == null) return false;
            User user = db.Users.FirstOrDefault(u => u.UserID == user_id);
            List<Group> lastgroups = (from x in db.Groups where group_ids.Contains(x.GroupID) select x).ToList();
            user.Groups.Clear(); // Mevcut grupları sil
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges(); // Modeli kaydet
            foreach (Group grp in lastgroups)            
                user.Groups.Add(grp); // Yeni grupları ekle
            db.Entry(user).State = EntityState.Modified;
            db.SaveChanges(); // Modeli kaydet
            return true;
        }

        public bool UpdateGrants(int group_id, int[] grant_ids)
        {
            if (group_id == 0 || grant_ids == null) return false;
            Group group = db.Groups.FirstOrDefault(g => g.GroupID == group_id);
            List<Grant> lastgrants = (from x in db.Grants where grant_ids.Contains(x.GrantID) select x).ToList();
            group.Grants.Clear();
            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();
            foreach (Grant grt in lastgrants)
                group.Grants.Add(grt);
            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();
            return true;
        }

        public string AddImage(HttpPostedFileBase fileBase,string imgPath,bool avatar = false)
        {
            string AvatarUrl = "";
            if (fileBase != null)
            {
                var image = Guid.NewGuid() + Path.GetExtension(fileBase.FileName);
                var path = Path.Combine(HttpContext.Current.Server.MapPath("/assets/media/" + imgPath), image);
                AvatarUrl = image;
                if (avatar)
                {
                    Bitmap bm = ResizeImage(Image.FromStream(fileBase.InputStream), 100, 100);
                    bm.Save(path);
                    return AvatarUrl;
                }
                fileBase.SaveAs(path);                
            }
            return AvatarUrl;
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {           
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
    }
}