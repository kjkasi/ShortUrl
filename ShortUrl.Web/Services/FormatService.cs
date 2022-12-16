namespace ShortUrl.Web.Services
{
    public class FormatService : IFormatService
    {
        public string GetFileName(string fileName)
        {
            return $"http://localhost:5000/images/{fileName}";
        }

        public string GetShortUrl(string shortUrl)
        {
            return $"http://localhost:5001/{shortUrl}";
        }
    }
}
