using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleDigital.CodeFirst.Models
{
    public abstract class Person : BusinessBase
    {
        public int PersonId { get; set; }
        [Required]
        [MaxLength(40)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(20)]
        public string LastName { get; set; }
        [MaxLength(70)]
        public string Address { get; set; }
        [MaxLength(40)]
        public string City { get; set; }
        [MaxLength(40)]
        public string State { get; set; }
        [MaxLength(40)]
        public string Country { get; set; }
        [MaxLength(10)]
        public string PostalCode { get; set; }
        [MaxLength(24)]
        public string Phone { get; set; }
        [MaxLength(24)]
        public string Fax { get; set; }
        [Required]
        [MaxLength(60)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}
