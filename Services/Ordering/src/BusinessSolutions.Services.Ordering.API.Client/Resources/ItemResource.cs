using BusinessSolutions.Services.Ordering.API.Client.Base;
using BusinessSolutions.Services.Ordering.API.Contract.Requests.Item;

namespace BusinessSolutions.Services.Ordering.API.Client.Resources
{
    public class ItemResource : IItemResource
    {
        private readonly IBaseClient _client;

        public ItemResource(IBaseClient client)
        {
            _client = client;
        }

        public async Task<T> GetById<T>(int id, CancellationToken cancellationToken)
        {
            var uri = _client.BuildUri($"api/items/{id}");
            return await _client.GetAsync<T>(uri, cancellationToken);
        }

        public async Task<T> Post<T>(AddItemRequest request, CancellationToken cancellationToken = default)
        {
            var uri = _client.BuildUri("api/items");
            return await _client.PostAsJson<T, AddItemRequest>(uri, request, cancellationToken);
        }

        public async Task<T> Put<T>(EditItemRequest request, CancellationToken cancellationToken = default)
        {
            var uri = _client.BuildUri($"api/items/{request.ItemId}");
            return await _client.PutAsJson<T, EditItemRequest>(uri, request, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            var uri = _client.BuildUri($"api/items/{id}");
            await _client.DeleteAsync(uri, cancellationToken);
        }
    }
}