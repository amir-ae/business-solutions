using BusinessSolutions.Services.Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessSolutions.Services.Ordering.Infrastructure.SchemaDefinitions
{
    public class ItemEntitySchemaDefinition : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            builder.ToTable("Items", OrderingContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.ItemId);

            builder.Property(p => p.OrderId)
                .IsRequired();

            builder.Property(p => p.Name)
                .IsRequired();

            builder.Property(p => p.Quantity)
                .IsRequired();

            builder.Property(p => p.Unit)
                .IsRequired();

            builder.HasData(
                new Item() { ItemId = 1, OrderId = 1, Name = "Green Apples", Quantity = 1, Unit = "Kilogram" },
                new Item() { ItemId = 2, OrderId = 1, Name = "Lifejacket", Quantity = 2, Unit = "Item" },
                new Item() { ItemId = 3, OrderId = 1, Name = "Soccer Ball", Quantity = 2, Unit = "Item" },
                new Item() { ItemId = 4, OrderId = 2, Name = "Chess Board", Quantity = 1, Unit = "Item" },
                new Item() { ItemId = 5, OrderId = 2, Name = "Red Fabric", Quantity = 5, Unit = "Meter" },
                new Item() { ItemId = 6, OrderId = 3, Name = "GOOD KID, m.A.A.d CITY", Quantity = 1, Unit = "Item" },
                new Item() { ItemId = 7, OrderId = 3, Name = "Organic Milk", Quantity = 3, Unit = "Liter" }
            );
        }
    }
}
