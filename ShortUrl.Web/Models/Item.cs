using System.ComponentModel.DataAnnotations;

namespace ShortUrl.Web.Models
{
    public class Item
    {
        public int Id { get; set; }

        [DataType(DataType.Url)]
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public string FileName { get; set; }
    }
}
