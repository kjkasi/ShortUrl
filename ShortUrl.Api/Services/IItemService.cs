namespace ShortUrl.Api.Services
{
    public interface IItemService
    {
        public string Encode(int num);

        public int Decode(string str);
    }
}
