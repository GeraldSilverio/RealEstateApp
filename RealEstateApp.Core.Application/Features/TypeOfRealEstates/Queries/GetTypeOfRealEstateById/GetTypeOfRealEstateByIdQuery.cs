using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Queries.GetTypeOfRealEstateById
{
    public class GetTypeOfRealEstateByIdQuery : IRequest<TypeOfEstateDto>
    {
        public int Id { get; set; }
    }
    public class GetTypeOfRealEstateByIdQueryHandler : IRequestHandler<GetTypeOfRealEstateByIdQuery, TypeOfEstateDto>
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        private readonly IMapper _mapper;

        public GetTypeOfRealEstateByIdQueryHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _mapper = mapper;
        }
        public async Task<TypeOfEstateDto> Handle(GetTypeOfRealEstateByIdQuery request, CancellationToken cancellationToken)
        {
            var typeOfRealEstate = _mapper.Map<TypeOfEstateDto>(await _typeOfRealEstateRepository.GetByIdAsync(request.Id));
            if (typeOfRealEstate is null) throw new Exception("Type of Real Estate not found");
            return typeOfRealEstate;
        }
    }
}
