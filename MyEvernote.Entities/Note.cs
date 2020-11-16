using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Notes")]
    public class Note:MyEntityBase{

        [Required,StringLength(60)]
        [DisplayName("Başlık")]
        public string Title { get; set; }

        [Required, StringLength(2000)]
        [DisplayName("İçerik")]
        public string Text { set; get; }


        [DisplayName("Taslak")]
        public bool isDraft { set; get; }

        [DisplayName("Like")]
        public int likeCount { get; set; }

        [DisplayName("Kategori")]
        public virtual Category Category { set; get;}

        public virtual EvernoteUser Owner { set; get;}
        public virtual List<Comment> Comments { set; get;}
        public virtual List<Liked> Likes { set; get; }

        public Note()
        {
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }
    }
}
