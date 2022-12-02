namespace BusinessSolutions.Services.Ordering.Infrastructure.Tests
{
    public class ProviderRepositoryTests : IClassFixture<OrderingContextFactory>
    {
        private readonly TestOrderingContext _context;
        private readonly ProviderRepository _sut;
        private readonly int _providerId = int.MaxValue;

        public ProviderRepositoryTests(OrderingContextFactory orderingContextFactory)
        {
            _context = orderingContextFactory.ContextInstance;
            _sut = new ProviderRepository(_context);
        }

        [Fact]
        public async Task should_get_data()
        {
            var result = await _sut.GetAsync();

            result.ShouldNotBeNull();
        }

        [Theory]
        [LoadData("provider")]
        public async Task should_return_record_by_id(Provider provider)
        {
            var result = await _sut.GetAsync(provider.ProviderId);

            result.ShouldNotBeNull();
            result.ProviderId.ShouldBe(provider.ProviderId);
            result.ProviderName.ShouldBe(provider.ProviderName);
        }

        [Theory]
        [LoadData("provider")]
        public async Task should_add_new_provider(Provider provider)
        {
            provider.ProviderId = default;

            _sut.Add(provider);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.Providers
                .FirstOrDefault(x => x.ProviderId == provider.ProviderId)
                .ShouldNotBeNull();
        }

        [Fact]
        public async Task should_return_null_with_id_not_present()
        {
            var result = await _sut.GetAsync(_providerId);

            result.ShouldBeNull();
        }

        [Fact]
        public async Task should_return_not_exist_with_id_not_present()
        {
            var result = await _sut.ExistsAsync(_providerId);

            result.ShouldBeFalse();
        }

        [Theory]
        [LoadData("provider")]
        public async Task should_delete_provider(Provider provider)
        {
            var result = _sut.Delete(provider);
            await _sut.UnitOfWork.SaveEntitiesAsync();

            _context.Providers
                .FirstOrDefault(x => x.ProviderId == result.ProviderId)?
                .ShouldBeNull();
        }
    }
}
