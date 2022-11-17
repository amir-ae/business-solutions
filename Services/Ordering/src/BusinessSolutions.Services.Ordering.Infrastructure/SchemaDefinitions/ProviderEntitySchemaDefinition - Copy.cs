using BusinessSolutions.Services.Ordering.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BusinessSolutions.Services.Ordering.Infrastructure.SchemaDefinitions
{
    public class ProviderEntitySchemaDefinition : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.ToTable("Providers", OrderingContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.ProviderId);

            builder.Property(p => p.ProviderName)
                .IsRequired();

            builder.HasData(
                new Provider() { ProviderId = 1, ProviderName = "Considine-Bauch" },
                new Provider() { ProviderId = 2, ProviderName = "Herzog PLC" },
                new Provider() { ProviderId = 3, ProviderName = "Steuber, Considine and Hermann" },
                new Provider() { ProviderId = 4, ProviderName = "Klocko Group" },
                new Provider() { ProviderId = 5, ProviderName = "Stracke Group" }
            );
        }
    }
}
