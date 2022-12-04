using Microsoft.EntityFrameworkCore;
using ShortUrl.Api.Data;
using ShortUrl.Api.Models;
using ShortUrl.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.Api.Repositories
{
    public class ItemRepository1 : IItemRepository
    {
        private readonly ItemContext _context;
        private readonly IItemService _service;

        public ItemRepository1(ItemContext context, IItemService service)
        {
            _context = context;
            _service = service;
        }

        public async Task<Item> CreateItem(Item item)
        {
            _context.Add(item);

            string shortUrl = _service.Encode(item.Id);
            item.ShortUrl = shortUrl;

            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteItem(int id)
        {
            try
            {
                Item item = await _context.Items.FirstOrDefaultAsync(u => u.Id == id);
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Item>> GetAllItems()
        {
            return await _context.Items.ToListAsync();
        }

        public async Task<Item> GetItemById(int id)
        {
            Item item = await _context.Items.Where(x => x.Id == id).FirstOrDefaultAsync();
            return item;
        }

        public async Task<Item> GetItemByUrl(string shortUrl)
        {
            Item item = await _context.Items.Where(x => x.ShortUrl == shortUrl).FirstOrDefaultAsync();
            return item;
        }
    }
}
