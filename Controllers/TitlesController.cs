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
    public class TitlesController : Controller
    {
        private MIMDBContext db = new MIMDBContext();

        // GET: Titles
        public async Task<ActionResult> Index()
        {
            var titles = db.Titles.Include(t => t.Organization).Where(x=>x.OrganizationID == Organization.current.OrganizationID);
            return View(await titles.ToListAsync());
        }

        public IPagedList<Title> GetTitleList(Title title, int? page)
        {
            var _page = page ?? 1;
            var titles = db.Titles.Include(u => u.Organization).Where(x => x.OrganizationID == Organization.current.OrganizationID);
            IEnumerable<Title> filtering_title = titles.ToList();
            if (title.TitleID > 0) filtering_title = filtering_title.Where(x => x.TitleID == title.TitleID).ToList();
            if (title.Name != null) filtering_title = filtering_title.Where(x => x.Name.Contains(title.Name)).ToList();
            if (title.Description != null) filtering_title = filtering_title.Where(x => x.Description.Contains(title.Description)).ToList();
            return filtering_title.ToPagedList(_page, MvcApplication.ListPerPage);
        }

        // GET: /Titles/Table
        public ActionResult Table(Title title, int? page)
        {
            bool grant = ViewBag.grant = MIM.Models.User.Current().isGranted("Table", "Titles");
            if (!grant) return View("");
            var _page = page ?? 1;
            var titles = GetTitleList(title, page);            
            return View(titles);
        }

        // GET: Titles/Details/5
        public async Task<ActionResult> Show(int? id)
        {
            bool grant = ViewBag.grant = MIM.Models.User.Current().isGranted("Show", "Titles");
            if (!grant) return View("");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = await db.Titles.FindAsync(id);
            if (title == null)
            {
                title.Name = "Ünvan yok";
                return View(title);
            }
            return View(title);
        }

        // GET: Titles/Create
        public ActionResult Create()
        {
            bool grant = ViewBag.grant = MIM.Models.User.Current().isGranted("Create", "Titles");
            if (!grant) return View("");
            return View();
        }

        // POST: Titles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "titleID,name,description")] Title title)
        {
            bool grant = ViewBag.grant = MIM.Models.User.Current().isGranted("Create", "Titles");
            if (!grant) return View("");
            title.OrganizationID = Organization.current.OrganizationID;
            if (ModelState.IsValid)
            {
                db.Titles.Add(title);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(title);
        }

        // GET: Titles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            bool grant = ViewBag.grant = MIM.Models.User.Current().isGranted("Edit", "Titles");
            if (!grant) return View("");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = await db.Titles.FindAsync(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: Titles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "titleID,name,description")] Title title)
        {
            bool grant = ViewBag.grant = MIM.Models.User.Current().isGranted("Edit", "Titles");
            if (!grant) return View("");
            title.OrganizationID = Organization.current.OrganizationID;
            if (ModelState.IsValid)
            {
                db.Entry(title).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(title);
        }

        // GET: Titles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            bool grant = ViewBag.grant = MIM.Models.User.Current().isGranted("Delete", "Titles");
            if (!grant) return View("");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Title title = await db.Titles.FindAsync(id);
            if (title == null)
            {
                return HttpNotFound();
            }
            return View(title);
        }

        // POST: Titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            bool grant = ViewBag.grant = MIM.Models.User.Current().isGranted("Delete", "Titles");
            if (!grant) return View("");
            var users = db.Users.Where(x => x.OrganizationID == Organization.current.OrganizationID && x.TitleID == id);
            foreach (var item in users) item.TitleID = null;
            db.SaveChanges();
            Title title = await db.Titles.FindAsync(id);
            db.Titles.Remove(title);
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
