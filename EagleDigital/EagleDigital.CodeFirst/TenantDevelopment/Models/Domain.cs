using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EagleDigital.CodeFirst.TenantDevelopment.Models
{
    [Table("Domain")]
    public  class Domain
    {
        public Domain()
        {
            this.RequestProjects = new HashSet<RequestProject>();
            this.DomainInfors = new HashSet<DomainInfor>();
        }
        [Key]
        public int Id { get; set; }
        public int SubCategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public virtual ICollection<RequestProject> RequestProjects { get; set; }
        public virtual ICollection<DomainInfor> DomainInfors { get; set; }
    }
}
