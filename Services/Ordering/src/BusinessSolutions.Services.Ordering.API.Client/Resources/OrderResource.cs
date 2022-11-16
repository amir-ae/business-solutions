using BusinessSolutions.Services.Ordering.API.Client.Base;
using BusinessSolutions.Services.Ordering.API.Contract.Requests.Order;

namespace BusinessSolutions.Services.Ordering.API.Client.Resources
{
    public class OrderResource : IOrderResource
    {
        private readonly IBaseClient _client;

        public OrderResource(IBaseClient client)
        {
            _client = client;
        }

        public async Task<T> Get<T>(int? pageSize, int? pageIndex, CancellationToken cancellationToken = default)
        {         
            var dict = new Dictionary<string, string>();
            if (pageSize is not null) dict.Add(nameof(pageSize), pageSize.Value.ToString());
            if (pageIndex is not null) dict.Add(nameof(pageIndex), pageIndex.Value.ToString());

            var uri = _client.BuildUri("api/orders", dict);
            return await _client.GetAsync<T>(uri, cancellationToken);
        }

        public async Task<T> GetById<T>(int id, CancellationToken cancellationToken)
        {
            var uri = _client.BuildUri($"api/orders/{id}");
            return await _client.GetAsync<T>(uri, cancellationToken);
        }

        public async Task<T> GetByNumber<T>(string number, CancellationToken cancellationToken = default)
        {
            var uri = _client.BuildUri($"api/orders/{number}");
            return await _client.GetAsync<T>(uri, cancellationToken);
        }

        public async Task<T> GetByFilter<T>(
            int? providerId, string? providerName, string? orderNumber, string? startTime, string? endTime,
            string? itemName, string? itemUnit, int? pageSize, int? pageIndex, CancellationToken cancellationToken = default)
        {
            var dict = new Dictionary<string, string>()
            {
                { nameof(providerName), providerName! },
                { nameof(orderNumber), orderNumber! },
                { nameof(startTime), startTime! },
                { nameof(endTime), endTime! },
                { nameof(itemName), itemName! },
                { nameof(itemUnit), itemUnit! }
            };
            if (providerId != null) dict.Add(nameof(providerId), providerId.Value.ToString());
            if (pageSize != null) dict.Add(nameof(pageSize), pageSize.Value.ToString());
            if (pageIndex != null) dict.Add(nameof(pageIndex), pageIndex.Value.ToString());
           
            foreach (var key in dict.Keys.ToArray())
            {
                if (string.IsNullOrEmpty(dict[key])) dict.Remove(key);
            }
          
            var uri = _client.BuildUri($"/api/orders/filter", dict!);

            return await _client.GetAsync<T>(uri, cancellationToken);
        }

        public async Task<T> Post<T>(AddOrderRequest request, CancellationToken cancellationToken = default)
        {
            var uri = _client.BuildUri("api/orders");
            return await _client.PostAsJson<T, AddOrderRequest>(uri, request, cancellationToken);
        }

        public async Task<T> Put<T>(EditOrderRequest request, CancellationToken cancellationToken = default)
        {
            var uri = _client.BuildUri($"api/orders/{request.Number}");
            return await _client.PutAsJson<T, EditOrderRequest>(uri, request, cancellationToken);
        }

        public async Task Delete(string number, CancellationToken cancellationToken = default)
        {
            var uri = _client.BuildUri($"api/orders/{number}");
            await _client.DeleteAsync(uri, cancellationToken);
        }
    }
}