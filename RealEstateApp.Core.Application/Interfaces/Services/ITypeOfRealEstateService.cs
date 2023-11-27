using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.ViewModel.TypeOfRealState;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ITypeOfRealEstateService:IGenericService<TypeOfRealEstate,SaveTypeOfRealStateViewModel,TypeOfRealStateViewModel>
    {
    }
}
