using ShortUrl.Api.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShortUrl.Api.Repositories
{
    public interface IItemRepository
    {
        Task<IEnumerable<Item>> GetAllItems();
        Task<bool> CreateItem(Item item);
        Task<Item> GetItemById(int id);
        Task<Item> GetItemByUrl(string shortUrl);
        Task<bool> UpdateItem(Item item);       
        Task<bool> DeleteItem(Item item);
    }
}
