using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TPFinalProgWebIII.Controllers
{
    public class CarpetasController : CustomController
    {
        // GET: Carpetas
        public ActionResult Index()
        {
          
            return View();
        }

        public ActionResult Crear()
        {
         
            return View();
        }

        public ActionResult Tareas()
        {
        
            return View();
        }
    }
}