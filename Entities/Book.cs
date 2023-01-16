using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Book : EntityBase
    {
        [Required]
        [StringLength(500)]
        public string Title { get; set; }

        [Required]
        [StringLength(200)]
        public string Author { get; set; }

        public string Synopsis { get; set; } 

        public DateTime ReleaseDate { get; set; }
    }
}
