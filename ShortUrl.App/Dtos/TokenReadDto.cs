using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.App.Dtos
{
    public class TokenReadDto
    {
        public string OriginalUrl { get; set; }

        public string ShortUrl { get; set; }
    }
}
