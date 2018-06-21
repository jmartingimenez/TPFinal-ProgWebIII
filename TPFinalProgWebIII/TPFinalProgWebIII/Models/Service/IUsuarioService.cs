using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPFinalProgWebIII.Models.Enum;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Models.Service
{
    public interface IUsuarioService
    {
        Usuario Login(Login login);
        Usuario FindByEmail(string email);
        Usuario BuildUsuario(Usuario usuario, Registro registro);
        Usuario ActivateAccount(CodigoDeActivacion cda);
        void CrearCarpetaGeneral(Usuario usuario);
        void SendKeyByMail(Usuario usuario);
        EstadoMail ComprobarEstadoMail(string email);
        Usuario RegistrarUsuarioConMailNuevo(Registro registro);
        Usuario RegistrarUsuarioConMailSinUso(Registro registro);
    }
}
