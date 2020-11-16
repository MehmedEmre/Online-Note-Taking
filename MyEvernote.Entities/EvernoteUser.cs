using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities
{
    [Table("EvernoteUsers")]
    public class EvernoteUser:MyEntityBase
    {
        [StringLength(30),Required]
        public string Name { set; get; }

        [StringLength(30), Required]
        public string Surname { set; get; }

        [Required,StringLength(30)]
        public string Username { set; get; }

        [Required, StringLength(70)]
        public string Email { set; get; }

        [Required, StringLength(30)]
        public string Password { set; get; }


        [Required, ScaffoldColumn(false)]  
        public Guid ActivateGuid { set; get; }

        [DisplayName("Aktif mi")]
        public bool isActive { set; get; }

        [DisplayName("Admin mi")]
        public bool isAdmin { set; get; }

        //ScaffoldColumn otomatik sayfa oluşturulurken oluşmasını istemiyorsak false verilir
        [StringLength(50),ScaffoldColumn(false)]
        public string ProfileImageFileName { get; set; }

        [DisplayName("Açıklama")]
        [StringLength(250)]
        public string Description { set; get; }

        [DisplayName("Meslek")]
        [StringLength(50)]
        public string Job { set; get; }

        [DisplayName("Ülke")]
        [StringLength(50)]
        public string Country { set; get; }

        [DisplayName("Doğum Tarihi")]
        public DateTime DateOfBirth { set; get; }

        [DisplayName("Yasaklı mı")]
        public bool isBanned { set; get; }

        [DisplayName("Favori Rengim")]
        public string myColor { set; get; }
 
        //int ve bool değerlerin varsayılan halleri boş geçilemezdir


        public virtual List<Note> Notes { set; get; }
        public virtual List<Comment> Comments { set; get; }
        public virtual List<Liked> Likes { set; get; }


        public EvernoteUser()
        {
            Notes = new List<Note>();
            Comments = new List<Comment>();
            Likes = new List<Liked>();
        }

    }

}
