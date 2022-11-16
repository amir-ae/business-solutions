using BusinessSolutions.Services.Ordering.API.Client.Base;
using BusinessSolutions.Services.Ordering.API.Contract.Requests.Provider;

namespace BusinessSolutions.Services.Ordering.API.Client.Resources
{
    public class ProviderResource : IProviderResource
    {
        private readonly IBaseClient _client;

        public ProviderResource(IBaseClient client)
        {
            _client = client;
        }

        public async Task<T> Get<T>(int? pageSize, int? pageIndex, CancellationToken cancellationToken = default)
        {
            var dict = new Dictionary<string, string>();
            if (pageSize is not null) dict.Add(nameof(pageSize), pageSize.Value.ToString());
            if (pageIndex is not null) dict.Add(nameof(pageIndex), pageIndex.Value.ToString());

            var uri = _client.BuildUri("api/providers", dict);
            return await _client.GetAsync<T>(uri, cancellationToken);
        }

        public async Task<T> GetById<T>(int id, CancellationToken cancellationToken)
        {
            var uri = _client.BuildUri($"api/providers/{id}");
            return await _client.GetAsync<T>(uri, cancellationToken);
        }

        public async Task<T> Post<T>(AddProviderRequest request, CancellationToken cancellationToken = default)
        {
            var uri = _client.BuildUri("api/providers");
            return await _client.PostAsJson<T, AddProviderRequest>(uri, request, cancellationToken);
        }

        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            var uri = _client.BuildUri($"api/providers/{id}");
            await _client.DeleteAsync(uri, cancellationToken);
        }
    }
}