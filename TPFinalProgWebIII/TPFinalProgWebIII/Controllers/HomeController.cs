using BotDetect.Web.Mvc;
using System;
using System.Collections.Generic;
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
        // GET: Home
        public ActionResult Index()
        { 
          
                return View();
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
            
            /*Si los datos no son validos o estan incompletos se vuelve a la vista 
             y se muestran los errores*/
            if (!ModelState.IsValid)
                return View("Login", login);
            else
            {
                /*FALTARIA PASAR LA CONSULTA A SERVICIOS*/
                if (db.Usuario.Any(x => x.Email == login.Email && x.Contrasenia == login.Contrasenia)){
                        /*Aca, es cuando los datos son correctos. Ahora se debería comprobar 
                 si existe el usuario y demas yerbas. Simplemente estoy mandando 
                 este mensaje a la vista para que se vea la diferencia.*/
                        ViewData["MensajeOK"] = "Todo OK. Ahora habría que ir a la BDD.";

                    /*Si se tildo la opción de recordar, aca es donde se gestionaría la cookie*/
                    if (login.Recordarme)
                        ViewData["MensajeOK"] = ViewData["MensajeOK"] + " Recordado!!";

                    return View("Login");
                 }
                else
                {
                    ViewData["MensajeOK"] = "Todo MAL - NO SE LOGUEO.";

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
            ViewData["MensajeOK"] = "Todo OK. Ahora habría que ir a la BDD.";
            return View("Registracion");
        }

        public ActionResult Logout()
        {
            return View();
        }
    }
}