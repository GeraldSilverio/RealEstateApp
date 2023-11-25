using AutoMapper;
using RealEstateApp.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class TypeOfSaleService : GenericService<TypeOfSale, SaveTypeOfSaleDto, TypeOfSaleDto>, ITypeOfSaleService
    {
        public TypeOfSaleService(ITypeOfSaleRepository typeOfSaleRepository, IMapper mapper) : base(typeOfSaleRepository, mapper)
        {
        }
    }
}
