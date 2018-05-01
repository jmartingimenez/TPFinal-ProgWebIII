using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPFinalProgWebIII.Models.Entity;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Controllers
{
    public class HomeController : Controller
    {
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
        [ActionName("Procesar-Login")]
        public ActionResult Login(Login login)
        {               
            /*Si los datos no son validos o estan incompletos se vuelve a la vista 
             y se muestran los errores*/
            if (!ModelState.IsValid) return View("Login", login);

            /*Aca, es cuando los datos son correctos. Ahora se debería comprobar 
             si existe el usuario y demas yerbas. Simplemente estoy mandando 
             este mensaje a la vista para que se vea la diferencia.*/
            ViewData["MensajeOK"] = "Todo OK. Ahora habría que ir a la BDD.";

            /*Si se tildo la opción de recordar, aca es donde se gestionaría la cookie*/
            if (login.Recordarme)
                ViewData["MensajeOK"] = ViewData["MensajeOK"] + " Recordado!!";

            return View("Login");
        }

        public ActionResult Registracion()
        {
            return View();
        }

        [HttpPost]
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