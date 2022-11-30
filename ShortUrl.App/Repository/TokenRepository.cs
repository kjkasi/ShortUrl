using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShortUrl.App.Dtos;
using ShortUrl.App.Models.Contexts;
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

        public TokenRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TokenReadDto> CreateToken(TokenCreateDto token)
        {
            Token createItem = _mapper.Map<TokenCreateDto, Token>(token);

            _context.Add(createItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<Token, TokenReadDto>(createItem);
        }

        public Task<TokenReadDto> GetTokenById(int tokenId)
        {
            throw new NotImplementedException();
        }
    }
}
