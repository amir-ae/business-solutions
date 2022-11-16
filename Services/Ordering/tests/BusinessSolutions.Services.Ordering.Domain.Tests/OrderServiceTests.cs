using AutoMapper;
using BusinessSolutions.Services.Ordering.Domain.Entities;
using BusinessSolutions.Services.Ordering.Domain.Requests.Order;
using BusinessSolutions.Services.Ordering.Domain.Services;
using BusinessSolutions.Services.Ordering.Infrastructure.Repositories;

namespace BusinessSolutions.Services.Ordering.Domain.Tests
{
    public class OrderServiceTests : IClassFixture<OrderingContextFactory>
    {
        private readonly OrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly int _orderId = int.MaxValue;
        public OrderServiceTests(OrderingContextFactory orderingContextFactory)
        {
            _orderRepository = new OrderRepository(orderingContextFactory.ContextInstance);
            _mapper = orderingContextFactory.Mapper;
        }

        [Fact]
        public async Task get_orders_should_return_right_data()
        {
            OrderService sut = new OrderService(_orderRepository, _mapper);

            var result = await sut.GetOrdersAsync();
            result.ShouldNotBeNull();
        }

        [Theory]
        [LoadData("order")]
        public async Task get_orders_by_filter_should_return_right_data(Order order)
        {
            OrderService sut = new OrderService(_orderRepository, _mapper);

            var request = new GetOrdersByFilterRequest
            {
                ProviderId = order.ProviderId,
                ProviderName = order.Provider!.ProviderName,
                StartTime = order.Date.AddDays(-1),
                EndTime = order.Date.AddDays(1),
                ItemName = order.Items!.First().Name,
                ItemUnit = order.Items!.First().Unit,
            };

            var result = await sut.GetOrdersByFilterAsync(request);

            result.All(i => i.ProviderId == request.ProviderId).ShouldBeTrue();
            result.All(i => i.Provider!.ProviderName == request.ProviderName).ShouldBeTrue();
            result.All(i => DateTimeOffset.Compare((DateTimeOffset)request.StartTime, i.Date) < 0
                    && DateTimeOffset.Compare((DateTimeOffset)request.EndTime, i.Date) > 0).ShouldBeTrue();
            result.All(i => i.Items!.Any(x => x.Name == request.ItemName)).ShouldBeTrue();
            result.All(i => i.Items!.Any(x => x.Unit == request.ItemUnit)).ShouldBeTrue();
        }

        [Theory]
        [LoadData("order")]
        public async Task get_orders_by_partial_filter_should_return_right_data(Order order)
        {
            OrderService sut = new OrderService(_orderRepository, _mapper);

            var request = new GetOrdersByFilterRequest
            {
                ProviderName = order.Provider!.ProviderName,
                EndTime = order.Date.AddDays(1),
                ItemUnit = order.Items!.First().Unit,
            };

            var result = await sut.GetOrdersByFilterAsync(request);

            result
                .All(i => i.Provider!.ProviderName == request.ProviderName).ShouldBeTrue();
            result
                .All(i => DateTimeOffset.Compare((DateTimeOffset)request.EndTime, i.Date) > 0)
                .ShouldBeTrue();
            result
                .All(i => i.Items!.Any(x => x.Unit == request.ItemUnit)).ShouldBeTrue();
        }

        [Theory]
        [InlineData("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6")]
        public async Task get_order_should_return_right_data(string number)
        {
            OrderService sut = new OrderService(_orderRepository, _mapper);

            var result = await sut.GetOrderAsync(new GetOrderRequest { Number = number });

            result
                .ShouldNotBeNull()
                .Number.ShouldBe(number);
        }

        [Fact]
        public void get_order_should_thrown_exception_with_null_id()
        {
            OrderService sut = new OrderService(_orderRepository, _mapper);

            sut.GetOrderAsync(null!).ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public async Task should_return_not_exist_with_id_not_present()
        {
            OrderService sut = new OrderService(_orderRepository, _mapper);

            var result = await sut.CheckOrderAsync(new GetOrderRequest { Id = _orderId });

            result.ShouldBeFalse();
        }

        [Theory]
        [LoadData("order")]
        public async Task add_order_should_add_right_entity(AddOrderRequest request)
        {
            IOrderService sut = new OrderService(_orderRepository, _mapper);

            var result = await sut.AddOrderAsync(request);

            result.ShouldNotBeNull();
            result.Number.ShouldBe(request.Number);
            result.ProviderId.ShouldBe(request.ProviderId);
        }

        [Theory]
        [LoadData("order")]
        public async Task edit_order_should_add_right_entity(EditOrderRequest request)
        {
            OrderService sut = new OrderService(_orderRepository, _mapper);

            var result = await sut.EditOrderAsync(request);

            result.ShouldNotBeNull();
            result.Number.ShouldBe(request.Number);
            result.ProviderId.ShouldBe(request.ProviderId);
        }

        [Theory]
        [LoadData("order")]
        public async Task delete_order_should_return_right_entity(DeleteOrderRequest request)
        {
            OrderService sut = new OrderService(_orderRepository, _mapper);

            var result = await sut.DeleteOrderAsync(request);

            result.ShouldNotBeNull();
            result.IsInactive.ShouldBeTrue();
        }
    }
}
