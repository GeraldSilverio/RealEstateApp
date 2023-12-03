using RealEstateApp.Core.Application.ViewModel.Improvement;
using RealEstateApp.Core.Application.ViewModel.TypeOfRealState;
using RealEstateApp.Core.Application.ViewModel.TypeOfSale;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModel.RealEstate
{
    public class SaveRealEstateViewModel
    {
        public int Id {  get; set; }
        public string IdAgent { get; set; } = null!;
        public string Code { get; set; } = null!;
        public int BathRooms { get; set; }
        public int BedRooms { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una tipo de venta")]
        public int IdTypeOfSale { get; set; }
        public List<TypeOfSaleViewModel>? TypeOfSale { get; set; } = null!;
        public int IdTypeOfRealEstate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una tipo de propiedad")]
        public List<TypeOfRealStateViewModel>? TypeOfRealEstate { get; set; } = null!;

        public int PropertiesImprovementsId { get; set; }
        public List<ImprovementViewModel>? Improvements { get; set; } = null!;

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
