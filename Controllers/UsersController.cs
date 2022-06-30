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

namespace MIM.Controllers
{
    public class UsersController : Controller
    {
        private MIMDBContext db = new MIMDBContext();

        public ActionResult TestDeneme()
        {
            ViewBag.organizationID = new SelectList(db.Organizations, "organizationID", "name");
            ViewBag.titleID = new SelectList(db.Titles, "titleID", "name");
            return View();
        }

        // GET: /Users
        public async Task<ActionResult> Index()
        {            
            var users = db.Users.Include(u => u.organization).Include(u => u.title);
            IEnumerable<User> asd = db.Users;
            return View(await users.ToListAsync());
        }


        // GET: /Users/Table
        public async Task<ActionResult> Table()
        {
            var users = db.Users.Include(u => u.organization).Include(u => u.title);
            return View(await users.ToListAsync());
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
            ViewBag.organizationID = new SelectList(db.Organizations, "organizationID", "name");
            ViewBag.titleID = new SelectList(db.Titles, "titleID", "name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "userID,organizationID,titleID,firstname,lastname,nickname,username,password,email,isActive,bornDate,superAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.organizationID = new SelectList(db.Organizations, "organizationID", "name", user.organizationID);
            ViewBag.titleID = new SelectList(db.Titles, "titleID", "name", user.titleID);
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
            ViewBag.organizationID = new SelectList(db.Organizations, "organizationID", "name", user.organizationID);
            ViewBag.titleID = new SelectList(db.Titles, "titleID", "name", user.titleID);
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "userID,organizationID,titleID,firstname,lastname,nickname,username,password,email,isactive,borndate,superAdmin")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("List");
            }
            ViewBag.organizationID = new SelectList(db.Organizations, "organizationID", "name", user.organizationID);
            ViewBag.titleID = new SelectList(db.Titles, "titleID", "name", user.titleID);
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
            return RedirectToAction("List");
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
