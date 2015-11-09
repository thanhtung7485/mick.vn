using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EagleDigital.CodeFirst.TenantDevelopment.Models
{
    [Table("ContactUs")]
    public  class ContactUs
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
    }
}
