using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TPFinalProgWebIII.Controllers
{
    public class CustomController : Controller
    {

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["Nombre"].Equals(String.Empty))
            {
                //Para obtener a donde se queria ingresar
                string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
                string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();

                Debug.WriteLine("\n\t\t\t=========================="+ 
                    actionName + ", " + controllerName);

                filterContext.Result = RedirectToAction("Login", "Home");
            }               
        }
    }
}