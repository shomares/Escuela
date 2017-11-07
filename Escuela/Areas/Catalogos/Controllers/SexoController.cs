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

namespace Escuela.Areas.Catalogos.Controllers
{
    [Authorize(Roles = "CATALOGOS")]

    public class SexoController : Controller
    {
        private App_DataEntities db = new App_DataEntities();

        // GET: Catalogos/Sexo
        public async Task<ActionResult> Index()
        {
            return View(await db.Sexo.ToListAsync());
        }

        // GET: Catalogos/Sexo/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = await db.Sexo.FindAsync(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // GET: Catalogos/Sexo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Catalogos/Sexo/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "idSexo,descripcion")] Sexo sexo)
        {
            if (ModelState.IsValid)
            {
                db.Sexo.Add(sexo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sexo);
        }

        // GET: Catalogos/Sexo/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = await db.Sexo.FindAsync(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // POST: Catalogos/Sexo/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "idSexo,descripcion")] Sexo sexo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sexo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sexo);
        }

        // GET: Catalogos/Sexo/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sexo sexo = await db.Sexo.FindAsync(id);
            if (sexo == null)
            {
                return HttpNotFound();
            }
            return View(sexo);
        }

        // POST: Catalogos/Sexo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sexo sexo = await db.Sexo.FindAsync(id);
            db.Sexo.Remove(sexo);
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
