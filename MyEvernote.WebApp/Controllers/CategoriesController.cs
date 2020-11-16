using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.WebApp.CacheHelper;
using MyEvernote.WebApp.Filter;

namespace MyEvernote.WebApp.Controllers
{

    [Except]
    public class CategoriesController : Controller
    {

        private CategoryManager categoryManager = new CategoryManager();


        public ActionResult CategorySelect(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category c = categoryManager.Find(x=>x.ID == id.Value);

            if (c == null)
            {
                return HttpNotFound();
            }

            TempData["CategorysNote"] = c.Notes.ToList().Where(x => x.isDraft == false).OrderByDescending(x => x.ModifiedOn).ToList();

            return RedirectToAction("Index", "Home");
        }

        [Admin]
        [Auth]
        public ActionResult Index()
        {
         
            return View(CacheHelpers.GetCategoriesFromCache());
        }

        [Admin]
        [Auth]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = categoryManager.Find(x=> x.ID == id.Value);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [Admin]
        [Auth]
        public ActionResult Create()
        {
            return View();
        }

        [Admin]
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            ModelState.Remove("Password");
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {
                categoryManager.insert(category);

                CacheHelpers.ClearCache("categories-cache");
                
                return RedirectToAction("Index");
            }
               
            

            return View(category);
        }

        [Admin]
        [Auth]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
            
            }
            Category category = categoryManager.Find(x => x.ID == id.Value);
            if (category == null)
            {
              
            }
            return View(category);
        }

        [Admin]
        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            ModelState.Remove("Password");
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUserName");

            if (ModelState.IsValid)
            {

                Category res = categoryManager.Find(x=>x.ID == category.ID);

                if(res != null)
                {
                    res.Title = category.Title;
                    res.Description = category.Description;
                    categoryManager.Update(res);
                    CacheHelpers.ClearCache("categories-cache");

                    return RedirectToAction("Index");
                }

            }
            return View(category);
        }

        [Admin]
        [Auth]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Category category = categoryManager.Find(x=>x.ID == id.Value);

            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [Admin]
        [Auth]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Category category = categoryManager.Find(x => x.ID == id);

            categoryManager.Delete(category);

            CacheHelpers.ClearCache("categories-cache");

            return RedirectToAction("Index");
        }

 
    }
}
