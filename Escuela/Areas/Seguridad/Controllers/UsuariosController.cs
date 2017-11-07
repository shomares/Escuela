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
using Escuela.Code;
using Escuela.Code.Infraestructure;

namespace Escuela.Areas.Seguridad.Controllers
{
    [Authorize(Roles ="SEGURIDAD")]
    public class UsuariosController : Controller
    {
        private App_DataEntities db = new App_DataEntities();
        private ISeguridad seg = new SeguridadActiveDirectory();

        // GET: Seguridad/Usuarios
        public async Task<ActionResult> Index()
        {
            var secUser = db.secUser.Include(s => s.secRol);
            return View(await secUser.ToListAsync());
        }

        // GET: Seguridad/Usuarios/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            secUser secUser = await db.secUser.FindAsync(id);
            if (secUser == null)
            {
                return HttpNotFound();
            }
            return View(secUser);
        }

        // GET: Seguridad/Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.secRol_idsecRol = new SelectList(db.secRol, "idsecRol", "nombre");
            return View();
        }

        // POST: Seguridad/Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(secUser secUser)
        {
            if (ModelState.IsValid)
            {
                db.secUser.Add(secUser);
                await db.SaveChangesAsync();
                seg.Save(secUser);
                return RedirectToAction("Index");
            }

            ViewBag.secRol_idsecRol = new SelectList(db.secRol, "idsecRol", "nombre", secUser.secRol_idsecRol);
            return View(secUser);
        }

        // GET: Seguridad/Usuarios/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            secUser secUser = await db.secUser.FindAsync(id);
            if (secUser == null)
            {
                return HttpNotFound();
            }
            ViewBag.secRol_idsecRol = new SelectList(db.secRol, "idsecRol", "nombre", secUser.secRol_idsecRol);
            return View(secUser);
        }

        // POST: Seguridad/Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idsecUser,cveUser,email,nombre,apellidoPaterno,apellidoMaterno,activo,secRol_idsecRol,password")] secUser secUser)
        {
            if (ModelState.IsValid)
            {
                db.Entry(secUser).State = EntityState.Modified;
                await db.SaveChangesAsync();
                seg.Update(secUser, secUser.cveUser);
                return RedirectToAction("Index");
            }
            ViewBag.secRol_idsecRol = new SelectList(db.secRol, "idsecRol", "nombre", secUser.secRol_idsecRol);
            return View(secUser);
        }

        // GET: Seguridad/Usuarios/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            secUser secUser = await db.secUser.FindAsync(id);
            if (secUser == null)
            {
                return HttpNotFound();
            }
            return View(secUser);
        }

        // POST: Seguridad/Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            secUser secUser = await db.secUser.FindAsync(id);
            db.secUser.Remove(secUser);
            await db.SaveChangesAsync();
            seg.Delete(secUser);
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
