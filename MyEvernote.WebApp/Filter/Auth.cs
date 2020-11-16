using MyEvernote.WebApp.CurrentSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace MyEvernote.WebApp.Filter
{
    public class Auth : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if(CSession.User == null)
            {
                filterContext.Result = new RedirectResult("/Home/Login");
            }
         
        }
    }
}