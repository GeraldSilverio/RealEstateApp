

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Infraestructure.Persistence.EntityConfigurations
{
    public class RealEstateConfiguration : IEntityTypeConfiguration<RealEstate>
    {
        public void Configure(EntityTypeBuilder<RealEstate> builder)
        {
            builder.ToTable("RealEstate");
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Code).IsUnique();

            builder.HasOne(x => x.TypeOfRealEstate)
                .WithMany(x => x.RealEstates)
                .HasForeignKey(x => x.IdTypeOfRealEstate)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(x => x.TypeOfSale)
                .WithMany(x => x.RealEstates)
                .HasForeignKey(x => x.IdTypeOfSale)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
