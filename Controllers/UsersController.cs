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
        public static ICollection<Group> Groups;
        // GET: /Users        
        public async Task<ActionResult> Index()
        {
            var users = db.Users.Include(u => u.Organization).Include(u => u.Title).Include(d=>d.Department).Where(x => x.OrganizationID == Organization.current.OrganizationID);
            return View(await users.ToListAsync());
        }

        // GET: /Users/Table
        public ActionResult Table(int? page)
        {
            if (!MIM.Models.User.current.isGranted("Table", "Users")) return View();
            var _page = page ?? 1;
            var users = db.Users.Include(u => u.Organization).Where(x => x.OrganizationID == Organization.current.OrganizationID).ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
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
            ViewBag.Groups = new SelectList(user.Groups, "GroupID", "Name");
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
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            ViewBag.Groups = new SelectList(db.Groups, "GroupID", "Name");
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,TitleID,Firstname,Lastname,Nickname,Username,Password,Email,IsActive,BornDate,SuperAdmin,DepartmentID,Groups")] User user,int[] GroupIDS)
        {   
            if (GroupIDS != null)
            {
                foreach (var item in GroupIDS)
                {
                    user.Groups.Add(db.Groups.FirstOrDefault(x => x.GroupID == item));
                }
            }
            
            user.OrganizationID = Organization.current.OrganizationID;
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = GetSelectedGroups(new Group[0]);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
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
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View(user);
        }

        // POST: Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UserID,TitleID,Firstname,Lastname,Nickname,Username,Password,Email,IsActive,BornDate,SuperAdmin,DepartmentID,Groups")] User user, int[] GroupIDS)
        {
            if (!MIM.Models.User.current.isGranted("Edit", "Users")) return View();
            if (GroupIDS != null)
            {
                foreach (var item in GroupIDS)
                {
                    var grp = db.Groups.FirstOrDefault(x => x.GroupID == item);
                    user.Groups.Add(grp);
                }
            }
            user.OrganizationID = Organization.current.OrganizationID;
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = GetSelectedGroups(user.Groups.ToArray());
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
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
