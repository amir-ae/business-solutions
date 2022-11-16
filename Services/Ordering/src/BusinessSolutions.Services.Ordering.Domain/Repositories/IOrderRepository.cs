using BusinessSolutions.Services.Ordering.Domain.Entities;

namespace BusinessSolutions.Services.Ordering.Domain.Repositories
{
    public interface IOrderRepository : IRepository
    {
        Task<bool> CheckAsync(string number);
        Task<bool> CheckByIdAsync(int id);
        Task<IEnumerable<Order>> GetAsync();
        Task<Order?> GetAsync(string number);
        Task<Order?> GetByIdAsync(int id);
        Task<IEnumerable<Order>> GetByFilterAsync(
            int? providerId, string? providerName, string? orderNumber, 
            DateTimeOffset? startTime, DateTimeOffset? endTime,
            string? itemName, string? itemUnit);
        Order Add(Order order);
        Order Update(Order order); 
        
    }
}
