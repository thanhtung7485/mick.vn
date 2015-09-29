using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleDigital.CodeFirst.Models
{
    [Table("ContactUs")]
    public  class ContactUs
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
