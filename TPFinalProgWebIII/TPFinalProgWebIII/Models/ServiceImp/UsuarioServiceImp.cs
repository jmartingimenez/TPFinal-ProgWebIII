using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPFinalProgWebIII.Models.Service;
using TPFinalProgWebIII.Models.Entity;
using TPFinalProgWebIII.Models.Repository;
using TPFinalProgWebIII.Models.RepositoryImp;


namespace TPFinalProgWebIII.Models.ServiceImp
{
    public class UsuarioServiceImp : IGeneralService<Usuario>
    {
        private IGeneralRepository<Usuario> generalrepository;

        public UsuarioServiceImp(IGeneralRepository<Usuario> generalrepository)
        {
            this.generalrepository = generalrepository;
        }

        public Usuario Crear(Usuario a)
        {
           return generalrepository.Crear(a);
        }

        public void Eliminar(Usuario a)
        {
            generalrepository.Crear(a);
        }

        public List<Usuario> Listar()
        {
            return generalrepository.Listar();
        }

        public Usuario Modificar(Usuario a)
        {
            return generalrepository.Modificar(a);
        }

        public Usuario BuscarPorId(int id)
        {
            return generalrepository.BuscarPorId(id);
        }
    }
}