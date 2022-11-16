using AutoMapper;
using BusinessSolutions.Services.Ordering.API.Client;
using BusinessSolutions.Services.Ordering.API.Contract.Requests.Item;
using BusinessSolutions.Services.Ordering.API.Contract.Requests.Order;
using BusinessSolutions.Services.Ordering.API.Contract.Requests.Provider;
using BusinessSolutions.Web.Domain.Models;
using BusinessSolutions.Web.Domain.Models.Api.Item;
using BusinessSolutions.Web.Domain.Models.Api.Order;
using BusinessSolutions.Web.Domain.Models.Api.Provider;
using BusinessSolutions.Web.Domain.Services;

namespace BusinessSolutions.Web.Infrastructure.Services
{
    public class OrderingService : IOrderingService
    {
        private readonly IOrderingClient orderingClient;
        private readonly IMapper mapper;

        public OrderingService(IOrderingClient orderingClient, IMapper mapper)
        {
            this.orderingClient = orderingClient;
            this.mapper = mapper;
        }

        // Provider
        public async Task<Paginated<Provider>> GetProviders(
            int? pageSize, int? pageIndex, CancellationToken cancellationToken = default)
            => await orderingClient.Provider.Get<Paginated<Provider>>(pageSize, pageIndex, cancellationToken);

        public async Task<Provider> GetProviderById(int id, CancellationToken cancellationToken = default) 
            => await orderingClient.Provider.GetById<Provider>(id, cancellationToken);

        public async Task<Provider> PostProvider(AddProvider request, CancellationToken cancellationToken = default)
            => await orderingClient.Provider.Post<Provider>(mapper.Map<AddProviderRequest>(request), cancellationToken);

        public async Task DeleteProvider(int id, CancellationToken cancellationToken = default)
            => await orderingClient.Provider.Delete(id, cancellationToken);

        // Order
        public async Task<Paginated<Order>> GetOrders(
            int? pageSize, int? pageIndex, CancellationToken cancellationToken = default)
            => await orderingClient.Order.Get<Paginated<Order>>(pageSize, pageIndex, cancellationToken);

        public async Task<Order> GetOrderById(int id, CancellationToken cancellationToken = default)
            => await orderingClient.Order.GetById<Order>(id, cancellationToken);
        public async Task<Order> GetOrderByNumber(string number, CancellationToken cancellationToken = default)
            => mapper.Map<Order>(await orderingClient.Order
                .GetByNumber<Order>(number, cancellationToken));

        public async Task<Paginated<Order>> GetOrdersByFilter(
            int? providerId, string? providerName, string? orderNumber, string? startTime, string? endTime,
            string? itemName, string? itemUnit, int? pageSize, int? pageIndex,
            CancellationToken cancellationToken = default)
            => await orderingClient.Order.GetByFilter<Paginated<Order>>(providerId, providerName, 
                orderNumber, startTime, endTime, itemName, itemUnit, pageSize, pageIndex, cancellationToken);

        public async Task<Order> PostOrder(AddOrder request, CancellationToken cancellationToken = default)
            => await orderingClient.Order.Post<Order>(mapper.Map<AddOrderRequest>(request), cancellationToken);

        public async Task<Order> PutOrder(EditOrder request, CancellationToken cancellationToken = default)
            => await orderingClient.Order.Put<Order>(mapper.Map<EditOrderRequest>(request), cancellationToken);

        public async Task DeleteOrder(string number, CancellationToken cancellationToken = default)
            => await orderingClient.Order.Delete(number, cancellationToken);

        // Item
        public async Task<Item> GetItemById(int id, CancellationToken cancellationToken = default)
            => await orderingClient.Item.GetById<Item>(id, cancellationToken);
 
        public async Task<Item> PostItem(AddItem request, CancellationToken cancellationToken = default)
            => await orderingClient.Item
                .Post<Item>(mapper.Map<AddItemRequest>(request), cancellationToken);

        public async Task<Item> PutItem(EditItem request, CancellationToken cancellationToken = default)
            => await orderingClient.Item
                .Put<Item>(mapper.Map<EditItemRequest>(request), cancellationToken);

        public async Task DeleteItem(int id, CancellationToken cancellationToken = default)
            => await orderingClient.Item.Delete(id, cancellationToken);
    }
}