using AutoMapper;
using BusinessSolutions.Services.Ordering.Domain.Entities;
using BusinessSolutions.Services.Ordering.Domain.Repositories;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Responses;

namespace BusinessSolutions.Services.Ordering.Domain.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<bool> CheckOrderAsync(GetOrderRequest request)
        {
            if (request?.Id > 0)
            {
                return await _orderRepository.CheckByIdAsync(request.Id);
            }
            else if (!string.IsNullOrEmpty(request?.Number))
            {
                return await _orderRepository.CheckAsync(request.Number);
            }

            throw new ArgumentNullException();
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersAsync()
        {
            var result = await _orderRepository.GetAsync();

            return result.Select(x => _mapper.Map<OrderResponse>(x));
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByFilterAsync(GetOrdersByFilterRequest request)
        {
            var result = await _orderRepository.GetByFilterAsync(
                request.ProviderId, request.ProviderName,
                request.OrderNumber, request.StartTime, request.EndTime,
                request.ItemName, request.ItemUnit);

            return result.Select(x => _mapper.Map<OrderResponse>(x));
        }

        public async Task<OrderResponse?> GetOrderAsync(GetOrderRequest request)
        {
            Order? result;
            if (request?.Id > 0)
            {
                result = await _orderRepository.GetByIdAsync(request.Id);
            }
            else if (!string.IsNullOrEmpty(request?.Number))
            {
                result = await _orderRepository.GetAsync(request.Number);
            }
            else
            {
                throw new ArgumentNullException();
            }

            return result == null ? null : _mapper.Map<OrderResponse>(result);
        }

        public async Task<OrderResponse> AddOrderAsync(AddOrderRequest request)
        {
            var order = _mapper.Map<Order>(request);
            
            var result = _orderRepository.Add(order);
            await _orderRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(result);
        }

        public async Task<OrderResponse> EditOrderAsync(EditOrderRequest request)
        {
            if (request?.Number == null) throw new ArgumentNullException();

            var existingRecord = await _orderRepository.GetAsync(request.Number);

            if (existingRecord == null)
            {
                throw new ArgumentException($"Entity with {request.Number} is not present");
            }

            var entity = _mapper.Map<Order>(request);
            entity.Id = existingRecord.Id;
            var result = _orderRepository.Update(entity);

            await _orderRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(result);
        }

        public async Task<OrderResponse> DeleteOrderAsync(DeleteOrderRequest request)
        {
            if (request?.Number == null) throw new ArgumentNullException();

            var entity = await _orderRepository.GetAsync(request.Number);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with {request.Number} is not present");
            }

            entity.IsInactive = true;

            _orderRepository.Update(entity);
            await _orderRepository.UnitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderResponse>(entity);
        }
    }
}
