using RealEstateApp.Core.Domain.Commons;

namespace RealEstateApp.Core.Domain.Entities
{
    public class TypeOfRealEstate:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public ICollection<RealEstate> RealEstates { get; set; } = null!;
    }
}
