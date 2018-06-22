using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPFinalProgWebIII.Models.Enum;
using TPFinalProgWebIII.Models.Service;
using TPFinalProgWebIII.Models.ServiceImp;
using TPFinalProgWebIII.Models.Util;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Controllers
{
    public class HomeController : CustomController
    {
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
            //Si ya estas logeado vas al index
            if (!Session["Nombre"].Equals(String.Empty))
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Procesar-Login")]
        public ActionResult Login(Login login)
        {
            Usuario usuario = new Usuario();

            if (!ModelState.IsValid)
                return View("Login", login);
            else
            {
                usuario = _usuarioService.Login(login);

                if (usuario != null){
                    
                    //No permitiendo logearse a un user inactivo
                    if(usuario.Activo == 0)
                    {
                        ViewBag.msg = "Necesitas activar tu cuenta primero.";
                        
                        return View("Login");
                    }
                    
                    //Seteando la sesión
                    Session["IdUsuario"] = usuario.IdUsuario;
                    Session["Nombre"] = usuario.Nombre;

                    if (login.Recordarme)
                    {
                        Response.Cookies["Usuario"]["IdUsuario"] = CryptHandler.Crypt(usuario.IdUsuario.ToString());
                        Response.Cookies["Usuario"]["Nombre"] = CryptHandler.Crypt(usuario.Nombre);
                        Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(90);
                    }

                    /*Si se intento entrar a una URL que 
                     requiere logearse, te devuelvo a esa. Si no, 
                     al Index*/
                    string destino = HttpUtility.ParseQueryString(Request.UrlReferrer.Query)["ReturnUrl"];
                    if (destino != null)
                        return Redirect(destino);

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
            //Si ya estas logeado vas al index
            if (!Session["Nombre"].Equals(String.Empty))
                return RedirectToAction("Index");

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
            if (!ModelState.IsValid)
                return View("Registracion", registro);

            EstadoMail estadoMail = _usuarioService.ComprobarEstadoMail(registro.Email);
            if (estadoMail == EstadoMail.NUEVO_MAIL)
            {
                _usuarioService.RegistrarUsuarioConMailNuevo(registro);
                ViewData["MensajeOK"] = "Cuenta nueva. Se le ha enviado un mail con su clave de activación";
            }
            else
            {
                if (estadoMail == EstadoMail.MAIL_INACTIVO)
                {
                    _usuarioService.RegistrarUsuarioConMailSinUso(registro);
                    ViewData["MensajeOK"] = "Se le ha enviado un mail con su clave de activación";
                }
                else ViewData["MensajeOK"] = "El mail se encuentra en uso";
            }

            return View("Registracion");
        }

        public ActionResult Logout()
        {
            /*Si estabas logeado además de desreferenciar de la sesión, hago vencer la 
             cookie para que luego te pida de nuevo logearte*/
            if (!Session["Nombre"].Equals(String.Empty))
            {
                Session["Nombre"] = String.Empty;
                if(Request.Cookies["Usuario"] != null)
                    Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(-1);
            }
                

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ActivarCuenta()
        {
            if (!Session["Nombre"].Equals(String.Empty))
                return RedirectToAction("Index");
            return View();
        }

        [HttpPost]
        public ActionResult ActivarCuenta(CodigoDeActivacion cda)
        {
            
                ViewBag.p = "entro";
                if (!ModelState.IsValid)
                {
                    ViewBag.p = "fallo";
                    return View(cda);
                }
                //activo usuario
                Usuario user = _usuarioService.ActivateAccount(cda);
                //Creo carpeta general
                _usuarioService.CrearCarpetaGeneral(user);
                ViewBag.p = user.Nombre + user.FechaActivacion + user.Activo + user.IdUsuario + user.Apellido + user.Email + user.FechaRegistracion;
                //}

                return View();

            
        }
    }
}