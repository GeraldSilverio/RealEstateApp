using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.ViewModel.Improvement;
using RealEstateApp.Core.Application.ViewModel.TypeOfRealState;
using RealEstateApp.Core.Application.ViewModel.TypeOfSale;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModel.RealEstate
{
    public class SaveRealEstateViewModel
    {
        #region Priority Properties
        public int Id { get; set; }
        public string IdAgent { get; set; } = null!;
        public string? Code { get; set; }
        public string Address { get; set; }
        public int BathRooms { get; set; }
        public int BedRooms { get; set; }
        public int Size { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        #endregion

        #region Navagation Properties

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una tipo de venta")]
        public int IdTypeOfSale { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una tipo de propiedad")]
        public int IdTypeOfRealEstate { get; set; }

        public List<int>? PropertiesImprovementsId { get; set; }

        #endregion

        #region Data For View
        public List<TypeOfSaleViewModel>? TypeOfSale { get; set; } = null!;
        public List<TypeOfRealStateViewModel>? TypeOfRealEstate { get; set; } = null!;
        public List<ImprovementViewModel>? Improvements { get; set; } = null!;
        #endregion

        #region  Images Configuration
        public string? Images { get; set; }
        [DataType(DataType.Upload)]
        public List<IFormFile>? Files { get; set; }

        #endregion

        #region Error Handling

        public bool HasError { get; set; }
        public string? Error { get; set; }


        #endregion
    }
}
