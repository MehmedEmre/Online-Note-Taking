using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.DataAccsessLayer;
using MyEvernote.Core.DataAccess;
namespace MyEvernote.DataAccsessLayer.MySQL
{
    //T tipi class olmak zorundadır dedik. Böylece int gibi tipler verilemez.
    public class Repository<T> : RepositoryBase,IDataAccess<T> where T:class
    {
        public List<T> List()
        {
            throw new NotImplementedException();
        }

        public List<T> List(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public T Find(Expression<Func<T, bool>> where)
        {
            throw new NotImplementedException();
        }

        public int insert(T obj)
        {
            throw new NotImplementedException();
        }

        public int Update(T obj)
        {
            throw new NotImplementedException();
        }

        public int Delete(T obj)
        {
            throw new NotImplementedException();
        }

        public int Save()
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> ListQueryable()
        {
            throw new NotImplementedException();
        }
    }
}
