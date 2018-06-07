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