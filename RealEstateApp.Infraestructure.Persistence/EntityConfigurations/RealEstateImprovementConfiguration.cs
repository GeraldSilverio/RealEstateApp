using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Infraestructure.Persistence.EntityConfigurations
{
    public class RealEstateImprovementConfiguration : IEntityTypeConfiguration<RealEstateImprovements>
    {
        public void Configure(EntityTypeBuilder<RealEstateImprovements> builder)
        {
            builder.ToTable("RealEstateImprovements");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Improvement).IsRequired(false);

            builder.HasOne(x => x.Improvement)
                .WithMany(x => x.RealEstateImprovements)
                .HasForeignKey(x => x.IdImprovement)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(x => x.RealEstate)
                .WithMany(x => x.RealEstateImprovements)
                .HasForeignKey(x => x.IdRealEstate)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
