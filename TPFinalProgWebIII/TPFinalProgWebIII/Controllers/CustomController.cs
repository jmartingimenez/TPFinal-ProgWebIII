using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TPFinalProgWebIII.Models.Util;

namespace TPFinalProgWebIII.Controllers
{
    public class CustomController : Controller {
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //Comprobando si puedo setear la sesión con la cookie. El If de adentro es 'por las dudas'
            if ((Session["Nombre"].Equals(String.Empty) || Session["IdUsuario"].Equals(String.Empty))
                && Request.Cookies["Usuario"] != null )
            {
                if (Request.Cookies["Usuario"]["IdUsuario"] != null && 
                    Request.Cookies["Usuario"]["Nombre"] != null)
                {
                    Session["IdUsuario"] = CryptHandler.Decrypt(Request.Cookies["Usuario"]["IdUsuario"].ToString());
                    Session["Nombre"] = CryptHandler.Decrypt(Request.Cookies["Usuario"]["Nombre"].ToString());
                }
            }

            /*Si se entra en algún ActionResult del Home, no hago nada. Los métodos de ese 
            controller tienen diferentes validaciones. Si no se cumple, entonces si generalizo 
            con lo de abajo.*/
            string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            if (controllerName.Equals("Home")) return;

            //Si no tenes permiso te mando al login con la url como parametro
            if (Session["Nombre"].Equals(String.Empty))
            {
                string urlIntentada = Request.Url.ToString();
                UrlHelper u = new UrlHelper(this.ControllerContext.RequestContext);
                string urlNueva = u.Action("Login",
                    "Home",
                    new { ReturnUrl = urlIntentada });
                filterContext.Result =  Redirect(urlNueva);
            }               
        }
    }
}