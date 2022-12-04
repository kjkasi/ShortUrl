namespace ShortUrl.Api.Services
{
    public interface IAliasService
    {
        public string ConfusionConvert(long num);
        public long ConfusionConvert(string num);
    }
}
