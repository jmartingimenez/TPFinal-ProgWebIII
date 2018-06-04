using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPFinalProgWebIII.Models.View;

namespace TPFinalProgWebIII.Models.Service
{
    public interface IUsuarioService
    {
        Usuario Login(Login login);
        Usuario FindByEmail(string email);
        Usuario BuildUsuario(Usuario usuario, Registro registro);
    }
}
