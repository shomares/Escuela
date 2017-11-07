using Escuela.Code;
using Escuela.Code.Infraestructure;
using Escuela.Types;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Escuela.Controllers
{
    public class HomeController : Controller
    {
        private App_DataEntities db = new App_DataEntities();
        private ISeguridad seg = new SeguridadActiveDirectory();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        [HttpPost]
        public ActionResult Index(secUser usuario)
        {
            secUser secUser = db.secUser.Where(s => s.cveUser == usuario.cveUser).FirstOrDefault();
            if (secUser != null)
            {
                if (seg.isPasswordValid(secUser, usuario.password))
                {
                    var x = HttpContext.GetOwinContext().Authentication;
                    var claims = new List<Claim>();
                    claims.Add(new Claim(ClaimTypes.Name, secUser.cveUser));
                    claims.Add(new Claim(ClaimTypes.Role, secUser.secRol.nombre));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, secUser.cveUser));
                    var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
                    x.SignIn(identity);
                    return Redirect("~/Principal/Default");
                }
            }

            ViewBag.Error = "El usuario y/contraseña son incorrectos";
            return View();
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
