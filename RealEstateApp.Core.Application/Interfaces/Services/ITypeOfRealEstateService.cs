using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ITypeOfRealEstateService:IGenericService<TypeOfRealEstate,SaveTypeOfEstateDto,TypeOfEstateDto>
    {
    }
}
