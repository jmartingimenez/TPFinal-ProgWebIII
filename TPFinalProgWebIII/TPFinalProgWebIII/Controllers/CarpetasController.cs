using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPFinalProgWebIII.Models.Service;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Controllers
{
    public class CarpetasController : CustomController
    {

        // GET: Carpetas

        private IUsuarioService _usuarioService;
        private IGeneralService<Carpeta> _generalService;
        private IGeneralService<Usuario> _generalUserService;
        private IGeneralService<Tarea> _generalTareaService;

        public CarpetasController(IUsuarioService usuarioService, IGeneralService<Carpeta> generalService, IGeneralService<Usuario> generalUserService, IGeneralService<Tarea> generalTareaService)
        {
            _usuarioService = usuarioService;
            _generalService = generalService;
            _generalUserService = generalUserService;
            _generalTareaService = generalTareaService;
        }

        public ActionResult Index()
        {
            try
            {
                int id;
                int.TryParse(Session["IdUsuario"].ToString(), out id);

                Usuario usuario = _generalUserService.Get(id);

                ViewBag.carpetas = usuario.Carpeta.OrderBy(x => x.Nombre).ToList();

                return View("Index");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ActionResult Crear()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Crear-Carpeta")]
        public ActionResult Crear(CarpetaVal carpetaVal)
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

                    Carpeta carpeta = new Carpeta();
                    int.TryParse(Session["IdUsuario"].ToString(), out id);
                    carpeta.IdUsuario = id;
                    carpeta.Nombre = carpetaVal.Nombre;
                    carpeta.Descripcion = carpetaVal.Descripcion;

                    _generalService.Create(carpeta);

                    return RedirectToAction("Index");
                }
            }
            catch (Exception e)
            {
                throw e;
            }


        }

        public ActionResult Tareas(int id)
        {
         //   Debugger.Break();
            try
            {
                int idU;
                int.TryParse(Session["IdUsuario"].ToString(), out idU);

                Usuario usuario = _generalUserService.Get(idU);

                ViewBag.nombreCarpeta = usuario.Carpeta.Where(x => x.IdCarpeta == id).First().Nombre;

                ViewBag.tareas = _generalTareaService.GetAll().Where(x => x.IdCarpeta == id).ToList();

              
                return View("Tareas");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}