using RealEstateApp.Core.Domain.Commons;

namespace RealEstateApp.Core.Domain.Entities
{
    public class RealEstate : BaseEntity
    {
        public string IdAgent { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int BathRooms { get; set; }
        public int BedRooms { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int IdTypeOfSale { get; set; }
        public TypeOfSale TypeOfSale { get; set; } = null!;
        public int IdTypeOfRealEstate { get; set; }
        public TypeOfRealEstate TypeOfRealEstate { get; set; } = null!;

        public ICollection<RealEstateImprovements> RealEstateImprovements { get; set; } = null!;
    }
}
