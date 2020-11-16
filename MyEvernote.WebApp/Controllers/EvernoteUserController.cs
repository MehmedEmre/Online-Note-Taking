using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.BusinessLayer.Results;
using MyEvernote.Entities;
using MyEvernote.WebApp.Filter;
using WebGrease.Css.Extensions;

namespace MyEvernote.WebApp.Controllers
{

    [Admin,Except]
    public class EvernoteUserController : Controller
    {
        private EvernoteUserManager evernoteUserManager = new EvernoteUserManager();

        // GET: EvernoteUser
        public ActionResult Index()
        {
            return View(evernoteUserManager.List());
        }

        // GET: EvernoteUser/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EvernoteUser evernoteUser = evernoteUserManager.Find(x=>x.ID == id);

            if (evernoteUser == null)
            {
                return HttpNotFound();
            }
            return View(evernoteUser);
        }

        [Auth]
        // GET: EvernoteUser/Create
        public ActionResult Create()
        {
            return View();
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EvernoteUser evernoteUser)
        {

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {

                BuisnessLayerResult<EvernoteUser> res = evernoteUserManager.insert(evernoteUser);

                if(res.Errors.Count > 0)
                {

                    res.Errors.ForEach(x => ModelState.AddModelError("",x.Message));
                    return View(evernoteUser);
                }

            }
            return RedirectToAction("Index");
          
        }

        [Auth]
        // GET: EvernoteUser/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EvernoteUser evernoteUser = evernoteUserManager.Find(x => x.ID == id);

            if (evernoteUser == null)
            {
                return HttpNotFound();
            }
            return View(evernoteUser);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EvernoteUser evernoteUser)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                BuisnessLayerResult<EvernoteUser> res = evernoteUserManager.Update(evernoteUser);

               if(res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("",x.Message));
                }



                return RedirectToAction("Index");
            }
            return View(evernoteUser);
        }

     
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EvernoteUser evernoteUser = evernoteUserManager.Find(x => x.ID == id);

            if (evernoteUser == null)
            {
                return HttpNotFound();
            }
            return View(evernoteUser);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvernoteUser evernoteUser = evernoteUserManager.Find(x => x.ID == id);

            evernoteUserManager.Delete(evernoteUser);

            return RedirectToAction("Index");
        }

    }
}
