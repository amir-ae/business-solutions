using BusinessSolutions.Services.Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessSolutions.Services.Ordering.Infrastructure.SchemaDefinitions
{
    public class OrderEntitySchemaDefinition : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders", OrderingContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Number)
                .IsRequired();

            builder.Property(p => p.ProviderId)
                .IsRequired();

            builder.HasData(
                 new Order() { Id = 1, Number = RandomNumber(), Date = RandomDate(), ProviderId = 1 },
                 new Order() { Id = 2, Number = RandomNumber(), Date = RandomDate(), ProviderId = 2 },
                 new Order() { Id = 3, Number = RandomNumber(), Date = RandomDate(), ProviderId = 3 }
            );
        }

        string RandomNumber()
        {
            return Guid.NewGuid().ToString();
        }

        DateTime RandomDate()
        {
            DateTime start = new DateTime(2010, 1, 1);
            int range = (DateTime.Today - start).Days;
            return start.AddDays(new Random().Next(range));
        }
    }
}
