using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TPFinalProgWebIII.Controllers
{
    public class TareasController : CustomController
    {

        PW3TP_20181C_TareasEntities db = new PW3TP_20181C_TareasEntities();
        // GET: Tareas
        public ActionResult Index()
        {
            int id;
            int.TryParse(Session["IdUsuario"].ToString(), out id);
            Usuario usuario = db.Usuario.Single(x => x.IdUsuario == id);

            ViewBag.tareas = usuario.Tarea.OrderByDescending(x => x.FechaCreacion).ToList();

            return View("Index", ViewBag);
        }

        public ActionResult Crear()
        {
         
            return View();
        }

        public ActionResult Detalle(int id)
        {
            ViewBag.tarea =  db.Tarea.Find(id);
            return View("Detalle", ViewBag);
        }
    }
}