using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleDigital.CodeFirst.Models
{
    public class Invoice : BusinessBase
    {
        public int InvoiceId { get; set; }
        public int CustomerId { get; set; }
        [Required]
        public DateTime InvoiceDate { get; set; }
        [MaxLength(70)]
        public string BillingAddress { get; set; }
        [MaxLength(40)]
        public string BillingCity { get; set; }
        [MaxLength(40)]
        public string BillingState { get; set; }
        [MaxLength(40)]
        public string BillingCountry { get; set; }
        [MaxLength(10)]
        public string BillingPostalCode { get; set; }
        [Required]
        public decimal Total { get; set; }

        // Navigation property  
        public virtual Customer Customer { get; set; }
    }
}
