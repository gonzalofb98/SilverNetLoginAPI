using System;

namespace Dto.Response
{
    public class DtoBookResponse
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Synopsis { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
