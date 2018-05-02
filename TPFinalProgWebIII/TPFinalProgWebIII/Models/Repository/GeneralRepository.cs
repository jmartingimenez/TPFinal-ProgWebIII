using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPFinalProgWebIII.Models.Entity;

namespace TPFinalProgWebIII.Models.Repository
{
    public interface IGeneralRepository<T>
    {
        T Crear(T a);

        T Modificar(T a);

        void Eliminar(T a);

        List<T> Listar();
        Usuario BuscarPorId(int id);
    }
}
