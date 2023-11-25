using AutoMapper;
using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class TypeOfRealEstateService : GenericService<TypeOfRealEstate, SaveTypeOfEstateDto, TypeOfEstateDto>, ITypeOfRealEstateService
    {
        public TypeOfRealEstateService(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper) : base(typeOfRealEstateRepository, mapper)
        {
        }
    }
}
