using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myevirnote.Entities
{
    [Table("Comments")]
    public class Comment
    {
        [Required,StringLength(500)]
        public string Text { get; set; }
        public virtual Note Note { set; get; }
        public virtual EvernoteUser Owner { set; get; }


    }
}
