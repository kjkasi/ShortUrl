using ShortUrl.App.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.App.Models
{
    public interface ITokenRepository
    {
        Task<TokenReadDto> GetTokenById(int tokenId);
        Task<TokenReadDto> CreateToken(TokenCreateDto token);
    }
}
