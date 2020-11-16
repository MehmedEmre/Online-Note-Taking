using MyEvernote.Common;
using MyEvernote.Entities;
using MyEvernote.WebApp.CurrentSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyEvernote.WebApp.init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentUserName()
        {
           if(CSession.User!= null)
            {
                return CSession.User.Username;
            }

            return "System";


        }
    }
}