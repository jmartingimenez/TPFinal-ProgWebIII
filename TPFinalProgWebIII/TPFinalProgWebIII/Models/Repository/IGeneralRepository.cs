using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPFinalProgWebIII.Models.Repository
{
    public interface IGeneralRepository<T>
    {
        T Create(T a);

        T Update(T a);

        void Delete(T a);

        List<T> GetAll();

        T Get<TKey>(TKey id);
    }
}
