using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPFinalProgWebIII.Models.Enum;
using TPFinalProgWebIII.Models.Service;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Controllers
{
    public class TareasController : CustomController
    {


        private IGeneralService<Tarea> _generalService;
        PW3TP_20181C_TareasEntities db = new PW3TP_20181C_TareasEntities();

        public TareasController(IGeneralService<Tarea> generalService)
        {
            _generalService = generalService;
        }



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
            int id;
            int.TryParse(Session["IdUsuario"].ToString(), out id);
            Usuario usuario = db.Usuario.Single(x => x.IdUsuario == id);
            //PARA EL SELECT DE CARPETAS
            IEnumerable<Carpeta> carpetas = usuario.Carpeta.ToList();
            
            ViewBag.carpetas = carpetas;
            

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(TareaVal tareaval)
        {
            int id;

            if (!ModelState.IsValid)
            {
                return View();

            }
            else
            {

                Tarea tarea = new Tarea();
                int.TryParse(Session["IdUsuario"].ToString(), out id);
                tarea.IdUsuario = id;
                tarea.Nombre = tareaval.Nombre;
                tarea.Descripcion = tareaval.Descripcion;
                //FALTARIA VER COMO COMPROBAR SI NO ELEGIO CARPETA
                tarea.IdCarpeta = tareaval.IdCarpeta;
                tarea.Prioridad = (short) tareaval.Prioridad;
                tarea.EstimadoHoras = tareaval.EstimadoHoras;

                _generalService.Create(tarea);

                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Detalle(int id)
        {
            ViewBag.tarea =  db.Tarea.Find(id);
            return View("Detalle", ViewBag);
        }
    }
}