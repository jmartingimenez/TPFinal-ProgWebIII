using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPFinalProgWebIII.Models.Service;
using TPFinalProgWebIII.Models.Repository;
using TPFinalProgWebIII.Models.RepositoryImp;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Models.ServiceImp
{
    public class UsuarioServiceImp : IGeneralService<Usuario>, IUsuarioService
    {
        private IGeneralRepository<Usuario> _generalRepository;
        private IUsuarioService _usuarioService;
        
        //para probar el login.
        public UsuarioServiceImp() { }


        public UsuarioServiceImp(IGeneralRepository<Usuario> generalRepository, IUsuarioService usuarioService)
        {
            _generalRepository = generalRepository;
            _usuarioService = usuarioService;
        }

        public Usuario Crear(Usuario a)
        {
           return _generalRepository.Crear(a);
        }

        public void Eliminar(Usuario a)
        {
            _generalRepository.Crear(a);
        }

        public List<Usuario> Listar()
        {
            return _generalRepository.Listar();
        }

        public Usuario Modificar(Usuario a)
        {
            return _generalRepository.Modificar(a);
        }

        public Usuario BuscarPorId(int id)
        {
            return _generalRepository.BuscarPorId(id);
        }

        public bool Login(Login login)
        {

            //UsuarioRepositoryImp uri = new UsuarioRepositoryImp();
           // return uri.Login(login);

            return _usuarioService.Login(login);
        }
    }
}