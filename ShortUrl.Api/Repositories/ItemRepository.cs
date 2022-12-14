using IronBarCode;
using Microsoft.EntityFrameworkCore;
using ShortUrl.Api.Data;
using ShortUrl.Api.Models;
using ShortUrl.Api.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ShortUrl.Api.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemContext _context;
        private readonly IAliasService _aliasService;

        public ItemRepository(ItemContext context, IAliasService aliasService)
        {
            _context = context;
            _aliasService = aliasService;
        }

        public async Task<bool> CreateItem(Item item)
        {
            try
            {
                _context.Add(item);
                await _context.SaveChangesAsync();

                string shortUrl = _aliasService.ConfusionConvert(item.Id);

                var fileName = Path.GetFileName($"{shortUrl}.png");
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot/images", fileName);

                var qr = QRCodeWriter.CreateQrCode($"http://localhost:5000/images/{fileName}", 150, QRCodeWriter.QrErrorCorrectionLevel.Low);
                qr.SaveAsPng(filePath);

                item.ShortUrl = shortUrl;
                item.FileName = fileName;

                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteItem(Item item)
        {
            try
            {
                _context.Remove(item);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
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
