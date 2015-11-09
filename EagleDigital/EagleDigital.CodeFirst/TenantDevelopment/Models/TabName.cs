using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EagleDigital.CodeFirst.TenantDevelopment.Models
{
    [Table("TabName")]
    public  class TabName
    {
        public TabName()
        {
            this.AboutUs = new HashSet<AboutUs>();
            this.DomainInfors = new HashSet<DomainInfor>();
        }
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public virtual ICollection<AboutUs> AboutUs { get; set; }
        public virtual ICollection<DomainInfor> DomainInfors { get; set; }

    }
}
