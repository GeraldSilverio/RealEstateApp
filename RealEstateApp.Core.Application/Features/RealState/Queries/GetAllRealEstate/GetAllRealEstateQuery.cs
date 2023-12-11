using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.RealState;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using System.Net;

namespace RealEstateApp.Core.Application.Features.RealState.Queries.GetAllRealState
{
    public class GetAllRealEstateQuery : IRequest<Response<IEnumerable<RealEstateDto>>>
    {
    }

    public class GetAllRealEstateQueryHandler : IRequestHandler<GetAllRealEstateQuery, Response<IEnumerable<RealEstateDto>>>
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IMapper _mapper;

        public GetAllRealEstateQueryHandler(IRealEstateRepository realEstateRepository, IMapper mapper)
        {
            _realEstateRepository = realEstateRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<RealEstateDto>>> Handle(GetAllRealEstateQuery request, CancellationToken cancellationToken)
        {
            var realStateDto = _mapper.Map<List<RealEstateDto>>(await _realEstateRepository.GetAllAsync());

            if (realStateDto.Count == 0) throw new ApiException("RealState not found", (int)HttpStatusCode.NotFound);

            return new Response<IEnumerable<RealEstateDto>>(realStateDto);
        }
    }
}
