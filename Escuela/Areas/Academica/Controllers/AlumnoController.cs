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
    public class AlumnoController : Controller
    {
        private App_DataEntities db = new App_DataEntities();

        // GET: Academica/Alumno
        public async Task<ActionResult> Index()
        {
            var alumno = db.Alumno.Include(a => a.Sexo);
            return View(await alumno.ToListAsync());
        }

        // GET: Academica/Alumno/Details/5
        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = await db.Alumno.FindAsync(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // GET: Academica/Alumno/Create
        public ActionResult Create()
        {
            ViewBag.Sexo_idSexo = new SelectList(db.Sexo, "idSexo", "descripcion");
            return View();
        }

        // POST: Academica/Alumno/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "boleta,nombre,apellidoPaterno,apellidoMaterno,fechaNacimiento,Sexo_idSexo")] Alumno alumno)
        {

            if (ModelState.IsValid)
            {
                db.Alumno.Add(alumno);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.Sexo_idSexo = new SelectList(db.Sexo, "idSexo", "descripcion", alumno.Sexo_idSexo);
            return View(alumno);
        }

        // GET: Academica/Alumno/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = await db.Alumno.FindAsync(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            ViewBag.Sexo_idSexo = new SelectList(db.Sexo, "idSexo", "descripcion", alumno.Sexo_idSexo);
            return View(alumno);
        }

        // POST: Academica/Alumno/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "boleta,nombre,apellidoPaterno,apellidoMaterno,fechaNacimiento,Sexo_idSexo")] Alumno alumno)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alumno).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.Sexo_idSexo = new SelectList(db.Sexo, "idSexo", "descripcion", alumno.Sexo_idSexo);
            return View(alumno);
        }

        // GET: Academica/Alumno/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Alumno alumno = await db.Alumno.FindAsync(id);
            if (alumno == null)
            {
                return HttpNotFound();
            }
            return View(alumno);
        }

        // POST: Academica/Alumno/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            Alumno alumno = await db.Alumno.FindAsync(id);
            db.Alumno.Remove(alumno);
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
