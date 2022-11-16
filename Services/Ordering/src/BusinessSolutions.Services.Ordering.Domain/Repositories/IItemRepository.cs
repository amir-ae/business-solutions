using BusinessSolutions.Services.Ordering.Domain.Entities;

namespace BusinessSolutions.Services.Ordering.Domain.Repositories
{
    public interface IItemRepository : IRepository
    {
        Task<bool> CheckAsync(int id);
        Task<IEnumerable<Item>> GetAsync();
        Task<Item?> GetAsync(int id);
        Item Add(Item item);
        Item Update(Item item);
        Item Delete(Item item);
    }
}
