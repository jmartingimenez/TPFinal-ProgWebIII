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
    }
}