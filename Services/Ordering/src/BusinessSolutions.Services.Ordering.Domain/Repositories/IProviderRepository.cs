using BusinessSolutions.Services.Ordering.Domain.Entities;

namespace BusinessSolutions.Services.Ordering.Domain.Repositories
{
    public interface IProviderRepository : IRepository
    {
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<Provider>> GetAsync();
        Task<Provider?> GetAsync(int id);
        Provider Add(Provider provider);
        Provider Delete(Provider provider);
    }
}
