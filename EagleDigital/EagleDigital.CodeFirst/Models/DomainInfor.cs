using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EagleDigital.CodeFirst.Models
{

    [Table("DomainInfor")]
    public class DomainInfor
    {
        [Key]
        public int Id { get; set; }
        public int DomainId { get; set; }
        public int TabNameId { get; set; }
        public string Content { get; set; }

        public virtual TabName TabName { get; set; }
        public virtual Domain Domain { get; set; }

    }
}