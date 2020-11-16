using System;
using System.Collections.Generic;
using System.Linq;
using MyEvernote.Entities;
using MyEvernote.BusinessLayer.Abstract;


namespace MyEvernote.BusinessLayer
{
    public class CategoryManager : ManagerBase<Category>
    {


        /*
        //ilişkili veri silme

        CommentManager commentManager = new CommentManager();
        LikedManager likedManager = new LikedManager();
        NoteManager noteManager = new NoteManager();

        public override int Delete(Category obj)
        {
            foreach (Note note in obj.Notes.ToList())
            {
                foreach (Liked like in note.Likes.ToList())
                {
                    likedManager.Delete(like);
                }
                foreach (Comment comment in note.Comments.ToList())
                {
                    commentManager.Delete(comment);
                }
                noteManager.Delete(note);
            }

            return base.Delete(obj);
        }

        */

    }
}
