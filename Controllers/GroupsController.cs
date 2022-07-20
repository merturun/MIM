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
    public class GroupsController : Controller
    {
        private MIMDBContext db = new MIMDBContext();

        // GET: Groups
        public  ActionResult Index()
        {
            return View();
        }

        // GET: /Groups/Table
        public ActionResult Table(int? page)
        {
            var _page = page ?? 1;
            var groups = db.Groups.Include(u => u.Organization).Where(x => x.OrganizationID == Organization.current.OrganizationID).ToList().ToPagedList(_page, MvcApplication.ListPerPage);
            return View(groups);
        }

        // GET: Groups/Details/5
        public async Task<ActionResult> Show(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // GET: Groups/Create
        public ActionResult Create()
        {
            ViewBag.OrganizationID = new SelectList(db.Organizations, "OrganizationID", "Name");
            ViewBag.Grants = GetSelectedGrants(new Grant[0]);
            return View();
        }
       
        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GroupID,Name,Description")] Group group, int[] GrantIDS)
        {
            if (GrantIDS != null)
                foreach (var item in GrantIDS)
                    group.Grants.Add(db.Grants.FirstOrDefault(x => x.GrantID == item));

            group.OrganizationID = Organization.current.OrganizationID;
            if (ModelState.IsValid)
            {
                db.Groups.Add(group);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Groups = db.Grants;
            return View(group);
        }
        public List<SelectListItem> GetSelectedGrants(Grant[] grants)
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            IEnumerable<Grant> allGrants = db.Grants;
            foreach (Grant grt in allGrants)
            {
                selectListItems.Add(new SelectListItem()
                {
                    Text = grt.Name.ToString(),
                    Value = grt.GrantID.ToString(),
                    Selected = grants.Any(x => x.GrantID == grt.GrantID)
                });
            }
            return selectListItems;
        }

        // GET: Groups/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            ViewBag.Grants = GetSelectedGrants(group.Grants.ToArray()); //Mert
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GroupID,Name,Description")] Group group, int[] GrantIDS)
        {
            group.OrganizationID = Organization.current.OrganizationID;
            DBHelper dbh = new DBHelper();

            dbh.UpdateGrants(group.GroupID, GrantIDS);

            if (ModelState.IsValid)
            {
                db.Entry(group).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Grants = GetSelectedGrants(group.Grants.ToArray()); //Mert

            return View(group);
        }

        // GET: Groups/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = await db.Groups.FindAsync(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: Groups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Group group = await db.Groups.FindAsync(id);
            db.Groups.Remove(group);
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
