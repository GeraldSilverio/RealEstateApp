using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Core.Application.Features.Improvements.Queries.GetAllImprovements
{
    public class GetAllImprovementsQuery : IRequest<IEnumerable<ImprovementDto>>
    {
    }

    public class GetAllImprovementsQueryHanfler : IRequestHandler<GetAllImprovementsQuery, IEnumerable<ImprovementDto>>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public GetAllImprovementsQueryHanfler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ImprovementDto>> Handle(GetAllImprovementsQuery request, CancellationToken cancellationToken)
        {
            var improvementsDto = _mapper.Map<List<ImprovementDto>>(await _improvementRepository.GetAllAsync());
            if (improvementsDto.Count == 0) throw new Exception("Improvements not found");
            return improvementsDto;
        }
    }
}
