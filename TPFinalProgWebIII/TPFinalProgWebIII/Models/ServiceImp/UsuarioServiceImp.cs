using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPFinalProgWebIII.Models.Service;
using TPFinalProgWebIII.Models.Repository;
using TPFinalProgWebIII.Models.RepositoryImp;
using TPFinalProgWebIII.Models.View;
using System.Net;
using System.Net.Mail;
using TPFinalProgWebIII.Models.Enum;

namespace TPFinalProgWebIII.Models.ServiceImp
{
    public class UsuarioServiceImp : IUsuarioService
    {
        private readonly IGeneralRepository<Usuario> _generalRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        
        //para probar el login.
        public UsuarioServiceImp() { }

        public UsuarioServiceImp(IGeneralRepository<Usuario> generalRepository, IUsuarioRepository usuarioRepository)
        {
            _generalRepository = generalRepository;
            _usuarioRepository = usuarioRepository;
        }

        public Usuario Create(Usuario a)
        {
            return _generalRepository.Create(a);
        }

        public Usuario Login(Login login)
        {
            return _usuarioRepository.Login(login);
        }

        public Usuario FindByEmail(string email)
        {
            return _usuarioRepository.FindByEmail(email);
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

        private string NuevoCodigoDeActivacion()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            //lo hice así para que sea igual a los ejemplos de la bdd que nos paso los profes
            string clave = new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return clave;
        }

        public Usuario ActivateAccount(CodigoDeActivacion cda)
        {
            Usuario usuario = _usuarioRepository.FindByEmail(cda.Email);
            //HAGO ESTO PARA PODER ACTUALIZAR EL USUARIO EN EL MISMO CONTEXTO DEL QUE SE BUSCA, SI NO TIRA ERROR MULTIPLES INSTANCIAS
            usuario = _generalRepository.Get(usuario.IdUsuario);

            if(usuario.CodigoActivacion == cda.CodigoActivacion)
            {
                
                usuario.Activo = 1;
                usuario.FechaActivacion = DateTime.Now;

                _generalRepository.Update(usuario);
            }

            return usuario;
        }

        public void SendKeyByMail(Usuario usuario)
        {
            MailMessage email = new MailMessage();

            email.To.Add(new MailAddress(usuario.Email));
            email.From = new MailAddress("pw3mailsample@gmail.com");
            email.Subject = "Asunto (Validar Cuenta en MyFolder)";
            email.Body = "Usuario: " + usuario.Nombre + "  Codigo de activacion: " + usuario.CodigoActivacion + "   active su cuenta aquí: http://localhost:49525/Home/ActivarCuenta";
            email.IsBodyHtml = true;
            email.Priority = MailPriority.Normal;

            SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.live.com";
            //smtp.Port = 25;

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            smtp.Credentials = new NetworkCredential(email.From.Address, "jonimauromarcos");

            smtp.Send(email);
            email.Dispose();
        }

        public EstadoMail ComprobarEstadoMail(string email)
        {
            Usuario usuario = _usuarioRepository.FindByEmail(email);

            if (usuario == null) return EstadoMail.NUEVO_MAIL;
            else if (usuario.Activo == 1) return EstadoMail.MAIL_ACTIVO;
            return EstadoMail.MAIL_INACTIVO;
        }

        public Usuario RegistrarUsuarioConMailNuevo(Registro registro)
        {
            Usuario usuario = BuildUsuario(new Usuario(), registro);
            _generalRepository.Create(usuario);
            SendKeyByMail(usuario);
            return usuario;
        }

        public Usuario RegistrarUsuarioConMailSinUso(Registro registro)
        {
            Usuario usuario = _usuarioRepository.FindByEmail(registro.Email);
            usuario = _generalRepository.Get(usuario.IdUsuario);    //Ver como fixear esto
            usuario = BuildUsuario(usuario, registro);
            _generalRepository.Update(usuario);
            SendKeyByMail(usuario);
            return usuario;
        }

        public void CrearCarpetaGeneral(Usuario usuario)
        {

            if (!usuario.Carpeta.Any(x => x.Nombre =="General")) { 
                Carpeta carpeta = new Carpeta();
                carpeta.Nombre = "General";
                carpeta.Descripcion = "De usos multiples";

                usuario.Carpeta.Add(carpeta);
                _generalRepository.Update(usuario);

            }
        }
    }
}