using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;


namespace MyEvernote.Common.Helper
{
    //https://koddefteri.net/c-sharp/c-sharp-ile-mail-gonderme.html

    public class EmailHelper
    {
        public static bool SendMail(string link,string to,bool isHtml = true)
        {

            bool result = false;
            string htmlBody = string.Empty;
            try
            {

                StreamReader reader = new StreamReader("C:/Users/mehme/source/repos/MyEvernoteSolution/MyEvernote.WebApp/Views/Email/Email.cshtml");

                htmlBody = reader.ReadToEnd();
                htmlBody = htmlBody.Replace("{link}", link);


                SmtpClient smtp = new SmtpClient();

                smtp.Host = ConfigHelper.Get<String>("MailHost");
                smtp.Port = ConfigHelper.Get<int>("MailPort");
                smtp.EnableSsl = true;
                //Kimin yetkileriyle mail göndereceksek onun kullanıcı adı ve şifresi
                smtp.Credentials = new NetworkCredential(
                    ConfigHelper.Get<string>("MailUser"),
                    ConfigHelper.Get<string>("MailPass"));



                MailMessage post = new MailMessage();

                //Gönderdiğimiz mailde görünecek olan mail adresi ve ismi tutar.
                post.From = new MailAddress(ConfigHelper.Get<String>("MailUser"),"MyEvernote.com");
                //Alıcı adreslerini tutar, Add()  fonksiyonu ile kullanılır, strin türü değer alır. Eğer birden fazla mail adresi eklenecek ise, adresler arasında virgül kullanılır.
                post.To.Add(to);
                post.Subject = "Myevernote";
                post.IsBodyHtml = isHtml;



                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(htmlBody, null,"text/html");

                post.AlternateViews.Add(htmlView);



                smtp.Send(post);

                result = true;
            }
            catch (Exception e)
            {
                return result;
            }

            return result;
        }


    }
}
