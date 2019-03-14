using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Calculadora.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        //POST: Home
        [HttpPost]
        public ActionResult Index(string bt, string visor)
        {
            return View();
        }
    }
}