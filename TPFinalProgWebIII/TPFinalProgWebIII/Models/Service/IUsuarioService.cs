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
        Boolean Login(Login login);
    }
}
