using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleDigital.CodeFirst.Models
{
    [Table("Customer")]
    public class Customer : Person
    {
        [MaxLength(80)]
        public string Company { get; set; }
        public int? SupportRepId { get; set; }

        // Navigation properties  
        [ForeignKey("SupportRepId")]
        public virtual Employee SupportRep { get; set; }
        public virtual ICollection<Invoice> Invoices { get; set; }
    }
}
