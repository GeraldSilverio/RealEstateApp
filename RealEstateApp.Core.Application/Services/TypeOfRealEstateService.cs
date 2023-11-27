using AutoMapper;
using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.TypeOfRealState;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class TypeOfRealEstateService : GenericService<TypeOfRealEstate, SaveTypeOfRealStateViewModel, TypeOfRealStateViewModel>, ITypeOfRealEstateService
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        private readonly IMapper _mapper;

        public TypeOfRealEstateService(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper) : base(typeOfRealEstateRepository, mapper)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _mapper = mapper;
        }
    }
}
