using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TPFinalProgWebIII.Models.Repository;
using TPFinalProgWebIII.Models.Service;

namespace TPFinalProgWebIII.Models.ServiceImp
{
    public class GeneralServiceImp<T> : IGeneralService<T> where T : class
    {
        private IGeneralRepository<T> _generalRepository;

        public GeneralServiceImp(IGeneralRepository<T> generalRepository)
        {
            this._generalRepository = generalRepository;
        }

        public T Create(T a)
        {
            return _generalRepository.Create(a);
        }

     

        public T Get<TKey>(TKey id)
        {
            return _generalRepository.Get(id);
        }

        public List<T> GetAll()
        {
            return _generalRepository.GetAll();
        }

        public T Update(T a)
        {
            return _generalRepository.Update(a);
        }
    }
}