using BotDetect.Web.Mvc;
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
                        ViewData["MensajeOK"] = "Necesitas activar tu cuenta primero.";
                        return View("Login");
                    }
                    
                    //Seteando la sesión
                    Session["IdUsuario"] = usuario.IdUsuario;
                    Session["Nombre"] = usuario.Email;

                    if (login.Recordarme)
                        CookieHandler(login.Email);

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

            RegistroHandler(registro);
            return View("Registracion");
        }

        public ActionResult Logout()
        {
            if (!Session["Nombre"].Equals(String.Empty))
                Session["Nombre"] = String.Empty;

            return RedirectToAction("Index", "Home");
        }

        /*===========================================================
         * ==================MÉTODO(S) PRIVADO(S)====================
         ===========================================================*/        

        private void RegistroHandler(Registro registro)
        {            
            Usuario usuario = _usuarioService.FindByEmail(registro.Email);

            //Si el mail no esta en uso
            if (usuario == null)    
            {
                usuario = _usuarioService.BuildUsuario(new Usuario(), registro);
                ViewData["MensajeOK"] = "Nuevo registro agregado a la BDD";
                _generalService.Create(usuario);
            }
            else    //Si esta en uso...
            {
                //... y el usuario no es activo...
                if (usuario.Activo == 0)
                {
                    usuario = _usuarioService.BuildUsuario(usuario, registro);
                    _generalService.Update(usuario);
                    ViewData["MensajeOK"] = "Se actualizo un usuario viejo";
                }
                //... y el usuario es activo...
                else ViewData["MensajeOK"] = "El mail se encuentra en uso";
            }          
        }

        /*Creando una cookie
         - Pendiente encriptarla, probablemente se pueda hacer 
         una clase con métodos para encriptar/desencriptar luego. 
         Dejo esto aca para que quede a la vista*/
        private void CookieHandler(string email)
        {
            if (Request.Cookies["Usuario"] == null)
            {
                Response.Cookies["Usuario"]["Mail"] = email;
                Response.Cookies["Usuario"].Expires = DateTime.Now.AddDays(90);
            }
        }
    }
}