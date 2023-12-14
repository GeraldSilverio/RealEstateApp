using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using System.Net;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Queries.GetTypeOfRealEstateById
{
    public class GetTypeOfRealEstateByIdQuery : IRequest<Response<TypeOfEstateDto>>
    {
        public int Id { get; set; }
    }
    public class GetTypeOfRealEstateByIdQueryHandler : IRequestHandler<GetTypeOfRealEstateByIdQuery, Response<TypeOfEstateDto>>
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        private readonly IMapper _mapper;

        public GetTypeOfRealEstateByIdQueryHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _mapper = mapper;
        }
        public async Task<Response<TypeOfEstateDto>> Handle(GetTypeOfRealEstateByIdQuery request, CancellationToken cancellationToken)
        {
            var typeOfRealEstate = _mapper.Map<TypeOfEstateDto>(await _typeOfRealEstateRepository.GetByIdAsync(request.Id));

            if (typeOfRealEstate is null) throw new ApiException("Type of Real Estate not found",(int)HttpStatusCode.NoContent);

            return new Response<TypeOfEstateDto>(typeOfRealEstate);
        }
    }
}
