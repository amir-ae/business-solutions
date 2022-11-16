using BusinessSolutions.Services.Ordering.API.Contract.Requests.Order;

namespace BusinessSolutions.Services.Ordering.API.Client.Resources
{
    public interface IOrderResource
    {
        Task<T> Get<T>(int? pageSize, int? pageIndex, CancellationToken cancellationToken = default);
        Task<T> GetById<T>(int id, CancellationToken cancellationToken = default);
        Task<T> GetByNumber<T>(string number, CancellationToken cancellationToken = default);
        Task<T> GetByFilter<T>(
            int? providerId, string? providerName, string? orderNumber, string? startTime, string? endTime,
            string? itemName, string? itemUnit, int? pageSize, int? pageIndex, 
            CancellationToken cancellationToken = default);
        Task<T> Post<T>(AddOrderRequest request, CancellationToken cancellationToken = default);
        Task<T> Put<T>(EditOrderRequest request, CancellationToken cancellationToken = default);
        Task Delete(string number, CancellationToken cancellationToken = default);
    }
}