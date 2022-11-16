using BusinessSolutions.Services.Ordering.Domain.Entities;
using BusinessSolutions.Services.Ordering.Domain.Repositories;
using BusinessSolutions.Services.Ordering.Infrastructure.SchemaDefinitions;
using Microsoft.EntityFrameworkCore;


namespace BusinessSolutions.Services.Ordering.Infrastructure
{
    public class OrderingContext : DbContext, IUnItOfWork
    {
        public const string DEFAULT_SCHEMA = "ordering";

        public DbSet<Item> Items => Set<Item>();
        public DbSet<Provider> Providers => Set<Provider>();
        public DbSet<Order> Orders => Set<Order>();

        public OrderingContext(DbContextOptions<OrderingContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ItemEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new ProviderEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new OrderEntitySchemaDefinition());
            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
