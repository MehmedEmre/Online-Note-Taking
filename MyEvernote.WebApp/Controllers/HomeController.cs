using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;
using MyEvernote.Entities.Message;
using MyEvernote.BusinessLayer.Results;
using MyEvernote.WebApp.ViewModel;
using Newtonsoft.Json;
using MyEvernote.WebApp.CurrentSession;
using MyEvernote.WebApp.CacheHelper;
using MyEvernote.WebApp.Filter;

namespace MyEvernote.WebApp.Controllers
{
    
    [Except]
    public class HomeController : Controller
    {

        private EvernoteUserManager evernoteUserManager = new EvernoteUserManager();
        private ErrorViewModel errorObject = new ErrorViewModel();
        private OkViewModel OkObject = new OkViewModel();
        private CategoryManager cm = new CategoryManager();
        private NoteManager note = new NoteManager();

        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }

        public PartialViewResult GetCategoryPartial()
        {
            
            List<Category> list = CacheHelpers.GetCategoriesFromCache();


            return PartialView("_CategoriesPage1", list);
        }

        public PartialViewResult GetCategorysNote()
        {
            //ListQueryable() denildiğinde Queryable listesi geriye döner ve bu liste üzerinde order by
            //descending çalışır. Ve buda Queryable sorgusu içine yerleşir. Ve ne zaman toList denirse
            // OrderByDEscending'li sorgu veritabanı üstünde çalıştırılır.
            // List<Note> List = note.ListQueryable().OrderByDescending(x => x.ModifiedOn).ToList();

            //Burada ise veriyi sql'den toList ile çekip. Orderby işlemi C# listesi üzerine yapılır.
            List<Note> List = note.List().Where(x => x.isDraft == false).OrderByDescending(x => x.ModifiedOn).ToList();

            //Orderby küçükten büyüğe
            //Orderbydescending Büyükten küçüğe

            if (TempData["CategorysNote"] != null)//Boş değilse yönlendirilmiş istektir.
            {
                return PartialView("_CardPartial", TempData["CategorysNote"] as List<Note>);
            }

            if(TempData["MostLiked"] != null)
            {
                return PartialView("_CardPartial", TempData["MostLiked"] as List<Note>);
            }

            if(TempData["MyMostLikedNote"] != null)
            {
                return PartialView("_CardPartial", TempData["MyMostLikedNote"] as List<Note>);
            }


                return PartialView("_CardPartial", List);
        }


        public ActionResult MostLiked()
        {

            NoteManager nm = new NoteManager();

            TempData["MostLiked"] = nm.ListQueryable().Where(x=>x.isDraft == false).OrderByDescending(x => x.likeCount).ToList();

            return View("Index");
        }

        public ActionResult About()
        {

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
        
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                BuisnessLayerResult<EvernoteUser> user = evernoteUserManager.Loginuser(model);

                if (user.Errors.Count > 0)
                {

                    ErrorMessage err = user.Errors.Find(x => x.Code == ErrorMessageCode.UserisNotActive);

                    ErrorMessage isBanned = user.Errors.Find(x => x.Code == ErrorMessageCode.Banned);

                    if (err != null)
                    {
                        ViewBag.userIsNotActive = false;

                    }else if (isBanned != null)
                    {
                        errorObject.IsRedirecting = false;
                        errorObject.Header = "Hesabınız Admin Tarafından Askıya Alındı !";
                        errorObject.Title = "Hesabınız askıdan alınana kadar erişimiz kısıtlandı !";

                        return View("Banned",errorObject);
                    }

                    user.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    ViewBag.valid = false;
                    return View(model);
                }
                else
                {

                    CSession.Set("login", user.Result);
                    return RedirectToAction("Index", "Home");
                }
            }
        

            ViewBag.valid = false;


            return View(model);
        }


        public ActionResult Logout()
        {
            Session.Clear();
            return View("Index");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {

            if (ModelState.IsValid)
            {
                BuisnessLayerResult<EvernoteUser> res = evernoteUserManager.RegisterUser(model);

                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    ViewBag.valid = false;
                    return View(model);
                }


                OkObject.Title = "Kayıt Başarılı";
                OkObject.RedirectUrl = "/Home/Login";
                OkObject.Items.Add("Lütfen e-posta adresinize gönderdiğimiz aktivasyon link'ine tıklayarak hesabınızı aktive ediniz.");

                return View("OK", OkObject);
            }


            ViewBag.valid = false;
            return View(model);
        }

        [Auth]
        public ActionResult UserActivate(Guid id)
        {

            BuisnessLayerResult<EvernoteUser> res = evernoteUserManager.ActivateUser(id);

            if (res.Errors.Count > 0)
            {
                errorObject.Items = res.Errors;

                return RedirectToAction("Error", errorObject);
            }

            OkObject.Title = "Hesabınız Aktifleştirildi! Hemen Gir Ve Not Almaya Başla!";
            OkObject.RedirectUrl = "/Home/Login";

            return RedirectToAction("UserActivateOK", OkObject);
        }

        [Auth]
        [HttpGet]
        public ActionResult ShowProfile(int? id)
        {
            BuisnessLayerResult<EvernoteUser> res = evernoteUserManager.GetUserById(id.Value);


            ProfileViewModel model = new ProfileViewModel()
            {
                ID = res.Result.ID,
                Username = res.Result.Username,
                Name = res.Result.Name,
                Surname = res.Result.Surname,
                ProfileImageFileName = res.Result.ProfileImageFileName,
                Description = res.Result.Description,
                Job = res.Result.Job,
                Country = res.Result.Country,
                DateOfBirth = res.Result.DateOfBirth,
                isAdmin = res.Result.isAdmin,
                Email = res.Result.Email

            };

            if (res.Errors.Count > 0)
            {
                errorObject.Title = "Hata Oluştu! Hesabınız Silinmiş Olabilir!";
                errorObject.Items = res.Errors;

                return View("Error", errorObject);
            }


            return View(model);
          
        }

        [Auth]
        [HttpPost]
        public ActionResult UpdateProfile(ProfileViewModel model)
        {
            //Server-side validation yapılacak
            //Server-side validation yapılacak
            //Server-side validation yapılacak
            //Server-side validation yapılacak
            ModelState.Remove("Password");
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                BuisnessLayerResult<EvernoteUser> res = evernoteUserManager.UpdatePersonProfile(model);

                if (res.Errors.Count > 0)
                {
                    errorObject.Title = "Güncelleme Yapılırken Bir Hata Oluştu! Hesabınız Engellenmiş Veya Silinmiş Olabilir!";
                    errorObject.Items = res.Errors;
                    return View("Error", errorObject);
                }


                CSession.Set("login", res.Result);

                ProfileViewModel ProfileModel = new ProfileViewModel()
                {
                    ID = res.Result.ID,
                    Username = res.Result.Username,
                    Name = res.Result.Name,
                    Surname = res.Result.Surname,
                    ProfileImageFileName = res.Result.ProfileImageFileName,
                    Description = res.Result.Description,
                    Job = res.Result.Job,
                    Country = res.Result.Country,
                    DateOfBirth = res.Result.DateOfBirth,
                    isAdmin = res.Result.isAdmin,
                    Email = res.Result.Email

                };

                return new JsonResult { Data = JsonConvert.SerializeObject(ProfileModel) };
            }


            return View("ShowProfile",model);
        }

        [Auth]
        [HttpPost]
        public ActionResult UpdateProfileImage(EvernoteUser model,HttpPostedFileBase ProfileImage)
        {
          

            if(ProfileImage !=null && 
                    (ProfileImage.ContentType =="image/jpeg" ||
                    ProfileImage.ContentType == "image/jpg" ||
                    ProfileImage.ContentType == "image/png"))
            {

                string fileName = $"user_{model.ID}.{ProfileImage.ContentType.Split('/')[1]}";
               
                ProfileImage.SaveAs(Server.MapPath($"~/Images/{fileName}"));
                model.ProfileImageFileName = fileName;

                BuisnessLayerResult<EvernoteUser> res = evernoteUserManager.UpdatePersonImage(model);

                if (res.Errors.Count > 0)
                {
                    errorObject.Title = "Kullanıcı Bulunamadı! ";
                    errorObject.Items = res.Errors;

                    return View("Error",errorObject);
                }


                CSession.Set("login", res.Result);

                return RedirectToAction("ShowProfile",new { id =res.Result.ID});
            }
         
                errorObject.Title = "Yalnızca JPEG,JPG Ve PNG Formatında Yükleme Yapabilirsiniz! ";

                return View("Error", errorObject);

        }


        public ActionResult AcccessDenied()
        {

            ErrorMessage message = new ErrorMessage();

            message.Message = "Tehlikeli Erişim Kısıtlaması!";
            message.Code = ErrorMessageCode.AccessRestriction;

            ErrorViewModel error = new ErrorViewModel()
            {
                Title = "Hata!",
                Header = "Bu sayfaya erişiminiz bulunmamaktadır!",
                IsRedirecting = false

            };

            error.Items.Add(message);

            return View("Error", error);
        }

        public ActionResult HasError()
        {

            return View();
        }


    }
}