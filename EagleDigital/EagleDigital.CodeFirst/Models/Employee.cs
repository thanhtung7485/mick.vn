using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleDigital.CodeFirst.Models
{
    [Table("Employee")]
    public class Employee : Person
    {
        [MaxLength(30)]
        public string Title { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> BirthDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public Nullable<DateTime> HireDate { get; set; }
        public int? ReportsTo { get; set; }

        // Navigation properties  
        [ForeignKey("ReportsTo")]
        public virtual Employee Superior { get; set; }
        public virtual ICollection<Employee> Subordinates { get; set; }
        public virtual ICollection<Customer> Customers { get; set; }
    }
}
