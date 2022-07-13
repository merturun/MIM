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
using PagedList;

namespace MIM.Controllers
{
    public class DepartmentsController : Controller
    {
        private MIMDBContext db = new MIMDBContext();

        // GET: Departments
        public async Task<ActionResult> Index()
        {
            var departments = db.Departments.Include(d => d.Organization);
            return View(await departments.ToListAsync());
        }

        // GET: /Departments/Table
        public ActionResult Table(int? page)
        {
            var _page = page ?? 1;
            var departments = db.Departments.Include(u => u.Organization).Include(u => u.User).Where(x => x.OrganizationID == Organization.current.OrganizationID).ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            ViewBag.UserID = new SelectList(db.Users, "UserID", "fullname");
            return View(departments);
        }

        // GET: Departments/Details/5
        public async Task<ActionResult> Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.Users, "UserID", "fullname");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartmentID,Name,Description,UserID")] Department department)
        {
            department.OrganizationID = Organization.current.OrganizationID;
            if (ModelState.IsValid)
            {
                db.Departments.Add(department);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "fullname");
            return View(department);
        }

        // GET: Departments/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "fullname");
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DepartmentID,Name,Description,UserID")] Department department)
        {
            department.OrganizationID = Organization.current.OrganizationID;
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "fullname");
            return View(department);
        }

        // GET: Departments/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = await db.Departments.FindAsync(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Department department = await db.Departments.FindAsync(id);
            db.Departments.Remove(department);
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
