using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EagleDigital.CodeFirst.TenantMusic.Models
{
    [Table("Song")]
    public class Song
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public int? GenreId { get; set; }
        public int? AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Genre Genre { get; set; }
    }
}
