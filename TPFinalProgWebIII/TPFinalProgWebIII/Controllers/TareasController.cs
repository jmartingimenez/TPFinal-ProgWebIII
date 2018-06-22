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
        private IGeneralService<Usuario> _generalUserService;

        public TareasController(IGeneralService<Tarea> generalService, IGeneralService<Usuario> generalUserService)
        {
            _generalService = generalService;
            _generalUserService = generalUserService;
        }

        // GET: Tareas
        public ActionResult Index()
        {
            try
            {

                int id;
                int.TryParse(Session["IdUsuario"].ToString(), out id);
                Usuario usuario = _generalUserService.Get(id);

                ViewBag.tareas = usuario.Tarea.OrderByDescending(x => x.FechaCreacion).ToList();

                return View("Index", ViewBag);
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public ActionResult Crear()
        {
            try
            {

                int id;
                int.TryParse(Session["IdUsuario"].ToString(), out id);
                Usuario usuario = _generalUserService.Get(id);
                //PARA EL SELECT DE CARPETAS
                IEnumerable<Carpeta> carpetas = usuario.Carpeta.ToList();

                ViewBag.carpetas = carpetas;


                return View();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crear(TareaVal tareaval)
        {
            try
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
                    tarea.Prioridad = (short)tareaval.Prioridad;
                    tarea.EstimadoHoras = tareaval.EstimadoHoras;

                    _generalService.Create(tarea);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Detalle(int id)
        {
            try
            {
                ViewBag.tarea = _generalService.Get(id);
                return View("Detalle", ViewBag);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Completar(int id)
        {
            try
            {
                Tarea tarea = _generalService.Get(id);
                tarea.Completada = 1;
                tarea.FechaFin = DateTime.Now;
                _generalService.Update(tarea);



                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}