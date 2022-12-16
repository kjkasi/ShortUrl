namespace ShortUrl.Web.Services
{
    public interface IFormatService
    {
        string GetFileName(string fileName);
        string GetShortUrl(string shortUrl);
    }
}
