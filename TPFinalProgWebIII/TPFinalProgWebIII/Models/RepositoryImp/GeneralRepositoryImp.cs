using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using TPFinalProgWebIII.Models.Repository;

namespace TPFinalProgWebIII.Models.RepositoryImp
{
    public class GeneralRepositoryImp<T> : IGeneralRepository<T> where T : class
    {
        private PW3TP_20181C_TareasEntities db = new PW3TP_20181C_TareasEntities();

        public T Create(T a)
        {
            T entity = db.Set<T>().Add(a);

            db.SaveChanges();

            return entity;
        }


        //HAY QUE CAMBIAR IMPLEMENTACION ES SOLO UNA PRUEBA
        public T Get<TId>(TId id)
        {
            return db.Set<T>().Find(id);
        }

        public List<T> GetAll()
        {
            //CONSIGO LA LISTA DE ENTIDADES A UN FORMA QUE LUEGO PUEDA CASTEAR EN LIST
            IQueryable<T> iq = db.Set<T>();

            //CASTEO A LIST Y DEVUELVO
            return iq.ToList<T>();
        }

        public T Update(T a)
        {
            db.SaveChanges();

            return a;
        }
    }
}