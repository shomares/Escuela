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
    [Authorize(Roles = "ACADEMICA_CALIFICACIONES")]

    public class CalificacionController : Controller
    {
        private App_DataEntities db = new App_DataEntities();

        // GET: Academica/Calificacion
        public async Task<ActionResult> Index()
        {
            var calificacion = db.Calificacion.Include(c => c.AlumnoMateria);
            return View(await calificacion.ToListAsync());
        }

        // GET: Academica/Calificacion/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = await db.Calificacion.FindAsync(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // GET: Academica/Calificacion/Create
        public ActionResult Create()
        {
            ViewBag.AlumnoMateria_idAlumnoMateria = new SelectList((from s in db.AlumnoMateria
                                                                    select new
                                                                    {
                                                                        idAlumnoMateria = s.idAlumnoMateria,
                                                                        Alumno_boleta = s.Alumno.boleta + "/" + s.MateriaProfesor.Materia.nombre + "-" + s.MateriaProfesor.periodo
                                                                    }), "idAlumnoMateria", "Alumno_boleta");
            return View();
        }

        // POST: Academica/Calificacion/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idCalificacion,Primer,Segunda,Tercera,AlumnoMateria_idAlumnoMateria")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Calificacion.Add(calificacion);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AlumnoMateria_idAlumnoMateria = new SelectList(db.AlumnoMateria, "idAlumnoMateria", "Alumno_boleta", calificacion.AlumnoMateria_idAlumnoMateria);
            return View(calificacion);
        }

        // GET: Academica/Calificacion/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = await db.Calificacion.FindAsync(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlumnoMateria_idAlumnoMateria = new SelectList((from s in db.AlumnoMateria
                                                                    select new
                                                                    {
                                                                        idAlumnoMateria = s.idAlumnoMateria,
                                                                        Alumno_boleta = s.Alumno.boleta + "/" + s.MateriaProfesor.Materia.nombre + "-" + s.MateriaProfesor.periodo
                                                                    }), "idAlumnoMateria", "Alumno_boleta"); return View(calificacion);
        }

        // POST: Academica/Calificacion/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idCalificacion,Primer,Segunda,Tercera,AlumnoMateria_idAlumnoMateria")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calificacion).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AlumnoMateria_idAlumnoMateria = new SelectList(db.AlumnoMateria, "idAlumnoMateria", "Alumno_boleta", calificacion.AlumnoMateria_idAlumnoMateria);
            return View(calificacion);
        }

        // GET: Academica/Calificacion/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Calificacion calificacion = await db.Calificacion.FindAsync(id);
            if (calificacion == null)
            {
                return HttpNotFound();
            }
            return View(calificacion);
        }

        // POST: Academica/Calificacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Calificacion calificacion = await db.Calificacion.FindAsync(id);
            db.Calificacion.Remove(calificacion);
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
