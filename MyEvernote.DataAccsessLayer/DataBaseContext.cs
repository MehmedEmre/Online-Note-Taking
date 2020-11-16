using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities;

//DataAccessLayer'a sağ tıkla -->add-->reference-->project-->Entity
//Artık References kısmında MyEvernote.Entities  görülebilir. Şimdi bunu using
//olarak ekleyebiliriz.

namespace MyEvernote.DataAccsessLayer
{
    public class DataBaseContext :DbContext
    {
        public DbSet<EvernoteUser> EverNoteUserT { set; get; }
        public DbSet<Note> NotesT { set; get; }
        public DbSet<Comment> CommentsT { set; get; }
        public DbSet<Category> CategorysT { set; get; }
        public DbSet<Liked> LikesT { set; get; }

        public DataBaseContext()
        {
            Database.SetInitializer(new MyInitializier());
        }

        /*

        //Fluent API Yöntemi
        //Data annotationdan farklı bir yöntem
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {


            modelBuilder.Entity<Category>()
                 .HasMany(n => n.Notes)
                 .WithRequired(c => c.Category)
                 .WillCascadeOnDelete(true);


            modelBuilder.Entity<EvernoteUser>()
                .HasMany(n => n.Notes)
                .WithRequired(c => c.Owner)
                .WillCascadeOnDelete(true);


            modelBuilder.Entity<Note>()
                .HasMany(n => n.Likes)
                .WithRequired(c => c.Note)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Note>()
             .HasMany(n => n.Comments)
             .WithRequired(c => c.Note)
             .WillCascadeOnDelete(true);


        }

        */

    }
}
