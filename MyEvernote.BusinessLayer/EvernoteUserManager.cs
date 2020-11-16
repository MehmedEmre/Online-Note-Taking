using MyEvernote.DataAccsessLayer.EntitiyFramework;
using MyEvernote.Entities;
using MyEvernote.Entities.ValueObjects;
using System;
using MyEvernote.BusinessLayer.Results;
using MyEvernote.Entities.Message;
using MyEvernote.Common.Helper;
using MyEvernote.Core.DataAccess;
using MyEvernote.BusinessLayer.Abstract;
using System.Linq;
using System.Collections.Generic;

namespace MyEvernote.BusinessLayer
{
    public class EvernoteUserManager:ManagerBase<EvernoteUser>
    {

        private BuisnessLayerResult<EvernoteUser> layerResult = new BuisnessLayerResult<EvernoteUser>();


        public BuisnessLayerResult<EvernoteUser> RegisterUser(RegisterViewModel data)
        {
          
            EvernoteUser user =  Find(x => x.Username == data.Username || x.Email == data.Email);
           
       
            if(user != null)
            {
                if(user.Username == data.Username)
                {
                    layerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı kayıtlı. Lütfen Başka bir kullanıcı adı deneyin!");

                }
                if(user.Email == data.Email)
                {
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu email kayıtlı. Lütfen Başka bir email deneyin!");
                }
            }
            else
            {
               int dbResult =  base.insert(new EvernoteUser { 
                               Username = data.Username,
                               Email = data.Email,
                               Password = data.Password,
                               ActivateGuid = Guid.NewGuid(),
                               ProfileImageFileName = "standartUser.png",
                               DateOfBirth=DateTime.Now,
                               isActive = false,
                               isAdmin = false,
                               isBanned = false,
                               myColor = data.myColor

                               });

                if (dbResult > 0)
                {
                   layerResult.Result = Find(x => x.Email == data.Email && x.Username == data.Username );

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");

                    string activateUri = $"{siteUri}/Home/ActivateUser/{layerResult.Result.ActivateGuid}";

                    EmailHelper.SendMail(activateUri, layerResult.Result.Email);
                }
            }


            return layerResult;
        }

        
        public BuisnessLayerResult<EvernoteUser> Loginuser(LoginViewModel data)
        {
            EvernoteUser user = Find(x => x.Username == data.Username && x.Password == data.Password);

            layerResult.Result = user;

            if (user != null)
            {
                if (!user.isActive)
                {
                    layerResult.AddError(ErrorMessageCode.UserisNotActive, "Aktivasyon kodunu henüz aktif etmedin.");
                    layerResult.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-mail'inizi kontrol edin");
                }else if (user.isBanned) {

                    layerResult.AddError(ErrorMessageCode.Banned, "Admin Tarafından Siteye Erişiminiz Kısıtlandı!");
                }

            }
            else
            {
                layerResult.AddError(ErrorMessageCode.UserNameOrPassWrong, "Kullanıcı adı veya şifre uyuşmuyor");
            }

            return layerResult;
        }


        public BuisnessLayerResult<EvernoteUser> ActivateUser(Guid guid)
        {

            layerResult.Result = Find(x => x.ActivateGuid == guid);

            if(layerResult.Result != null)
            {
                if (layerResult.Result.isActive)
                {
                    layerResult.AddError(ErrorMessageCode.UserAlreadyActive, "Bu kullanıcı zaten aktif edilmiştir!");

                    return layerResult;
                }

                layerResult.Result.isActive = true;

                Update(layerResult.Result);
            }
            else
            {
                layerResult.AddError(ErrorMessageCode.ActivateIdDoesNotExists,"Aktifleştirilecek Kullanıcı Bulunamadı!");
            }

            return layerResult;
        }

        public BuisnessLayerResult<EvernoteUser> GetUserById(int id)
        {
            layerResult.Result = Find(x => x.ID == id);

            if(layerResult.Result == null)
            {
                layerResult.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı");

                return layerResult;
            }

            return layerResult;
        }

        public BuisnessLayerResult<EvernoteUser> UpdatePersonProfile(ProfileViewModel model)
        {
            BuisnessLayerResult<EvernoteUser> res = new BuisnessLayerResult<EvernoteUser>();

            EvernoteUser user = Find(x => model.ID == x.ID);

            int count = 0;

            if (user == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound,"Kullanıcı Bulunamadı");

                return res;
            }
           
             EvernoteUser resUser = Find(x => model.Username == x.Username);

            if(resUser == null || resUser.ID == model.ID)
            {

                    user.Username = model.Username;
                    user.Name = model.Name;
                    user.Surname = model.Surname;
                    user.Country = model.Country;
                    user.DateOfBirth = model.DateOfBirth;
                    user.Description = model.Description;
                    user.Job = model.Job;
                    count = base.Update(user);
            }
            else
             {
               res.AddError(ErrorMessageCode.UserAlreadyExists, "Bu Kullanıcı Adı Zaten Mevcut!");

               return res;
            }

            if (count > 0)
            {
                res.Result = user;
            }
            else
            {
                res.AddError(ErrorMessageCode.ErrorOccurredDuringUpdate, "Güncelleme Yapılırken Bir Hata Meydana Geldi!");
            }


            return res;
        }


        public BuisnessLayerResult<EvernoteUser> UpdatePersonImage(EvernoteUser model)
        {
            BuisnessLayerResult<EvernoteUser> res = new BuisnessLayerResult<EvernoteUser>();

            EvernoteUser user= Find(x=> x.ID == model.ID);
            

            if (user == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı");
            }
            else
            {
                user.ProfileImageFileName = model.ProfileImageFileName;
                int count = base.Update(user);

                if(count > 0)
                {
                    res.Result = user;
                }
                else
                {
                    res.AddError(ErrorMessageCode.ErrorOccurredDuringUpdate, "Güncelleme Yapılırken Bir Hata Meydana Geldi!");
                }
            }

            return res;
        }


        //new anahtar kelimesi kullandık. Çünkü Hem üst sınıfta hemde bu sınıfta insert adında aynı ismi taşıyan iki metod var. Ve biz bu class'dakini new kelimesi ile gizleyerek metod hiding yaptık.

        public new  BuisnessLayerResult<EvernoteUser> insert(EvernoteUser data)
        {

            EvernoteUser user = Find(x => x.Username == data.Username || x.Email == data.Email);


            if (user != null)
            {
                if (user.Username == data.Username)
                {
                    layerResult.AddError(ErrorMessageCode.UsernameAlreadyExists, "Bu kullanıcı adı kayıtlı. Lütfen Başka bir kullanıcı adı deneyin!");

                }
                if (user.Email == data.Email)
                {
                    layerResult.AddError(ErrorMessageCode.EmailAlreadyExists, "Bu email kayıtlı. Lütfen Başka bir email deneyin!");
                }
            }
            else
            {
                data.ActivateGuid = Guid.NewGuid();
                data.ProfileImageFileName = "standartUser.png";
                data.DateOfBirth = DateTime.Now;

                int dbResult = base.insert(data);

                if (dbResult == 0)
                {
                    layerResult.AddError(ErrorMessageCode.ErrorOccurredDuringInsert, "Ekleme Sırasında Bir Hata Meydana Geldi!");

                }
            }

            return layerResult;
        }



        public new BuisnessLayerResult<EvernoteUser> Update(EvernoteUser data)
        {
            BuisnessLayerResult<EvernoteUser> res = new BuisnessLayerResult<EvernoteUser>();

            EvernoteUser user = Find(x => data.ID == x.ID);

            int count = 0;

            if (user == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı Bulunamadı");

                return res;
            }
           

            EvernoteUser resUser = Find(x => data.Username == x.Username || data.Email == x.Email);


            if (resUser == null || resUser.ID == data.ID)
            {
                user.Username = data.Username;
                user.Name = data.Name;
                user.Surname = data.Surname;
                user.Country = data.Country;
                user.DateOfBirth = data.DateOfBirth;
                user.Description = data.Description;
                user.Job = data.Job;
                user.myColor = data.myColor;
                user.Password = data.Password;
                user.isActive = data.isActive;
                user.isAdmin = data.isAdmin;
                user.isBanned = data.isBanned;
                user.Email = data.Email;
                count = base.Update(user);

            }

            if (resUser.Username == data.Username)
            {
                res.AddError(ErrorMessageCode.UserAlreadyExists, "Bu Kullanıcı Adı Zaten Mevcut!");
            }

            if (resUser.Email == data.Email)
            {
                res.AddError(ErrorMessageCode.UserAlreadyExists, "Bu Mail Adresi Zaten Mevcut!");
            }
 

            if (count > 0)
            {
                res.Result = user;
            }
            else
            {
                res.AddError(ErrorMessageCode.ErrorOccurredDuringUpdate, "Güncelleme Yapılırken Bir Hata Meydana Geldi!");
            }


            return res;
        }

   


    }
}
