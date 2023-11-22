using RealEstateApp.Core.Domain.Commons;

namespace RealEstateApp.Core.Domain.Entities
{
    public class RealEstateImprovements:BaseEntity
    {
        public int IdRealEstate { get; set; }
        public int IdImprovement { get; set; }
        public RealEstate RealEstate { get; set; } = null!;
        public Improvement Improvement { get; set; } = null!;
    }
}
