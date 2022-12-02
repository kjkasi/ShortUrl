using ShortUrl.App.Dtos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.App.Models
{
    public interface ITokenRepository
    {
        Task<Token> CreateToken(Token token);
        Task<Token> GetTokenByUrl(string shortUrl);
        Task<IEnumerable<Token>> GetAllTokens();
        Task<Token> GetTokenById(int id);
        Task<bool> DeleteToken(int id);
    }
}
