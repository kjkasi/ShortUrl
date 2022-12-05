namespace ShortUrl.Web.Services
{
    public class FormatService : IFormatService
    {
        public string GetFormattedFileName(string fileName)
        {
            return $"http://localhost:5000/images/{fileName}";
        }

        public string GetFormattedShortUrl(string shortUrl)
        {
            return $"http://localhost:5001/{shortUrl}";
        }
    }
}
