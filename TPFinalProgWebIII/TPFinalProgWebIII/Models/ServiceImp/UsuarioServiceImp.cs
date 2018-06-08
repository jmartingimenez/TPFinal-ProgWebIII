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

        public void Delete(Usuario a)
        {
            throw new NotImplementedException();
        }

        public Usuario Get<TKey>(TKey id)
        {
            throw new NotImplementedException();
        }

        public List<Usuario> GetAll()
        {
            throw new NotImplementedException();
        }

        public Usuario Login(Login login)
        {
            return _usuarioRepository.Login(login);
        }

        public Usuario Update(Usuario a)
        {
            throw new NotImplementedException();
        }

        public Usuario FindByEmail(string email)
        {
            return _usuarioRepository.FindByEmail(email);
        }

        public Usuario BuildUsuario(Usuario usuario, Registro registro)
        {
            return _usuarioRepository.BuildUsuario(usuario, registro);
        }

        public Usuario ActivateAccount(Usuario user,CodigoDeActivacion cda)
        {
            return _usuarioRepository.ActivateAccount(user,cda);
        }
    }
}