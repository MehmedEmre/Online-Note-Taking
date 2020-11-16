using MyEvernote.Core.DataAccess;
using MyEvernote.DataAccsessLayer.EntitiyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.BusinessLayer.Abstract
{
    //Esas iş yine DataAccesslayer'da yapılır
    public abstract class ManagerBase<T> : IDataAccess<T> where T : class
    {
        Repository<T> repo = new Repository<T>();

        public  int Delete(T obj)
        {
            return repo.Delete(obj);
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            return repo.Find(where);
        }

        public int insert(T obj)
        {
            return repo.insert(obj);
        }

        public List<T> List()
        {
            return repo.List();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            return repo.List(where);
        }

        public IQueryable<T> ListQueryable()
        {
            return repo.ListQueryable();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public int Update(T obj)
        {
            return repo.Update(obj);
        }
    }
}
