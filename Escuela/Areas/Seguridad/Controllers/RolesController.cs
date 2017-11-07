using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Escuela.Types;

namespace Escuela.Areas.Seguridad.Controllers
{
    [Authorize(Roles = "SEGURIDAD")]
    public class RolesController : Controller
    {
        private App_DataEntities db = new App_DataEntities();

        // GET: Seguridad/Roles
        public async Task<ActionResult> Index()
        {
            return View(await db.secRol.ToListAsync());
        }

        // GET: Seguridad/Roles/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            secRol secRol = await db.secRol.FindAsync(id);
            if (secRol == null)
            {
                return HttpNotFound();
            }
            return View(secRol);
        }

        // GET: Seguridad/Roles/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Seguridad/Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idsecRol,nombre,descripcion")] secRol secRol)
        {
            if (ModelState.IsValid)
            {
                db.secRol.Add(secRol);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(secRol);
        }

        // GET: Seguridad/Roles/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            secRol secRol = await db.secRol.FindAsync(id);
            if (secRol == null)
            {
                return HttpNotFound();
            }
            return View(secRol);
        }

        // POST: Seguridad/Roles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idsecRol,nombre,descripcion")] secRol secRol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secRol).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(secRol);
        }

        // GET: Seguridad/Roles/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            secRol secRol = await db.secRol.FindAsync(id);
            if (secRol == null)
            {
                return HttpNotFound();
            }
            return View(secRol);
        }

        // POST: Seguridad/Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            secRol secRol = await db.secRol.FindAsync(id);
            db.secRol.Remove(secRol);
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
