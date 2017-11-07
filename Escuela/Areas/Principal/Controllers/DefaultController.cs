using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Escuela.Areas.Principal.Controllers
{
    [Authorize]
    public class DefaultController : Controller
    {
        // GET: Principal/Default
        public ActionResult Index()
        {
            return View();
        }
    }
}