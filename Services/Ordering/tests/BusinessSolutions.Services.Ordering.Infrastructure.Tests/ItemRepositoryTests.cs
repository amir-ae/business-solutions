namespace Ordering.Infrastructure.Tests
{
    public class ItemRepositoryTests : IClassFixture<OrderingContextFactory>
    {
        private readonly TestOrderingContext _context;
        private readonly ItemRepository _sut;
        private readonly int _itemId = int.MaxValue;

        public ItemRepositoryTests(OrderingContextFactory catalogContextFactory)
        {
            _context = catalogContextFactory.ContextInstance;
            _sut = new ItemRepository(_context);
        }

        [Fact]
        public async Task should_get_data()
        {
            var result = await _sut.GetAsync();

            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task should_return_null_with_id_not_present()
        {
            var result = await _sut.GetAsync(_itemId);

            result.ShouldBeNull();
        }

        [Theory]
        [LoadData("item.ItemId")]
        public async Task should_return_record_by_id(int id)
        {
            var result = await _sut.GetAsync(id);

            result
                .ShouldNotBeNull()
                .ItemId.ShouldBe(id);
        }

        [Theory]
        [LoadData("item")]
        public async Task should_add_new_item(Item item)
        {
            item.ItemId = default;

            var result = _sut.Add(item);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.Items
                .FirstOrDefault(_ => _.ItemId == result.ItemId)
                .ShouldNotBeNull();
        }

        [Theory]
        [LoadData("item")]
        public async Task should_update_item(Item item)
        {
            item.Quantity = 5;

            var result = _sut.Update(item);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.Items
                .FirstOrDefault(x => x.ItemId == result.ItemId)?
                .Quantity.ShouldBe(5);
        }

        [Fact]
        public async Task should_return_not_exist_with_id_not_present()
        {
            var result = await _sut.CheckAsync(_itemId);

            result.ShouldBeFalse();
        }

        [Theory]
        [LoadData("item")]
        public async Task should_delete_item(Item item)
        {
            var result = _sut.Delete(item);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.Items
                .FirstOrDefault(x => x.ItemId == result.ItemId)?
                .ShouldBeNull();
        }
    }
}
