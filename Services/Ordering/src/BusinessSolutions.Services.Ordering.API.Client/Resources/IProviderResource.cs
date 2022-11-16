using BusinessSolutions.Services.Ordering.API.Contract.Requests.Provider;

namespace BusinessSolutions.Services.Ordering.API.Client.Resources
{
    public interface IProviderResource
    {
        Task<T> Get<T>(int? pageSize, int? pageIndex, CancellationToken cancellationToken = default);
        Task<T> GetById<T>(int id, CancellationToken cancellationToken = default);
        Task<T> Post<T>(AddProviderRequest request, CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
    }
}