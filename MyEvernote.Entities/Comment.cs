using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("Comments")]
    public class Comment:MyEntityBase
    {

        [Required]
        public string Text { set; get; }
        public Note Note { set; get; }
        public EvernoteUser Owner { set; get; }


    }
}
