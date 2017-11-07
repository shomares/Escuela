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
    public class AlumnoMateriaController : Controller
    {
        private App_DataEntities db = new App_DataEntities();

        // GET: Academica/AlumnoMateria
        public async Task<ActionResult> Index()
        {
            var alumnoMateria = db.AlumnoMateria.Include(a => a.Alumno).Include(a => a.MateriaProfesor);
            return View(await alumnoMateria.ToListAsync());
        }

        // GET: Academica/AlumnoMateria/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlumnoMateria alumnoMateria = await db.AlumnoMateria.FindAsync(id);
            if (alumnoMateria == null)
            {
                return HttpNotFound();
            }
            return View(alumnoMateria);
        }

        // GET: Academica/AlumnoMateria/Create
        public ActionResult Create()
        {
            ViewBag.Alumno_boleta = new SelectList((from s in db.Alumno
                                                    select new
                                                    {
                                                        boleta = s.boleta,
                                                        name = s.nombre + " " + s.apellidoPaterno + " " + s.apellidoMaterno
                                                    }), "boleta", "name");
            ViewBag.MateriaProfesor_idMateriaProfesor = new SelectList((from s in db.MateriaProfesor
                                                                        select new
                                                                        {
                                                                            idMateriaProfesor = s.Materia_idMateria,
                                                                            name = s.Materia.nombre + "/" + s.Profesor.nombre + " " + s.Profesor.apellidoPaterno + " " + s.Profesor.apellidoMaterno
                                                                            + "/" + s.periodo
                                                                        }), "idMateriaProfesor", "name");
            return View();
        }

        // POST: Academica/AlumnoMateria/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idAlumnoMateria,MateriaProfesor_idMateriaProfesor,Alumno_boleta")] AlumnoMateria alumnoMateria)
        {
            if (ModelState.IsValid)
            {
                db.AlumnoMateria.Add(alumnoMateria);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Alumno_boleta = new SelectList(db.Alumno, "boleta", "nombre", alumnoMateria.Alumno_boleta);
            ViewBag.MateriaProfesor_idMateriaProfesor = new SelectList(db.MateriaProfesor, "idMateriaProfesor", "periodo", alumnoMateria.MateriaProfesor_idMateriaProfesor);
            return View(alumnoMateria);
        }

        // GET: Academica/AlumnoMateria/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlumnoMateria alumnoMateria = await db.AlumnoMateria.FindAsync(id);
            if (alumnoMateria == null)
            {
                return HttpNotFound();
            }
            ViewBag.Alumno_boleta = new SelectList((from s in db.Alumno
                                                    select new
                                                    {
                                                        boleta = s.boleta,
                                                        name = s.nombre + " " + s.apellidoPaterno + " " + s.apellidoMaterno
                                                    }), "boleta", "name");
            ViewBag.MateriaProfesor_idMateriaProfesor = new SelectList((from s in db.MateriaProfesor
                                                                        select new
                                                                        {
                                                                            idMateriaProfesor = s.Materia_idMateria,
                                                                            name = s.Materia.nombre + "/" + s.Profesor.nombre + " " + s.Profesor.apellidoPaterno + " " + s.Profesor.apellidoMaterno
                                                                            + "/" + s.periodo
                                                                        }), "idMateriaProfesor", "name"); return View(alumnoMateria);
        }

        // POST: Academica/AlumnoMateria/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idAlumnoMateria,MateriaProfesor_idMateriaProfesor,Alumno_boleta")] AlumnoMateria alumnoMateria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumnoMateria).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Alumno_boleta = new SelectList(db.Alumno, "boleta", "nombre", alumnoMateria.Alumno_boleta);
            ViewBag.MateriaProfesor_idMateriaProfesor = new SelectList(db.MateriaProfesor, "idMateriaProfesor", "periodo", alumnoMateria.MateriaProfesor_idMateriaProfesor);
            return View(alumnoMateria);
        }

        // GET: Academica/AlumnoMateria/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlumnoMateria alumnoMateria = await db.AlumnoMateria.FindAsync(id);
            if (alumnoMateria == null)
            {
                return HttpNotFound();
            }
            return View(alumnoMateria);
        }

        // POST: Academica/AlumnoMateria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AlumnoMateria alumnoMateria = await db.AlumnoMateria.FindAsync(id);
            db.AlumnoMateria.Remove(alumnoMateria);
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
