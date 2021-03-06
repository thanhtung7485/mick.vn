﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EagleDigital.CodeFirst.TenantDevelopment.Models
{
    [Table("Category")]
    public  class Category
    {
        public Category()
        {
            this.SubCategories = new HashSet<SubCategory>();
        }
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }

        public virtual ICollection<SubCategory> SubCategories { get; set; }
    }
}
