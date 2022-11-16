using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Responses;

namespace BusinessSolutions.Services.Ordering.Domain.Services
{
    public interface IOrderService
    {
        Task<bool> CheckOrderAsync(GetOrderRequest request);
        Task<IEnumerable<OrderResponse>> GetOrdersAsync();
        Task<IEnumerable<OrderResponse>> GetOrdersByFilterAsync(GetOrdersByFilterRequest request);
        Task<OrderResponse?> GetOrderAsync(GetOrderRequest request);
        Task<OrderResponse> AddOrderAsync(AddOrderRequest request);
        Task<OrderResponse> EditOrderAsync(EditOrderRequest request);
        Task<OrderResponse> DeleteOrderAsync(DeleteOrderRequest request);
    }
}
