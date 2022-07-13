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
        // GET: /Users        
        public async Task<ActionResult> Index()
        {            
            var users = db.Users.Include(u => u.Organization).Include(u => u.Title).Include(d=>d.Department).Where(x => x.OrganizationID == Organization.current.OrganizationID);
            return View(await users.ToListAsync());
        }

        // GET: /Users/Table
        public ActionResult Table(int? page)
        {
            var _page = page ?? 1;
            var users = db.Users.Include(u => u.Organization).Include(u => u.Title).Include(d => d.Department).Where(x => x.OrganizationID == Organization.current.OrganizationID).ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View(users);
        }

        // GET: /Users/Show/5
        public async Task<ActionResult> Show(int? id)
        {
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
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UserID,TitleID,Firstname,Lastname,Nickname,Username,Password,Email,IsActive,BornDate,SuperAdmin,DepartmentID")] User user)
        {
            //UserValidator uValidator = new UserValidator();
            //ValidationResult results = uValidator.Validate(user);
            //if (results.IsValid)
            //{
            //    db.Entry(user).State = EntityState.Modified;
            //    await db.SaveChangesAsync();
            //    return RedirectToAction("List");
            //}
            //else
            //{
            //    foreach (var item in results.Errors)
            //    {
            //        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
            //    }
            //}

            //ViewBag.organizationID = new SelectList(db.Organizations, "organizationID", "name", user.organizationID);
            //ViewBag.titleID = new SelectList(db.Titles, "titleID", "name", user.titleID);
            //return View(user);
            user.OrganizationID = Organization.current.OrganizationID;
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = await db.Users.FindAsync(id);
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
        public async Task<ActionResult> Edit([Bind(Include = "UserID,TitleID,Firstname,Lastname,Nickname,Username,Password,Email,IsActive,BornDate,SuperAdmin,DepartmentID")] User user)
        {
            user.OrganizationID = Organization.current.OrganizationID;
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.TitleID = new SelectList(db.Titles, "TitleID", "Name");
            ViewBag.DepartmentID = new SelectList(db.Departments, "DepartmentID", "Name");
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
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
