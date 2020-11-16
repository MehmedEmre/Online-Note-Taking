using MyEvernote.Entities.Message;
using MyEvernote.WebApp.CurrentSession;
using MyEvernote.WebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Filter
{
    public class Admin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {

            if (CSession.User != null && !CSession.User.isAdmin)
            {
                filterContext.Result = new RedirectResult("/Home/AcccessDenied");
            }

        }
    }
}