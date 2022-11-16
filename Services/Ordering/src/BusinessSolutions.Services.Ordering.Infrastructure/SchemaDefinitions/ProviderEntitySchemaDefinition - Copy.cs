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
        }
    }
}
