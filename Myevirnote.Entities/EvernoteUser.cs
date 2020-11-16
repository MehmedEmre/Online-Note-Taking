using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myevirnote.Entities
{
    [Table("EvernoteUsers")]
    public class EvernoteUser:MyEntityBase
    {
        [StringLength(30)]
        public string Name { set; get; }
        [StringLength(30)]
        public string Surname { set; get; }
        [Required,StringLength(30)]
        public string Username { set; get; }
        [Required, StringLength(70)]
        public string Email { set; get; }
        [Required, StringLength(30)]
        public string Password { set; get; }
        [Required]
        public Guid ActivateGuid { set; get; }
        public bool isActive { set; get; }
        public bool isAdmin { set; get; }
        //int ve bool değerlerin varsayılan halleri boş geçilemezdir


        public virtual List<Note> Notes { set; get; }
        public virtual List<Comment> Comments { set; get; }
        public virtual List<Liked> Likes { set; get; }
    }
}
