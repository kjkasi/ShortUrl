namespace ShortUrl.Web.Services
{
    public interface IFormatService
    {
        string GetFormattedFileName(string fileName);
        string GetFormattedShortUrl(string shortUrl);
    }
}
