using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EagleDigital.CodeFirst.TenantDevelopment.Models
{
    [Table("AboutUs")]
    public class AboutUs
    {
        [Key]
        public int Id { get; set; }
        public int TabNameId { get; set; }
        public string Content { get; set; }

        public virtual TabName TabName { get; set; }
    }

}
