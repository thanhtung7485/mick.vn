using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EagleDigital.CodeFirst.TenantDevelopment.Models
{

    [Table("RequestProject")]
    public  class RequestProject
    {
        [Key]
        public int Id { get; set; }
        public Nullable<int> DomainId { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }
        public string Content { get; set; }
        [MaxLength(500)]
        public string Contact { get; set; }

        public virtual Domain Domain { get; set; }
    }
}
