using BusinessSolutions.Services.Ordering.Domain.Entities;
using BusinessSolutions.Services.Ordering.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutions.Services.Ordering.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderingContext _context;
        public IUnItOfWork UnitOfWork => _context;

        public OrderRepository(OrderingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> CheckAsync(string number)
        {
            return await _context
                .Orders
                .AsNoTracking()
                .AnyAsync(e => e.Number == number);
        }

        public async Task<bool> CheckByIdAsync(int id)
        {
            return await _context
                .Orders
                .AsNoTracking()
                .AnyAsync(e => e.Id == id);
        } 

        public async Task<IEnumerable<Order>> GetAsync()
        {
            return await _context
                .Orders
                .AsNoTracking()
                .Where(x => !x.IsInactive)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public async Task<Order?> GetAsync(string number)
        {
            return await _context
                .Orders
                .AsNoTracking()
                .Where(x => x.Number == number)
                .Include(x => x.Provider)
                .Include(x => x.Items)
                .FirstOrDefaultAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context
                .Orders
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Include(x => x.Provider)
                .Include(x => x.Items)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetByFilterAsync(
            int? providerId, string? providerName, string? orderNumber,
            DateTimeOffset? startTime, DateTimeOffset? endTime,
            string? itemName, string? itemUnit)
        {
            return await _context
                .Orders
                .AsNoTracking()
                .Where(x => !x.IsInactive)
                .Where(x => orderNumber == null || x.Number!.StartsWith(orderNumber))
                .Where(x => providerId == null || x.ProviderId == providerId)
                .Where(x => string.IsNullOrEmpty(providerName) || x.Provider!.ProviderName == providerName)
                .Where(x => startTime == null || DateTimeOffset.Compare(x.Date, (DateTimeOffset)startTime) > 0)
                .Where(x => endTime == null || DateTimeOffset.Compare(x.Date, (DateTimeOffset)endTime) < 0)
                .Where(x => string.IsNullOrEmpty(itemName) || x.Items!.Any(c => c.Name == itemName))
                .Where(x => string.IsNullOrEmpty(itemUnit) || x.Items!.Any(c => c.Unit == itemUnit))
                .Include(x => x.Provider)
                .Include(x => x.Items)
                .OrderByDescending(x => x.Date)
                .ToListAsync();
        }

        public Order Add(Order order)
        {
            return _context.Orders.Add(order).Entity;
        }

        public Order Update(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            return order;
        }
    }
}
