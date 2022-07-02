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
    public class titlesController : Controller
    {
        private MIMDBContext db = new MIMDBContext();


        // GET: titles
        public async Task<ActionResult> List()
        {
            Session["asd"] = "asd";
            return View(await db.Titles.ToListAsync());
        }

        // GET: titles/Details/5
        public async Task<ActionResult> Show(int? id)
        {
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

        // GET: titles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: titles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "titleID,name,description")] Title title)
        {
            if (ModelState.IsValid)
            {
                db.Titles.Add(title);
                await db.SaveChangesAsync();
                return RedirectToAction("List");
            }

            return View(title);
        }

        // GET: titles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
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

        // POST: titles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "titleID,name,description")] Title title)
        {
            if (ModelState.IsValid)
            {
                db.Entry(title).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("List");
            }
            return View(title);
        }

        // GET: titles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
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

        // POST: titles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Title title = await db.Titles.FindAsync(id);
            db.Titles.Remove(title);
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
