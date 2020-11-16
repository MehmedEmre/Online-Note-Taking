using MyEvernote.DataAccsessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.DataAccsessLayer.EntitiyFramework
{
    //Singleton pattern
    public class RepositoryBase
    {

        protected static DataBaseContext db;//Miras alan sınıfın erişmesi için protected yapıldı
        private static object _lockSync = new object();

        protected RepositoryBase()
        {
           CreateContext();
        }


        private static void  CreateContext()
        {
            if(db == null)
            {
                lock (_lockSync)//Multi thread uygulamalarında birden fazla thred'i önlemek
                                //için kullanılır ve bir obje parametresi olur.
                {
                    db = new DataBaseContext();
                }
            }

        }

    }
}
