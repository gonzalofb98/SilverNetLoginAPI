using System;
using System.ComponentModel.DataAnnotations;

namespace Dto.Request
{
    public class DtoBookRequest
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
