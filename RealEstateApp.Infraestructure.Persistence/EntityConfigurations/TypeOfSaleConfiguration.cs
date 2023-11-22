using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Infraestructure.Persistence.EntityConfigurations
{
    public class TypeOfSaleConfiguration : IEntityTypeConfiguration<TypeOfSale>
    {
        public void Configure(EntityTypeBuilder<TypeOfSale> builder)
        {
            builder.ToTable("TypeOfSale");
            builder.HasKey(x => x.Id);

        }
    }
}
