using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MIM.Config;
using MIM.Models;
using FluentValidation.Results;
using PagedList;
using PagedList.Mvc;

namespace MIM.Controllers
{
    public class UsersController : Controller
    {
        private MIMDBContext db = new MIMDBContext();
        DBHelper dbh = new DBHelper();

        public static ICollection<Group> Groups;
        // GET: /Users        
        public async Task<ActionResult> Index()
        {
            var users = db.Users.Include(u => u.Organization).Include(u => u.Title).Include(d=>d.Department).Where(x => x.OrganizationID == Organization.current.OrganizationID);
            return View(await users.ToListAsync());
        }

        public IPagedList<User> GetUserList(User user, int? page)
        {
            var _page = page ?? 1;
            var users = db.Users.Include(u => u.Organization).Where(x => x.OrganizationID == Organization.current.OrganizationID);
            IPagedList<User> filtering_user = users.ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            if (user.UserID > 0) filtering_user = users.Where(x => x.UserID == user.UserID).ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            if (user.Firstname !=null) filtering_user = users.Where(x => x.Firstname.Contains(user.Firstname)).ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            if (user.Lastname != null) filtering_user = users.Where(x => x.Lastname.Contains(user.Lastname)).ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            if (user.TitleID != null) filtering_user = users.Where(x => x.TitleID == user.TitleID).ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            if (user.DepartmentID != null) filtering_user = users.Where(x => x.DepartmentID == user.DepartmentID).ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            return filtering_user;
        }

        // GET: /Users/Table
        public ActionResult Table(User user, int? page)
        {
            if (!MIM.Models.User.current.isGranted("Table", "Users")) return View();
            var users = GetUserList(user, page);
            ViewBag.TitleID = new SelectList(db.Titles.Where(x => x.OrganizationID == Organization.current.OrganizationID), "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments.Where(x => x.OrganizationID == Organization.current.OrganizationID), "DepartmentID", "Name");
            return View(users);
        }

        // GET: /Users/Show/5
        public async Task<ActionResult> Show(int? id)
        {
            if (!MIM.Models.User.current.isGranted("Show", "Users")) return View();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: /Users/Create
        public ActionResult Create()
        {
            if (!MIM.Models.User.current.isGranted("Create", "Users")) return View("");
            ViewBag.Groups = GetSelectedGroups(new Group[0]);
            ViewBag.titleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,TitleID,Firstname,Lastname,Nickname,Username,Password,Email,IsActive,BornDate,SuperAdmin,AvatarUrl,DepartmentID,Groups")] User user,int[] GroupIDS,HttpPostedFileBase fb)
        {   
            if (GroupIDS != null)            
                foreach (var item in GroupIDS)                
                    user.Groups.Add(db.Groups.FirstOrDefault(x => x.GroupID == item)); 

            user.OrganizationID = Organization.current.OrganizationID;
            user.AvatarUrl = dbh.AddImage(fb, "AvatarImages/", true);
            if (ModelState.IsValid)
            {                
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = GetSelectedGroups(new Group[0]);
            ViewBag.TitleID = new SelectList(db.Titles.Where(x => x.OrganizationID == Organization.current.OrganizationID), "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments.Where(x => x.OrganizationID == Organization.current.OrganizationID), "DepartmentID", "Name");
            return View(user);
        }
        public List<SelectListItem> GetSelectedGroups(Group[] groups)
        {
            List <SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable <Group> allGroups = db.Groups.Where(x => x.OrganizationID == Organization.current.OrganizationID);
            foreach (Group grp in allGroups)
            {
                selectListItems.Add(new SelectListItem()
                { 
                    Text = grp.Name.ToString(),
                    Value = grp.GroupID.ToString(),
                    Selected = groups.Any(x => x.GroupID == grp.GroupID)
                });
            }
            return selectListItems;
        }
        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (!MIM.Models.User.current.isGranted("Edit", "Users")) return View();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }           
            User user = await db.Users.FindAsync(id);
            ViewBag.Groups = GetSelectedGroups(user.Groups.ToArray());
            Groups = user.Groups;
            if (user == null)
            {
                return HttpNotFound();
            }
            ViewBag.fb = user.AvatarUrl;
            ViewBag.TitleID = new SelectList(db.Titles.Where(x => x.OrganizationID == Organization.current.OrganizationID), "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments.Where(x => x.OrganizationID == Organization.current.OrganizationID), "DepartmentID", "Name");
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,TitleID,Firstname,Lastname,Nickname,Username,Password,Email,IsActive,BornDate,SuperAdmin,DepartmentID,Groups")] User user, int[] GroupIDS)
        {
            if (!MIM.Models.User.current.isGranted("Edit", "Users")) return View();

            user.OrganizationID = Organization.current.OrganizationID;
            dbh.UpdateGroups(user.UserID, GroupIDS);

            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Groups = GetSelectedGroups(user.Groups.ToArray());
            ViewBag.TitleID = new SelectList(db.Titles.Where(x => x.OrganizationID == Organization.current.OrganizationID), "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments.Where(x => x.OrganizationID == Organization.current.OrganizationID), "DepartmentID", "Name");
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (!MIM.Models.User.current.isGranted("Delete", "Users")) return View();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            User user = await db.Users.FindAsync(id);

            foreach (var group in user.Groups.ToList())
            {
                user.Groups.Remove(group);
            }
            dbh.DeleteImage(user.AvatarUrl);
            db.Users.Remove(user);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
