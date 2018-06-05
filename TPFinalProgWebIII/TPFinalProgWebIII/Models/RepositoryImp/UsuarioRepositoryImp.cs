using System;
using System.Collections.Generic;
using System.Linq;
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

            return new string(Enumerable.Repeat(chars, 8)
                .Select(s => s[random.Next(s.Length)]).ToArray())+"-"+
                 new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray()) + "-" +
                 new string(Enumerable.Repeat(chars, 12)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            
        }
    }
}