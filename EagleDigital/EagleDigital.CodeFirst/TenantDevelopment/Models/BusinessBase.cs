using System;
using System.ComponentModel.DataAnnotations;

namespace EagleDigital.CodeFirst.TenantDevelopment.Models
{
    public abstract class BusinessBase
    {
        [Required]
        [MaxLength(60)]
        public string StatusId { get; set; }
        [Required]
        public DateTime InsAt { get; set; }
        [Required]
        [MaxLength(50)]
        public string InsBy { get; set; }
        [Required]
        public DateTime UpdAt { get; set; }
        [Required]
        [MaxLength(50)]
        public string UpdBy { get; set; }
    }
}
