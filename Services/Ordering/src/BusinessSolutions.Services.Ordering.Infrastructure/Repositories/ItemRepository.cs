using BusinessSolutions.Services.Ordering.Domain.Entities;
using BusinessSolutions.Services.Ordering.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutions.Services.Ordering.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly OrderingContext _context;
        public IUnItOfWork UnitOfWork => _context;

        public ItemRepository(OrderingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CheckAsync(int id)
        {
            return await _context
                .Items
                .AnyAsync(e => e.ItemId == id);
        }

        public async Task<IEnumerable<Item>> GetAsync()
        {
            return await _context
                .Items
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Item?> GetAsync(int id)
        {
            return await _context
                .Items
                .AsNoTracking()
                .Where(x => x.ItemId == id)
                .FirstOrDefaultAsync();
        }

        public Item Add(Item item)
        {
            return _context.Items.Add(item).Entity;
        }

        public Item Update(Item item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }

        public Item Delete(Item item)
        {
            return _context.Items.Remove(item).Entity;
        }
    }
}
