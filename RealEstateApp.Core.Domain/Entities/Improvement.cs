using RealEstateApp.Core.Domain.Commons;

namespace RealEstateApp.Core.Domain.Entities
{
    public class Improvement:BaseEntity
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public ICollection<RealEstateImprovements> RealEstateImprovements { get; set; } = null!;


    }
}
