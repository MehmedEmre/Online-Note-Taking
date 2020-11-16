using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Likes")]
    public class Liked
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { set; get; }
        public Note Note { set; get; }
        public  EvernoteUser LikedUser { set; get; }

    }
}
