using RealEstateApp.Core.Domain.Commons;

namespace RealEstateApp.Core.Domain.Entities
{
    public class RealEstateImage:BaseEntity
    {
        public string Image { get; set; }
        public int IdRealEstate { get; set; } 
    }
}
