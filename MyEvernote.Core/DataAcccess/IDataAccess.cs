using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Core.DataAccess
{
    public interface IDataAccess<T>
    {
        public List<T> List();
        List<T> List(Expression<Func<T, bool>> where);
        IQueryable<T> ListQueryable();
        public T Find(Expression<Func<T, bool>> where);
        int insert(T obj);
        int Update(T obj);
        int Delete(T obj);
        int Save();
    }

}