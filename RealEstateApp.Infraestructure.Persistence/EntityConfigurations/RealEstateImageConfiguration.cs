using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Infraestructure.Persistence.EntityConfigurations
{
    public class RealEstateImageConfiguration : IEntityTypeConfiguration<RealEstateImage>
    {
        public void Configure(EntityTypeBuilder<RealEstateImage> builder)
        {
            builder.ToTable("RealEstateImage");
            builder.HasKey(x => x.Id);
        }
    }
}
