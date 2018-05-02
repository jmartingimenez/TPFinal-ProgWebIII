using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPFinalProgWebIII.Models.Repository;
using TPFinalProgWebIII.Models.Entity;

namespace TPFinalProgWebIII.Models.RepositoryImp
{
    public class UsuarioRepositoryImp : IGeneralRepository<Usuario>
    {

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
    }
}