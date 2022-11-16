namespace BusinessSolutions.Services.Ordering.Infrastructure.Tests
{
    public class OrderRepositoryTests : IClassFixture<OrderingContextFactory>
    {
        private readonly TestOrderingContext _context;
        private readonly OrderRepository _sut;
        private readonly int _orderId = int.MaxValue;

        public OrderRepositoryTests(OrderingContextFactory orderingContextFactory)
        {
            _context = orderingContextFactory.ContextInstance;
            _sut = new OrderRepository(_context);
        }

        [Fact]
        public async Task should_get_data()
        {
            var result = await _sut.GetAsync();

            result.ShouldNotBeNull();
        }

        [Theory]
        [InlineData("996e7d38-7d07-4753-c92b-08dac4d5819c")]
        public async Task getitems_should_not_return_inactive_records(string id)
        {
            var result = await _sut.GetAsync();

            result.Any(x => x.Number == id).ShouldBeFalse();
        }

        [Theory]
        [LoadData("order")]
        public async Task should_return_record_by_number(Order order)
        {
            var result = await _sut.GetAsync(order.Number!);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(order.Id);
            result.Number.ShouldBe(order.Number);
        }

        [Theory]
        [LoadData("order")]
        public async Task should_return_record_by_id(Order order)
        {
            var result = await _sut.GetByIdAsync(order.Id);

            result.ShouldNotBeNull();
            result.Id.ShouldBe(order.Id);
            result.Number.ShouldBe(order.Number);
        }

        [Fact]
        public async Task should_return_null_with_number_not_present()
        {
            var result = await _sut.GetAsync(Guid.NewGuid().ToString());

            result.ShouldBeNull();
        }

        [Fact]
        public async Task should_return_null_with_id_not_present()
        {
            var result = await _sut.GetByIdAsync(_orderId);

            result.ShouldBeNull();
        }

        [Fact]
        public async Task should_return_not_exist_with_id_not_present()
        {
            var result = await _sut.CheckByIdAsync(_orderId);

            result.ShouldBeFalse();
        }

        [Fact]
        public async Task should_return_not_exist_with_number_not_present()
        {
            var result = await _sut.CheckAsync(new Guid().ToString());

            result.ShouldBeFalse();
        }

        [Theory]
        [LoadData("order")]
        public async Task should_return_records_by_filter(Order order)
        {
            var providerId = order.ProviderId;
            var providerName = order.Provider!.ProviderName;
            var orderNumber = order.Number!.Substring(0, 3);
            var startTime = order.Date.AddDays(-1);
            var endTime = order.Date.AddDays(1);
            var itemName = order.Items!.First().Name;
            var itemUnit = order.Items!.First().Unit;
            

            var result = await _sut.GetByFilterAsync(providerId, providerName, 
                orderNumber, startTime, endTime, itemName, itemUnit);

            result
                .All(i => i.ProviderId == providerId).ShouldBeTrue();
            result
                .All(i => i.Provider!.ProviderName == providerName).ShouldBeTrue();
            result
                .All(i => i.Number!.StartsWith(orderNumber)).ShouldBeTrue();
            result
                .All(i => DateTimeOffset.Compare(startTime, i.Date) < 0
                    && DateTimeOffset.Compare(endTime, i.Date) > 0)
                .ShouldBeTrue();
            result
                .All(i => i.Items!.Any(x => x.Name == itemName)).ShouldBeTrue();
            result
                .All(i => i.Items!.Any(x => x.Unit == itemUnit)).ShouldBeTrue();
        }

        [Theory]
        [LoadData("order")]
        public async Task should_return_records_by_partial_filter(Order order)
        {
            var startTime = order.Date.AddDays(-1);
            var itemName = order.Items!.First().Name;
            var providerId = order.ProviderId;

            var result = await _sut.GetByFilterAsync(providerId, null, null, startTime, null, itemName, null);

            result
                .All(i => i.ProviderId == providerId).ShouldBeTrue();
            result
                .All(i => DateTimeOffset.Compare(startTime, i.Date) < 0).ShouldBeTrue();
            result
                .All(i => i.Items!.Any(x => x.Name == itemName)).ShouldBeTrue();
        }

        [Theory]
        [LoadData("order")]
        public async Task should_add_new_order(Order order)
        {
            var entity = new Order
            {
                Id = default,
                Number = order.Number,
                Date = order.Date,
                ProviderId = order.ProviderId
            };

            _sut.Add(entity);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.Orders
                .FirstOrDefault(x => x.Id == entity.Id)
                .ShouldNotBeNull();
        }

        [Theory]
        [LoadData("order")]
        public async Task should_update_order(Order order)
        {
            var number = Guid.NewGuid().ToString();
            order.Number = number;

            _sut.Update(order);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.Orders
                .FirstOrDefault(x => x.Number == number)
                .ShouldNotBeNull();
        }
    }
}
