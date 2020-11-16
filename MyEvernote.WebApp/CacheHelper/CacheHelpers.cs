using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace MyEvernote.WebApp.CacheHelper
{
    public class CacheHelpers
    {


        public static List<Category> GetCategoriesFromCache()
        {

            var result = WebCache.Get("categories-cache");

            if(result == null){

                CategoryManager categoryManager = new CategoryManager();

                result = categoryManager.List();

                //Son paramatre sliding expiraiton true yapıldı yani cache'den her okumada süre sıfırlansın
                WebCache.Set("categories-cache", result, 20,true);
               
            }

            return result;
        }

        public static void ClearCache(string key)
        {
            WebCache.Remove(key);
        }


    }
}