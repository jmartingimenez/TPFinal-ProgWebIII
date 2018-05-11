using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPFinalProgWebIII.Models.Repository;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Models.RepositoryImp
{
    public class UsuarioRepositoryImp : IGeneralRepository<Usuario>, IUsuarioRepository
    {

        PW3TP_20181C_TareasEntities db = new PW3TP_20181C_TareasEntities();

        public Usuario Crear(Usuario a)
        {
            return a;
        }

        public void Eliminar(Usuario a)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> Listar()
        {
            throw new NotImplementedException();
        }

        public Usuario Modificar(Usuario a)
        {
            return a;
        }


        public Usuario BuscarPorId(int id)
        {
            throw new NotImplementedException();
        }

        public bool Login(Login login)
        {
            return db.Usuario.Any(x => x.Email == login.Email && x.Contrasenia == login.Contrasenia);
        }
        
    }
}