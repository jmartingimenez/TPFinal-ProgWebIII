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
         private static string NuevoCodigoDeActivacion()
        {
            return "RANDOM123";
        }
    }
}