using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using System.Net;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Queries.GetAllTypeOfRealEstate
{
    public class GetAllTypeOfRealEstateQuery : IRequest<Response<IEnumerable<TypeOfEstateDto>>>
    {
    }
    public class GetAllTypeOfRealEstateQueryHandler : IRequestHandler<GetAllTypeOfRealEstateQuery, Response<IEnumerable<TypeOfEstateDto>>> 
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        private readonly IMapper _mapper;

        public GetAllTypeOfRealEstateQueryHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<TypeOfEstateDto>>> Handle(GetAllTypeOfRealEstateQuery request, CancellationToken cancellationToken)
        {
            var typeOfRealEstates = _mapper.Map<List<TypeOfEstateDto>>(await _typeOfRealEstateRepository.GetAllAsync());

            if (typeOfRealEstates.Count is 0) throw new ApiException("Type of Real Estates not found",(int)HttpStatusCode.NoContent);

            return new Response<IEnumerable<TypeOfEstateDto>>(typeOfRealEstates);
        }
    }
}
