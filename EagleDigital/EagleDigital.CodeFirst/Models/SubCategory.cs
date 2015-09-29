using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleDigital.CodeFirst.Models
{

    [Table("SubCategory")]
    public  class SubCategory
    {
        public SubCategory()
        {
            this.Domains = new HashSet<Domain>();
        }
        [Key]
        public int Id { get; set; }
        public int CategoryId { get; set; }
       
        [MaxLength(100)]
        public string Name { get; set; }
     
        [MaxLength(100)]
        public string Description { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<Domain> Domains { get; set; }
    }
}
