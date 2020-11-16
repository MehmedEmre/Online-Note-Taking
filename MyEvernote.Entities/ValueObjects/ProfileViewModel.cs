using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvernote.Entities.ValueObjects
{
    public class ProfileViewModel
    {

        [DisplayName("Ad"), StringLength(15, MinimumLength = 5, ErrorMessage = "{0} Maximum {1} Minumum {2} Karakter Uzunluğunda Olmalıdır"), Required(ErrorMessage ="{0} Alanı Boş Geçilemez.")]
   
        public string Name { set; get; }


        [DisplayName("Soyad"), StringLength(15, MinimumLength = 5, ErrorMessage = "{0} Maximum {1} Minumum {2} Karakter Uzunluğunda Olmalıdır."), Required(ErrorMessage = "{0} Alanı Boş Geçilemez.")]
       
        public string Surname { set; get; }

       
       [DisplayName("E-Mail"), StringLength(45, MinimumLength = 5, ErrorMessage = "{0} Minumum {1} Maximum {2} Karakter Uzunluğunda Olmalıdır."), Required(ErrorMessage = "{0} Alanı Boş Geçilemez.")]
       
        public string Email { set; get; }


        [DisplayName("Ad"), StringLength(45, MinimumLength = 3, ErrorMessage = "{0} Minumum {1} Maximum {2} Karakter Uzunluğunda Olmalıdır.")]
        public string Job { set; get; }
       
        public DateTime DateOfBirth { set; get; }

        [DisplayName("Ülke"),StringLength(15, MinimumLength = 3, ErrorMessage = "{0} Maximum {1} Minumum {2} Karakter Uzunluğunda Olmalıdır")]
        public string Country { set; get; }

        [Required, StringLength(30), DisplayName("Kullanıcı Adı")]
        public string Username { set; get; }

        [StringLength(50)]
        public string ProfileImageFileName { get; set; }

        [DisplayName("Açıklama"), StringLength(250, ErrorMessage = "{0}  Maximum {2} Karakter Uzunluğunda Olmalıdır")]
        public string Description { set; get; }

        public bool isAdmin { set; get; }

        public int ID { get; set; }


    }
}
