using MIM.Config;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    }
}