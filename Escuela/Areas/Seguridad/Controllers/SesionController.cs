using Escuela.Code;
using Escuela.Code.Infraestructure;
using Escuela.Types;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escuela.Areas.Seguridad.Controllers
{
    public class SesionController : Controller
    {
        private App_DataEntities db = new App_DataEntities();
        private ISeguridad seg = new SeguridadActiveDirectory();


        // GET: Seguridad/Sesion/Close
        public ActionResult Close()
        {
            HttpContext.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Redirect("~/");
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