using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myevirnote.Entities
{
    [Table("Notes")]
    public class Note:MyEntityBase{

        [Required,StringLength(60)]
        public string Title { get; set; }
        [Required, StringLength(2000)]
        public string Text { set; get; }
        public bool isDraft { set; get; }
        public int likeCount { get; set; }
        public virtual Category Category { set; get;}
        public virtual EvernoteUser Owner { set; get;}
        public virtual List<Comment> Comments { set; get;}
        public virtual List<Liked> Likes { set; get; }
        //Foreign Key
        public int CategoryID { set; get; }
    }
}
