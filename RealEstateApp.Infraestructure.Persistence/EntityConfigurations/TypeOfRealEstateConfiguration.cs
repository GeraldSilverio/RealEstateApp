using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Infraestructure.Persistence.EntityConfigurations
{
    public class TypeOfRealEstateConfiguration : IEntityTypeConfiguration<TypeOfRealEstate>
    {
        public void Configure(EntityTypeBuilder<TypeOfRealEstate> builder)
        {
            builder.ToTable("TypeOfRealEstate");
            builder.HasKey(x => x.Id);
        }
    }
}
