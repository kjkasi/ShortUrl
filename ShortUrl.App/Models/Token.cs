using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.App.Models
{
    public class Token
    {
        public int Id { get; set; }

        public string Url { get; set; }

        public string ShortUr { get; set; }
    }
}
