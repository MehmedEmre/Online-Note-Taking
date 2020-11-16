using MyEvernote.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.WebApp.CurrentSession
{
    public class CSession
    {

        public static EvernoteUser User
        {
            get
            {
                return Get<EvernoteUser>("login");
            }

        }

        public static void Set<T>(string key, T obj){

            HttpContext.Current.Session[key] = obj;

        }

        public static T Get<T>(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                return (T)HttpContext.Current.Session[key];
            }

            return default(T);
        }

        public static void RemoveSession(string key)
        {
            if (HttpContext.Current.Session[key] != null)
            {
                HttpContext.Current.Session.Remove(key);
            }
        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

    }
}