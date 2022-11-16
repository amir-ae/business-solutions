using BusinessSolutions.Services.Ordering.API.Contract.Requests.Item;

namespace BusinessSolutions.Services.Ordering.API.Client.Resources
{
    public interface IItemResource
    {
        Task<T> GetById<T>(int id, CancellationToken cancellationToken = default);
        Task<T> Post<T>(AddItemRequest request, CancellationToken cancellationToken = default);
        Task<T> Put<T>(EditItemRequest request, CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
    }
}