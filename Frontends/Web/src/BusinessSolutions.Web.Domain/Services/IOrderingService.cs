using BusinessSolutions.Web.Domain.Models;
using BusinessSolutions.Web.Domain.Models.Api.Item;
using BusinessSolutions.Web.Domain.Models.Api.Order;
using BusinessSolutions.Web.Domain.Models.Api.Provider;

namespace BusinessSolutions.Web.Domain.Services
{
    public interface IOrderingService
    {
        // Provider
        Task<Paginated<Provider>> GetProviders(int? pageSize, int? pageIndex, CancellationToken cancellationToken = default);
        Task<Provider> GetProviderById(int id, CancellationToken cancellationToken = default);
        Task<Provider> PostProvider(AddProvider request, CancellationToken cancellationToken = default);
        Task DeleteProvider(int id, CancellationToken cancellationToken = default);

        // Order
        Task<Paginated<Order>> GetOrders(int? pageSize, int? pageIndex, CancellationToken cancellationToken = default);
        Task<Order> GetOrderById(int id, CancellationToken cancellationToken = default);
        Task<Order> GetOrderByNumber(string number, CancellationToken cancellationToken = default);
        Task<Paginated<Order>> GetOrdersByFilter(
            int? providerId, string? providerName, string? orderNumber, string? startTime, string? endTime,
            string? itemName, string? itemUnit, int? pageSize, int? pageIndex,
            CancellationToken cancellationToken = default);
        Task<Order> PostOrder(AddOrder request, CancellationToken cancellationToken = default);
        Task<Order> PutOrder(EditOrder request, CancellationToken cancellationToken = default);
        Task DeleteOrder(string number, CancellationToken cancellationToken = default);

        // Item
        Task<Item> GetItemById(int id, CancellationToken cancellationToken = default);
        Task<Item> PostItem(AddItem request, CancellationToken cancellationToken = default);
        Task<Item> PutItem(EditItem request, CancellationToken cancellationToken = default);
        Task DeleteItem(int id, CancellationToken cancellationToken = default);
    }
}