using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyEvernote.Entities.ValueObjects
{
    public class RegisterViewModel
    {
        [DisplayName("Kullanıcı Adı"),
        Required(ErrorMessage = "{0} alanı boş geçilemez")]
        [StringLength(70, ErrorMessage = "{0} maximum {1} karakter olmalıdır")]
        public string Username { get; set; }

        [DisplayName("E-Mail"), 
        Required(ErrorMessage = "{0} alanı boş geçilemez")]
        [StringLength(70,ErrorMessage ="{0} maximum {1} karakter olmalıdır")]
        [EmailAddress(ErrorMessage ="{0} alanı için geçerli bir e-posta adresi girin")]
        public string Email { get; set; }

        [DisplayName("Şifre"),
        Required(ErrorMessage = "{0} alanı boş geçilemez")]
        [StringLength(25, ErrorMessage = "{0} maximum {1} karakter olmalıdır")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        [StringLength(25, ErrorMessage = "{0} maximum {1} karakter olmalıdır")]
        [Compare(nameof(Password), ErrorMessage ="{0} ile {1} uyuşmuyor")]
        public string Repassword { get; set; }

        [DisplayName("Favori Rengim"), Required(ErrorMessage = "{0} alanı boş geçilemez")]
        public string myColor { get; set; }

    }
}