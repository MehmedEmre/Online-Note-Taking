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
using MyEvernote.WebApp.CurrentSession;
using MyEvernote.WebApp.Filter;

namespace MyEvernote.WebApp.Controllers
{
    [Except]
    public class NoteController : Controller
    {

        private NoteManager noteManager = new NoteManager();
        private CategoryManager categoryManager = new CategoryManager();
        private LikedManager likedManager = new LikedManager();
        private EvernoteUserManager userManager = new EvernoteUserManager();

        [Auth]
        // GET: Note
        public ActionResult Index()
        {

            var notes = noteManager.List().Where(x => x.Owner.ID == CSession.Get<EvernoteUser>("login").ID).OrderByDescending(x => x.ModifiedOn);

            return View(notes.ToList());
        }

        [Auth]
        // GET: Note/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = noteManager.Find(x => x.ID == id);

            if (note == null)
            {
                return HttpNotFound();
            }
          
            return View(note);
        }

        [Auth]
        // GET: Note/Create
        public ActionResult Create()
        {
            ViewBag.Categories = new SelectList(CacheHelpers.GetCategoriesFromCache(), "ID", "Title");

            return View();
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Note note, int CategoryID)
        {
            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");


            if (ModelState.IsValid)
            {
                Category cat = categoryManager.Find(x=>x.ID == CategoryID);
                note.Owner = CSession.User;

                note.Category = cat;

                noteManager.insert(note);
                return RedirectToAction("Index");
            }

            //Cacheleme yapılacak
            ViewBag.Categories = new SelectList(CacheHelpers.GetCategoriesFromCache(), "id", "Title");

            return View(note);
        }

        [Auth]
        // GET: Note/Edit/5
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Note note = noteManager.Find(x => x.ID == id);

            if (note == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = new SelectList(CacheHelpers.GetCategoriesFromCache(), "ID", "Title");

            return View(note);
        }

        [Auth]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Note note,int CategoryID)
        {

            ModelState.Remove("CreatedOn");
            ModelState.Remove("ModifiedOn");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                Note res = noteManager.Find(x => x.ID == note.ID);
                Category cat = categoryManager.Find(x => x.ID == CategoryID);

                res.isDraft = note.isDraft;
                res.Category = cat;
                res.Text = note.Text;
                res.Title = note.Title;

                noteManager.Update(res);

                return RedirectToAction("Index");
            }

            ViewBag.Categories = new SelectList(CacheHelpers.GetCategoriesFromCache(), "ID", "Title");

            return View(note);
        }

        [Auth]
        public ActionResult MyMostLikedNote()
        {

            EvernoteUser user = CSession.User;


           TempData["MyMostLikedNote"] = likedManager.ListQueryable().Where(x => x.LikedUser.ID == user.ID).Select(x => x.Note).ToList();

            return RedirectToAction("Index","Home");
        }

        [Auth]
        // GET: Note/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.ID == id);
            if (note == null)
            {
                return HttpNotFound();
            }
            return View(note);
        }
        [Auth]

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Note note = noteManager.Find(x => x.ID == id);
            noteManager.Delete(note);

            return RedirectToAction("Index");
        }

      
        [HttpPost]
        public ActionResult GetLiked(int[] ids)
        {
            List<int> likedNote = likedManager.List(x => x.LikedUser.ID == CSession.User.ID && ids.Contains(x.Note.ID)).Select(x => x.Note.ID).ToList();


            return Json(new { data = likedNote});
        }

        [Auth]
        public ActionResult SendLiked(int? nID,bool state)
        {
            Note note = null;
            EvernoteUser user = null;

            int? lID = CSession.User.ID;

            note = noteManager.Find(x => x.ID == nID);
            user = userManager.Find(x => x.ID == lID);

            int count = 0;

            if (nID != null  && lID!= null)
            {
        
                if (note != null && user != null)
                {

                    if (state)
                    {
                        Liked Liked = likedManager.Find(x => x.LikedUser.ID == lID && x.Note.ID == note.ID);

                        count = likedManager.Delete(Liked);

                        if (count > 0)
                        {
                            note.likeCount = note.likeCount - 1;
                            noteManager.Update(note);

                            return Json(new { status = true, likedCount = note.likeCount });
                        }

                    }
                    else
                    {
                        Liked liked = new Liked();
                      
                        liked.LikedUser = user;
                        liked.Note = note;

                        count = likedManager.insert(liked);

                        if (count > 0)
                        {
                            note.likeCount = note.likeCount + 1;
                            noteManager.Update(note);

                            return Json(new { status = true, likedCount = note.likeCount });
                        }

                    }

                    return  Json(new { status = false, text = "Beğendiğiniz Not Silinmiş Olabilir!" });
                }

                return Json(new { status = false, text = "Beğendiğiniz Not Silinmiş Olabilir!" });

            }

            return Json(new { status = false, text = "Beğendiğiniz Not Silinmiş Olabilir!" });
        }


        
        public ActionResult DescriptionAndTitle(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Note note = noteManager.Find(x => x.ID == id);

            if (note == null)
            {
                return HttpNotFound();
            }


            return PartialView("_PartialDescriptionAndTitle", note);
        }

    }
}
