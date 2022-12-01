using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShortUrl.App.Dtos;
using ShortUrl.App.Models.Contexts;
using ShortUrl.App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.App.Models
{
    public class TokenRepository : ITokenRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ITokenService _service;

        public TokenRepository(AppDbContext context, IMapper mapper, ITokenService service)
        {
            _context = context;
            _mapper = mapper;
            _service = service;
        }

        public async Task<Token> CreateToken(Token token)
        {
            _context.Add(token);

            string shortUrl = _service.Encode(token.Id);

            token.ShortUrl = $"localhost:5000/api/v1/Token/{shortUrl}";

            await _context.SaveChangesAsync();
            return token;
        }

        public async Task<Token> GetTokenByUrl(string shortUrl)
        {
            Token token = await _context.TokenItems.Where(x => x.ShortUrl == shortUrl).FirstOrDefaultAsync();
            return token;
        }
    }
}
