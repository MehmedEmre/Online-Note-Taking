using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;

namespace MyEvernote.WebApp.ViewModel
{
    public class NotifyViewModelBase<T>
    {
        public List<T> Items { set; get; }
        public string Header { set; get; }
        public string Title { set; get; }
        public bool IsRedirecting { set; get; }
        public string RedirectUrl { set; get; }
        public int RedirectingTimeOut { set; get; }


        public NotifyViewModelBase()
        {
            Header = "Yönlendiriliyorsunuz ....";
            Title = "Geçersiz İşlem!";
            IsRedirecting = true;
            RedirectUrl = "/Home/Index";
            RedirectingTimeOut = 10000;
            Items = new List<T>();
        }


    }
}