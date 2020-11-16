using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyEvernote.Entities;

namespace MyEvernote.DataAccsessLayer
{
    class MyInitializier:CreateDatabaseIfNotExists<DataBaseContext>
    {
        protected override void Seed(DataBaseContext context)
        {
            EvernoteUser admin = new EvernoteUser()
            {
                Name = "Mehmed Emre",
                Surname = "AKDIN",
                Email = "mehmedemree1461@hotmail.com",
                ProfileImageFileName = "admin.png",
                ActivateGuid = Guid.NewGuid(),
                isActive = true,
                isAdmin = true,
                Username = "mehmedemre",
                Password = "12345",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "mehmedemre",
                Description = "Merhaba, ben bu sitenin adminiyim. Umarım iyi bir gün geçirirsiniz. Hayvanlar. Hayvanoğlu hayvanlar.",
                Job = "Back-End Programmer",
                Country = "Turkey",
                DateOfBirth = DateTime.Now,
                myColor = "blue",
                isBanned = false
            };

            EvernoteUser standartUser = new EvernoteUser()
            {
                Name = "Tarık",
                Surname = "Akyüz",
                Email = "tarik@hotmail.com",
                ActivateGuid = Guid.NewGuid(),
                ProfileImageFileName = "standartUser.png",
                isActive = true,
                isAdmin = false,
                Username = "tarik",
                Password = "12345",
                CreatedOn = DateTime.Now,
                ModifiedOn = DateTime.Now.AddMinutes(5),
                ModifiedUserName = "tarik",
                Description = FakeData.TextData.GetSentences(3),
                Job = FakeData.NameData.GetCompanyName(),
                Country = FakeData.PlaceData.GetCounty(),
                DateOfBirth = DateTime.Now,
                myColor = "blue",
                isBanned = false
            };

            context.EverNoteUserT.Add(admin);
            context.EverNoteUserT.Add(standartUser);

            for (int i = 0; i < 10; i++)
            {
                EvernoteUser user = new EvernoteUser();

                user.Name = FakeData.NameData.GetFirstName();
                user.Surname = FakeData.NameData.GetSurname();
                user.Email = FakeData.NetworkData.GetEmail();
                user.ActivateGuid = Guid.NewGuid();
                user.ProfileImageFileName = "standartUser.png";
                user.isActive = true;
                user.isAdmin = false;
                user.Username = $"user{i}";
                user.Password = "12345";
                user.CreatedOn = DateTime.Now;
                user.ModifiedOn = DateTime.Now.AddMinutes(5);
                user.ModifiedUserName = $"user{i}";
                user.Description = FakeData.TextData.GetSentences(3);
                user.Job = FakeData.NameData.GetCompanyName();
                user.Country = FakeData.PlaceData.GetCounty();
                user.DateOfBirth = DateTime.Now;
                user.myColor = "blue";
                user.isBanned = false;


                context.EverNoteUserT.Add(user);

            }

          
                context.SaveChanges();
    


            List<EvernoteUser> MyList = context.EverNoteUserT.ToList();

            //FakeCategory
            for (int i = 0; i < 10; i++)
            {
                Category cat = new Category()
                {
                    Title = FakeData.PlaceData.GetStreetName(),
                    Description = FakeData.PlaceData.GetAddress(),
                    CreatedOn = DateTime.Now,
                    ModifiedOn = DateTime.Now,
                    ModifiedUserName = "mehmedemre",
                    Notes = new List<Note>()
                };

                context.CategorysT.Add(cat);


                //Adding Notes
                for (int j = 0; j < MyList.Count; j++)
                {

                    Note note = new Note()
                    {
                        Title = FakeData.TextData.GetAlphabetical(4),
                        Text = FakeData.TextData.GetSentences(3),
                        ModifiedOn = DateTime.Now,
                        ModifiedUserName = MyList[j].Username,
                        CreatedOn = DateTime.Now.AddDays(j),
                        likeCount = FakeData.NumberData.GetNumber(1,9),
                        Category = cat,
                        Owner = MyList[j],
                        Comments = new List<Comment>(),
                        Likes = new List<Liked>()
                    };

                    cat.Notes.Add(note);

                    for (int k = 0; k < FakeData.NumberData.GetNumber(3, 5); k++)
                    {
                        Comment comment = new Comment()
                        {
                            Text = FakeData.TextData.GetSentence(),
                            CreatedOn = DateTime.Now,
                            ModifiedOn = DateTime.Now,
                            ModifiedUserName = MyList[k].Username,
                            Note = note,
                            Owner = MyList[k],
                        };

                        note.Comments.Add(comment);
                    }

                    for (int k = 0; k< note.likeCount; k++)
                    {
                       
                        Liked liked = new Liked()
                        {
                            LikedUser = MyList[k],
                            Note = note,
                        };
                        note.Likes.Add(liked);

                    }


                }

            }

            context.SaveChanges();
        }

    }
}
