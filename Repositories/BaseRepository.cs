using DataAccess;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T>
        where T : class, new()
    {
        private BlackSoundEntities Context;

        public BaseRepository()
        {
            Context = new BlackSoundEntities();
        }

        public List<T> GetAll()
        {
            return Context.Set<T>().ToList();
        }

        public List<T> GetAll(Predicate<T> filter)
        {
            List<T> listDb = new List<T>();
            foreach (T item in Context.Set<T>().ToList())
            {
                if (filter(item))
                {
                    listDb.Add(item);
                }
            }

            return listDb;
        }

        public T GetById(int id)
        {
            return Context.Set<T>().Find(id);
        }
        public void Create(T item)
        {
            Context.Set<T>().Add(item);
            Context.SaveChanges();
        }
        public void Update(T item, Func<T, bool> findByIDPredicate)
        {
            var local = Context.Set<T>()
                         .Local
                         .FirstOrDefault(findByIDPredicate);
            if (local != null)
            {
                Context.Entry(local).State = EntityState.Detached;
            }
            Context.Entry(item).State = EntityState.Modified;
            Context.SaveChanges();
        }

        public bool DeleteByID(int id)
        {
            bool isDeleted = false;
            T dbItem = Context.Set<T>().Find(id);
            if (dbItem != null)
            {
                Context.Set<T>().Remove(dbItem);
                int recordsChanged = Context.SaveChanges();
                isDeleted = recordsChanged > 0;
            }
            return isDeleted;
        }

        public bool Delete(Func<T, bool> filter)
        {
            bool isDeleted = false;
            List<T> dbItems = Context.Set<T>().Where(filter).ToList();
            foreach (T dbItem in dbItems)
            {
                Context.Set<T>().Remove(dbItem);
            }
            int recordsChanged = Context.SaveChanges();
            isDeleted = recordsChanged > 0;
            return isDeleted;
        }

        public abstract void Save(T item);
    }
}
