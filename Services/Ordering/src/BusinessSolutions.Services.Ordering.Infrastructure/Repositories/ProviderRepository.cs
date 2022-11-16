using BusinessSolutions.Services.Ordering.Domain.Entities;
using BusinessSolutions.Services.Ordering.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BusinessSolutions.Services.Ordering.Infrastructure.Repositories
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly OrderingContext _context;
        public IUnItOfWork UnitOfWork => _context;


        public ProviderRepository(OrderingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context
                .Providers
                .AnyAsync(e => e.ProviderId == id);
        }

        public async Task<IEnumerable<Provider>> GetAsync()
        {
            return await _context
                .Providers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Provider?> GetAsync(int id)
        {
            return  await _context
                .Providers
                .AsNoTracking()
                .Where(x => x.ProviderId == id)
                .FirstOrDefaultAsync();
        }

        public Provider Add(Provider provider)
        {
            return _context.Providers.Add(provider).Entity;
        }

        public Provider Delete(Provider provider)
        {
            return _context.Providers.Remove(provider).Entity;
        }
    }
}
