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

namespace Escuela.Areas.Academica.Controllers
{
    [Authorize(Roles = "ACADEMICA_INSCRIPCION")]
    public class ProfesorController : Controller
    {
        private App_DataEntities db = new App_DataEntities();

        // GET: Academica/Profesor
        public async Task<ActionResult> Index()
        {
            var profesor = db.Profesor.Include(p => p.Sexo);
            return View(await profesor.ToListAsync());
        }

        // GET: Academica/Profesor/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = await db.Profesor.FindAsync(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        // GET: Academica/Profesor/Create
        public ActionResult Create()
        {
            ViewBag.Sexo_idSexo = new SelectList(db.Sexo, "idSexo", "descripcion");
            return View();
        }

        // POST: Academica/Profesor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RFC,nombre,apellidoPaterno,apellidoMaterno,fechaNacimiento,Sexo_idSexo,cedula")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                db.Profesor.Add(profesor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Sexo_idSexo = new SelectList(db.Sexo, "idSexo", "descripcion", profesor.Sexo_idSexo);
            return View(profesor);
        }

        // GET: Academica/Profesor/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = await db.Profesor.FindAsync(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sexo_idSexo = new SelectList(db.Sexo, "idSexo", "descripcion", profesor.Sexo_idSexo);
            return View(profesor);
        }

        // POST: Academica/Profesor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "RFC,nombre,apellidoPaterno,apellidoMaterno,fechaNacimiento,Sexo_idSexo,cedula")] Profesor profesor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(profesor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Sexo_idSexo = new SelectList(db.Sexo, "idSexo", "descripcion", profesor.Sexo_idSexo);
            return View(profesor);
        }

        // GET: Academica/Profesor/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = await db.Profesor.FindAsync(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        // POST: Academica/Profesor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Profesor profesor = await db.Profesor.FindAsync(id);
            db.Profesor.Remove(profesor);
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
