using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using System.Net;

namespace RealEstateApp.Core.Application.Features.Improvements.Queries.GetAllImprovements
{
    /// <summary>
    /// Listar todas las mejoras
    /// </summary>
    public class GetAllImprovementsQuery : IRequest<Response<IEnumerable<ImprovementDto>>>
    {
    }

    public class GetAllImprovementsQueryHandler : IRequestHandler<GetAllImprovementsQuery, Response<IEnumerable<ImprovementDto>>> 
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public GetAllImprovementsQueryHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<ImprovementDto>>> Handle(GetAllImprovementsQuery request, CancellationToken cancellationToken)
        {
            var improvementsDto = _mapper.Map<List<ImprovementDto>>(await _improvementRepository.GetAllAsync());

            if (improvementsDto.Count == 0) throw new ApiException("Improvements not found",(int)HttpStatusCode.NoContent);

            return new Response<IEnumerable<ImprovementDto>>(improvementsDto);
        }
    }
}
