namespace ShortUrl.Api.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string OriginalUrl { get; set; }
        public string ShortUrl { get; set; }
        public string FileName { get; set; }
    }
}
