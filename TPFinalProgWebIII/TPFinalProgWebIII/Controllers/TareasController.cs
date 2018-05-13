using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TPFinalProgWebIII.Controllers
{
    public class TareasController : Controller
    {
        // GET: Tareas
        public ActionResult Index()
        {
            if (Session["Nombre"].Equals(String.Empty))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Crear()
        {
            if (Session["Nombre"].Equals(String.Empty))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Detalle()
        {
            if (Session["Nombre"].Equals(String.Empty))
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
    }
}