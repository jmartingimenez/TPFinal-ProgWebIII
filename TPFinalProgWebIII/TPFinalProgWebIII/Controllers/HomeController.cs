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
        // GET: Home
        public ActionResult Index()
        {
            int id; 

            if (Session["IdUsuario"] == null)
                return View();
            else {

                int.TryParse(Session["IdUsuario"].ToString(), out id);
                Usuario usuario = db.Usuario.Single(x => x.IdUsuario == id);

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
                /*FALTARIA PASAR LA CONSULTA A SERVICIOS*/

                if (db.Usuario.Any(x => x.Email == login.Email && x.Contrasenia == login.Contrasenia)){

                    usuario = db.Usuario.Single(x => x.Email == login.Email && x.Contrasenia == login.Contrasenia);
                    /* 
                         UsuarioServiceImp usi = new UsuarioServiceImp();

                         if (usi.Login(login))
                         {*/

                    //probando la sesion
                    Session["IdUsuario"] = usuario.IdUsuario;
                    Session["Nombre"] = usuario.Email;

                    Console.WriteLine(Session["IdUsuario"]);
                    /*Aca, es cuando los datos son correctos. Ahora se debería comprobar 
                     si existe el usuario y demas yerbas. Simplemente estoy mandando 
                     este mensaje a la vista para que se vea la diferencia.*/
                    ViewData["MensajeOK"] = "Todo OK. Ahora habría que ir a la BDD";

                    /*Si se tildo la opción de recordar, aca es donde se gestionaría la cookie*/
                    if (login.Recordarme)
                        ViewData["MensajeOK"] = ViewData["MensajeOK"] + " Recordado!!";

                    return RedirectToAction("Index");
//   }

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

        private Usuario CrearUsuario(Usuario usuario, Registro registro)
        {
            usuario.Nombre = registro.Nombre;
            usuario.Apellido = registro.Apellido;
            usuario.Email = registro.Email;
            usuario.Contrasenia = registro.Contrasenia;

            /*Hay que ver luego como generarlo y como el usuario 
             se dara de alta (ej. mandar un mail)*/
            usuario.CodigoActivacion = "RANDOM123";

            /*Esto debería hacerlo solo la BDD, pero no lo hace. 
             El campo no puede ser nulo*/
            usuario.FechaRegistracion = DateTime.Now;

            return usuario;
        }
            
        /*Cuando se saque esto de aca. Desarmarlo en métodos mas 
        chicos si es posible. Estoy viendo si el user existe en 
        la BDD, y si existe si es activo o no el user. Por eso quedo 
        largo*/
        private string GestionRegistro(Registro registro)
        {
            string mensaje = "El mail se encuentra en uso.";
            Usuario usuario = db.Usuario.FirstOrDefault(x => x.Email == registro.Email);

            if (usuario == null)    
            {
                usuario = CrearUsuario(new Usuario(), registro);
                mensaje = "Nuevo registro agregado a la BDD.";
                db.Usuario.Add(usuario);
                db.Entry(usuario).State = System.Data.EntityState.Added;
            }
            else                    
            {
                if (usuario.Activo == 1)
                    return mensaje;

                usuario = CrearUsuario(usuario, registro);
                mensaje = "Se actualizo un usuario viejo.";
                db.Entry(usuario).State = System.Data.EntityState.Modified;
            }

            /*El catch lo encontre en SO. Te imprime errores de 
             insersión, por ejemplo: Un campo que no puede ser 
             NULL y no se lo estas dando.*/
            try
            {
                db.SaveChanges();
                return mensaje;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Debug.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Debug.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            catch(Exception e)
            {
                Debug.Write("\n\t\tALGUN ERROR, VER QUE PASA\n\n" + e.StackTrace);
                throw;
            }
        }
    }
}