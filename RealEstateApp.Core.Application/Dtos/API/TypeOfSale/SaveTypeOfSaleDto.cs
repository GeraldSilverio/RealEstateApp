using System.Text.Json.Serialization;

namespace RealEstateApp.Core.Application.Dtos.API.TypeOfSale
{
    public class SaveTypeOfSaleDto
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
}
