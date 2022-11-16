using BusinessSolutions.Services.Ordering.Fixtures.Extensions;
using BusinessSolutions.Services.Ordering.Infrastructure;

namespace BusinessSolutions.Services.Ordering.Fixtures
{
    public class TestOrderingContext : OrderingContext
    {
        public TestOrderingContext(DbContextOptions<OrderingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Seed<Item>("./Data/item.json");
            modelBuilder.Seed<Provider>("./Data/provider.json");
            modelBuilder.Seed<Order>("./Data/order.json");
        }
    }
}
