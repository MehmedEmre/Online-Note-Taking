using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myevirnote.Entities
{

    public class MyEntityBase
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        public DateTime CreatedOn { set; get; }
        [Required]
        public DateTime ModifiedOn { set; get; }
        [Required,StringLength(30)]
        public string ModifiedUserName { set; get; }

    }
}
