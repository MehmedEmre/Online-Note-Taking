using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Filter
{
    public class Except : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            
            //filterContext.Exception  Bize oluşan hatayı verir

            filterContext.ExceptionHandled = true;//Hatayı biz yöneteceğimizi belirtiyoruz

            filterContext.Controller.TempData["LastError"] = filterContext.Exception;

            filterContext.Result = new RedirectResult("/Home/HasError");

        }
    }
}