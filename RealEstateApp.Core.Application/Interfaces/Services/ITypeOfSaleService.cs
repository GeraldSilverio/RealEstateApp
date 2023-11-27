using RealEstateApp.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApp.Core.Application.ViewModel.TypeOfSale;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ITypeOfSaleService:IGenericService<TypeOfSale, SaveTypeOfSaleViewModel, TypeOfSaleViewModel>
    {
    }
}
