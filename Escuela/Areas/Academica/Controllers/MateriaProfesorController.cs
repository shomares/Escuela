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
    public class MateriaProfesorController : Controller
    {
        private App_DataEntities db = new App_DataEntities();

        // GET: Academica/MateriaProfesor
        public async Task<ActionResult> Index()
        {
            var materiaProfesor = db.MateriaProfesor.Include(m => m.Materia).Include(m => m.Profesor);
            return View(await materiaProfesor.ToListAsync());
        }

        // GET: Academica/MateriaProfesor/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MateriaProfesor materiaProfesor = await db.MateriaProfesor.FindAsync(id);
            if (materiaProfesor == null)
            {
                return HttpNotFound();
            }
            return View(materiaProfesor);
        }

        // GET: Academica/MateriaProfesor/Create
        public ActionResult Create()
        {
            ViewBag.Materia_idMateria = new SelectList(db.Materia, "idMateria", "nombre");
            ViewBag.Profesor_RFC = new SelectList((from s in db.Profesor
                                                   select new { rfc = s.RFC,
                                                       name = s.nombre + " " + s.apellidoPaterno + " " +  s.apellidoMaterno }), "rfc", "name");

            return View();
        }

        // POST: Academica/MateriaProfesor/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idMateriaProfesor,periodo,Profesor_RFC,Materia_idMateria")] MateriaProfesor materiaProfesor)
        {
            if (ModelState.IsValid)
            {
                db.MateriaProfesor.Add(materiaProfesor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Materia_idMateria = new SelectList(db.Materia, "idMateria", "nombre", materiaProfesor.Materia_idMateria);
            ViewBag.Profesor_RFC = new SelectList(db.Profesor, "RFC", "nombre", materiaProfesor.Profesor_RFC);
            return View(materiaProfesor);
        }

        // GET: Academica/MateriaProfesor/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MateriaProfesor materiaProfesor = await db.MateriaProfesor.FindAsync(id);
            if (materiaProfesor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Materia_idMateria = new SelectList(db.Materia, "idMateria", "nombre", materiaProfesor.Materia_idMateria);
            ViewBag.Profesor_RFC = new SelectList((from s in db.Profesor
                                                   select new
                                                   {
                                                       rfc = s.RFC,
                                                       name = s.nombre + " " + s.apellidoPaterno + " " + s.apellidoMaterno
                                                   }), "rfc", "name");
            return View(materiaProfesor);
        }

        // POST: Academica/MateriaProfesor/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idMateriaProfesor,periodo,Profesor_RFC,Materia_idMateria")] MateriaProfesor materiaProfesor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(materiaProfesor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Materia_idMateria = new SelectList(db.Materia, "idMateria", "nombre", materiaProfesor.Materia_idMateria);
            ViewBag.Profesor_RFC = new SelectList(db.Profesor, "RFC", "nombre", materiaProfesor.Profesor_RFC);
            return View(materiaProfesor);
        }

        // GET: Academica/MateriaProfesor/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MateriaProfesor materiaProfesor = await db.MateriaProfesor.FindAsync(id);
            if (materiaProfesor == null)
            {
                return HttpNotFound();
            }
            return View(materiaProfesor);
        }

        // POST: Academica/MateriaProfesor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MateriaProfesor materiaProfesor = await db.MateriaProfesor.FindAsync(id);
            db.MateriaProfesor.Remove(materiaProfesor);
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
