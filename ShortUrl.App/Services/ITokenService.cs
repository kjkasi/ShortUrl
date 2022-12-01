namespace ShortUrl.App.Services
{
    public interface ITokenService
    {
        public string Encode(int num);

        public int Decode(string str);
    }
}
