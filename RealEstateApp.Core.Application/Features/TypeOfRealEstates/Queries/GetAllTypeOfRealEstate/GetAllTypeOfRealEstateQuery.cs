using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Queries.GetAllTypeOfRealEstate
{
    public class GetAllTypeOfRealEstateQuery : IRequest<IEnumerable<TypeOfEstateDto>>
    {
    }
    public class GetAllTypeOfRealEstateQueryHandler : IRequestHandler<GetAllTypeOfRealEstateQuery, IEnumerable<TypeOfEstateDto>
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        private readonly IMapper _mapper;

        public GetAllTypeOfRealEstateQueryHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TypeOfEstateDto>> Handle(GetAllTypeOfRealEstateQuery request, CancellationToken cancellationToken)
        {
            var typeOfRealEstates = _mapper.Map<List<TypeOfEstateDto>>(await _typeOfRealEstateRepository.GetAllAsync());
            if (typeOfRealEstates.Count is 0) throw new Exception("Type of Real Estates not found");
            return typeOfRealEstates;
        }
    }
}
