using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Infraestructure.Persistence.EntityConfigurations
{
    public class RealEstateClientConfiguration : IEntityTypeConfiguration<RealEstateClient>
    {
        public void Configure(EntityTypeBuilder<RealEstateClient> builder)
        {
            builder.ToTable("RealEstateClient");
            builder.HasKey(x=>x.Id);   
            builder.Property(x=> x.CreatedBy).IsRequired(false);
        }
    }
}
