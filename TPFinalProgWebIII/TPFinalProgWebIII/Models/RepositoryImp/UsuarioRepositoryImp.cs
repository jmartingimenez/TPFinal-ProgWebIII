using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using TPFinalProgWebIII.Models.Repository;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Models.RepositoryImp
{
    public class UsuarioRepositoryImp : IUsuarioRepository
    {

        PW3TP_20181C_TareasEntities db = new PW3TP_20181C_TareasEntities();

        public Usuario Login(Login login)
        {
            return db.Usuario.SingleOrDefault(x => x.Email == login.Email && x.Contrasenia == login.Contrasenia);
        }

        public Usuario FindByEmail(string email)
        {
            return db.Usuario.SingleOrDefault(x => x.Email == email);
        }

        public Usuario BuildUsuario(Usuario usuario, Registro registro)
        {
            usuario.Nombre = registro.Nombre;
            usuario.Apellido = registro.Apellido;
            usuario.Email = registro.Email;
            usuario.Contrasenia = registro.Contrasenia;
            usuario.CodigoActivacion = NuevoCodigoDeActivacion();
            usuario.FechaRegistracion = DateTime.Now;
            return usuario;
        }

        /*Ver luego como generar/acomodar esto, si se manda por 
         * mail, etc..
         *Lo pongo de manera simple para que quede a la vista */
     /*    private static string NuevoCodigoDeActivacion()
        {
            return "RANDOM123";
        }*/

        private static Random random = new Random();

        public static string NuevoCodigoDeActivacion()
        {
         const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            //lo hice así para que sea igual a los ejemplos de la bdd que nos paso los profes
            string clave= new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray())+"-"+
                 new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());


            //acá empieza el mail 
            MailMessage email = new MailMessage();

            email.To.Add(new MailAddress("mail destinatario"));
            email.From = new MailAddress("mail remitente");
            email.Subject = "Asunto (Probando el mail) ";
            email.Body = "codigo de activacion: " + clave;
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            //smtp.live.com es para hotmail, creo que no funca con gmail
            smtp.Host = "smtp.live.com";
            smtp.Port = 25;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            //
            smtp.Credentials = new NetworkCredential("email remitente", "contraseña");

         
                smtp.Send(email);
                email.Dispose();
              

            return clave;
        }
    }
}