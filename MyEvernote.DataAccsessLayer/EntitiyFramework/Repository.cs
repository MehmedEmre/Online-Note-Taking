using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Core.DataAccess;
using MyEvernote.Common;
using MyEvernote.Entities;

namespace MyEvernote.DataAccsessLayer.EntitiyFramework
{
    //T tipi class olmak zorundadır dedik. Böylece int gibi tipler verilemez.
    public class Repository<T> : RepositoryBase,IDataAccess<T> where T:class
    {
        private DbSet<T> ObjectSet;//private ObjectSet<T> ObjectSet;

        public Repository()
        {
          
            //Set<T> verilen entitiy type için DbSet'i döndürür
            ObjectSet = db.Set<T>();//Buraya verilen obje tipine ait kümeyi her obje için bir kere
                                    //çağırırız. Ve fonksiyonlarda bunu kullanılırız. Bu şekilde yapmasaydık
                                    //her bir fonksiyon için birden fazla db.Set<T> çağırımı olacaktı.
        }

        public List<T> List()
        {
           
            return ObjectSet.ToList();
        }


        //parametre olarak T alıp bool döndüren bir expression verilir
        public List<T> List(Expression<Func<T, bool>> where)
        {
            return ObjectSet.Where(where).ToList();
        }

        public IQueryable<T> ListQueryable()
        {
            return ObjectSet.AsQueryable<T>();
        }


        #region Not

        /*
        ToList() yapıldığında database'e sorgu atılmış olur. Bu fonksiyondan
        dönen değere orderby veya ilk 10 kayıt gibi işlemler veri tabanı üstünde yaptırılmak
        isteniyorsa.
         
         public IQueryable<T> List(Expression<Func<T, bool>> where)
        {
            return ObjectSet.Where(where);
        }
         Kullanıcı bu fonksiyonu  kullandıktan sonra toList() diyerek database'e
         sorgu atabilir.
         

         */

        #endregion

        //FirstOrDefault geriye tek bir değer döndürür. Bulamazsa null döndürür
        public T Find(Expression<Func<T,bool>> where)
        {

            return ObjectSet.FirstOrDefault(where);
        }

        public int insert(T  obj)
        {
            ObjectSet.Add(obj);

            if(obj is MyEntityBase)//obj MyEntityBase ise. Veya ondan türüyorsa
            {
                //obj'nin türü belli değildir.Açık casting gerektirir.
                MyEntityBase myObj = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                myObj.CreatedOn = now;
                myObj.ModifiedOn = now;
                myObj.ModifiedUserName = App.Common.GetCurrentUserName();

            }
           
            return Save();
        }

        public int Update(T obj)//Entitiyframeworkte propertylerde değişiklik yapıldığında
                                 // zaten otomatik update yapar.
        {
            if(obj is MyEntityBase)
            {
                MyEntityBase myObj = obj as MyEntityBase;
                DateTime now = DateTime.Now;

                myObj.ModifiedOn = now;
                myObj.ModifiedUserName = App.Common.GetCurrentUserName();

            }


            return Save();
        }

        public int Delete(T obj)
        {
            ObjectSet.Remove(obj);
            return Save();
        }

        public int Save()//Kaç kayıt  eklendiyse onun sayısını geriye döndürür
        {
            return db.SaveChanges();
        }




    }
}
