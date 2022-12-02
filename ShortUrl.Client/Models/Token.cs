using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.Client.Models
{
    public class Token
    {
        public int Id { get; set; }

        [DataType(DataType.Url)]
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
    }
}
