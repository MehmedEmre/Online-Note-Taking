using MyEvernote.Entities.Message;
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

    public class MyEntityBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [DisplayName("Oluşturulma Tarihi")]
        public DateTime CreatedOn { set; get; }

        [Required]
        [DisplayName("Düzenlenme Tarihi")]
        public DateTime ModifiedOn { set; get; }


        [Required,StringLength(30)]
        [DisplayName("Düzenleyen Kişi")]
        public string ModifiedUserName { set; get; }

    }
}
