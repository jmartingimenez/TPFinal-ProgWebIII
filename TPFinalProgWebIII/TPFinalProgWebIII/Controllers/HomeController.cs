﻿using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPFinalProgWebIII.Models.Service;
using TPFinalProgWebIII.Models.ServiceImp;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Controllers
{
    public class HomeController : Controller
    {

        PW3TP_20181C_TareasEntities db = new PW3TP_20181C_TareasEntities();

        private IUsuarioService _usuarioService;
        private IGeneralService<Usuario> _generalService;

        public HomeController()
        {
        }

        public HomeController(IUsuarioService usuarioService, IGeneralService<Usuario> generalService)
        {
            _usuarioService = usuarioService;
            _generalService = generalService;
        }



        // GET: Home
        public ActionResult Index()
        {
            int id;

            if (Session["Nombre"].Equals(String.Empty))
                return View();
            else {

                int.TryParse(Session["IdUsuario"].ToString(), out id);
                Usuario usuario = _generalService.Get(id);

                List<Carpeta> carpetas = usuario.Carpeta.OrderBy(x => x.Nombre).ToList();
                List<Tarea> tareas = usuario.Tarea.OrderBy(x => x.Prioridad).ThenBy(x => x.FechaFin).ToList();

                ViewBag.carpetas = carpetas;
                ViewBag.tareas = tareas;

                return View("Index", ViewBag);
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Procesar-Login")]
        public ActionResult Login(Login login)
        {

            Usuario usuario = new Usuario();

            /*Si los datos no son validos o estan incompletos se vuelve a la vista 
             y se muestran los errores*/

            if (!ModelState.IsValid)
                return View("Login", login);
            else
            {

                usuario = _usuarioService.Login(login);

                if (usuario != null){
                    
                    //No permitiendo logearse a un user inactivo
                    if(usuario.Activo == 0)
                    {
                        ViewData["MensajeOK"] = "Necesitas activar tu cuenta primero.";
                        return View("Login");
                    }
                    
                    //probando la sesion
                    Session["IdUsuario"] = usuario.IdUsuario;
                    Session["Nombre"] = usuario.Email;

                    Debug.WriteLine(Session["IdUsuario"]);

                    /*Creando una cookie
                     - Pendiente revisar que exista
                     - Pendiente encriptarla*/
                    if (login.Recordarme)
                    {
                        Response.Cookies["Usuario"]["Mail"] = login.Email;
                        Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(90);
                    }

                    return RedirectToAction("Index");
               }
                else
                {
                    ViewData["MensajeOK"] = "Usuario o contraseña incorrecto.";

                    return View("Login", login);
                }
            }            
        }

        public ActionResult Registracion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        [CaptchaValidation("CaptchaCode", "Captcha", 
            "Ingrese el captcha correctamente")]
        [ActionName("Procesar-Registro")]
        public ActionResult Registracion(Registro registro)
        {
            if (!ModelState.IsValid) return View("Registracion", registro);

            //Lo que esta aca, pasarlo luego a servicios
            ViewData["MensajeOK"] = this.GestionRegistro(registro);

            return View("Registracion");
        }

        public ActionResult Logout()
        {
            if (!Session["Nombre"].Equals(String.Empty))
            {
                Session["Nombre"] = String.Empty;
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        /*===========================================================
         * ESTO SE TIENE QUE SACAR DE ACA LUEGO, PASARLO A SERVICIOS
         ===========================================================*/        

        private string GestionRegistro(Registro registro)
        {
            string mensaje = "El mail se encuentra en uso.";
            Usuario usuario = _usuarioService.FindByEmail(registro.Email);

            if (usuario == null)    
            {
                usuario = _usuarioService.BuildUsuario(new Usuario(), registro);
                mensaje = "Nuevo registro agregado a la BDD.";
                _generalService.Create(usuario);
            }
            else                    
            {
                if (usuario.Activo == 1)
                    return mensaje;

                usuario = _usuarioService.BuildUsuario(usuario, registro);
                _generalService.Update(usuario);
                mensaje = "Se actualizo un usuario viejo.";
            }
            return mensaje;
        }
    }}