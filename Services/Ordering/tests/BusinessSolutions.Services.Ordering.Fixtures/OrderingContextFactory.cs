using AutoMapper;
using BusinessSolutions.Services.Ordering.Domain.Mappers;
using BusinessSolutions.Services.Ordering.Infrastructure;

namespace BusinessSolutions.Services.Ordering.Fixtures
{
    public class OrderingContextFactory
    {
        public readonly TestOrderingContext ContextInstance;
        public readonly IMapper Mapper;

        public OrderingContextFactory()
        {
            var contextOptions = new DbContextOptionsBuilder<OrderingContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .EnableSensitiveDataLogging()
                .Options;

            EnsureCreation(contextOptions);

            ContextInstance = new TestOrderingContext(contextOptions);

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderingProfile>();
            });
            Mapper = mapperConfig.CreateMapper();
        }

        private void EnsureCreation(DbContextOptions<OrderingContext> contextOptions)
        {
            using var context = new TestOrderingContext(contextOptions);
            context.Database.EnsureCreated();
        }
    }
}
