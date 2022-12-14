using ShortUrl.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortUrl.Api.Repositories
{
    public interface IItemRepository
    {
        Task<bool> CreateItem(Item item);
        Task<Item> GetItemByUrl(string shortUrl);
        Task<IEnumerable<Item>> GetAllItems();
        Task<Item> GetItemById(int id);
        Task<bool> DeleteItem(Item item);
    }
}
