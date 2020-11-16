using MyEvernote.BusinessLayer;
using MyEvernote.Entities;
using MyEvernote.WebApp.CurrentSession;
using MyEvernote.WebApp.Filter;
using MyEvernote.WebApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyEvernote.WebApp.Controllers
{
    [Except]
    [Auth]
    public class CommentController : Controller
    {

        private NoteManager noteManager = new NoteManager();
        private CommentManager commentManager = new CommentManager();


        // GET: Comment
        public ActionResult ShowNoteComments(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Note note = noteManager.Find(x=> x.ID == id);
            // note.Comments  ---> //Veritabanına tekrar sorgu atılarak comment'ler  çekilir

            Note note = noteManager.ListQueryable().Include("Comments").FirstOrDefault(x => x.ID == id);


            if (note == null)
            {

                return HttpNotFound();
            }
            
            
            return PartialView("_PartialComment",note.Comments);//Veritabanına tekrar sorgu atılmaz çünkü üstte notlar çekilirken commentler çekilmiştir
        }


        public ActionResult Edit(int? id,string text)
        {

            Comment comment = null;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.NotFound);
            }

           comment = commentManager.Find(x => x.ID == id);

            if (comment == null)
            {
                return Json(new { data = true, text = "Böyle Bir Yorum Bulunamadı!" }, JsonRequestBehavior.AllowGet);
            }

            comment.Text = text;

            int count = commentManager.Update(comment);

            if(count == 0)
            {
                return Json(new { data = true, text = "Güncelleme İşlemi Başarısız Oldu!" },JsonRequestBehavior.AllowGet);
            }


            return Json(new { data = true , text ="Güncelleme İşlemi Başarılı Oldu!"},JsonRequestBehavior.AllowGet);
        }


        public ActionResult Delete(int? id)
        {
            Comment comment = null;

            if(id == null)
            {

                return Json(new { data = false, text = "Böyle Bir Yorum Bulunamadı ! " }, JsonRequestBehavior.AllowGet);

            }

           comment = commentManager.Find(x => x.ID == id);

            int count = commentManager.Delete(comment);

            if(count == 0)
            {
                return Json(new { data = false, text = "Silme İşlemi Sırasında Bir Hata Gerçekleşti ! " }, JsonRequestBehavior.AllowGet);
            }


            return Json(new { data = true, text = "Silme İşlemi Başarılı! " },JsonRequestBehavior.AllowGet);
        }


        public ActionResult writeComment(Comment comment, int? noteID)
        {

            if(noteID == null)
            {
                return Json(new { data = false, text = "Yorum Yaptığınız Not Silinmiş Olabilir !" });
            }

            Note note = noteManager.Find(x => x.ID == noteID);

            if (note == null)
            {

                return Json(new { data = false, text = "Yorum Yaptığınız Not Silinmiş Olabilir !" });
            }


            comment.Note = note;
            comment.Owner = CSession.User;

            int count =  commentManager.insert(comment);

            if(count == 0)
            {
                return Json(new { data = false, text = "Yorum İşlemi Esnasında Bir Hata Meydana Geldi !" });
            }

           return Json(new { data = true, text = "Yorum İşlemi Başarılı!" });


        }

    }
}