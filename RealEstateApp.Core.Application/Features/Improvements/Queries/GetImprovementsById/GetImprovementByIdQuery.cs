using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Improvements.Queries.GetImprovementsById
{
    /// <summary>
    /// Parametros para obtener una mejora por Id.
    /// </summary>
    public class GetImprovementByIdQuery : IRequest<ImprovementDto>
    {
        /// <example>
        /// 1
        /// </example>
        [SwaggerParameter(Description ="Debe colocar el id de la mejora que quiere obtener")]
       
        public int Id { get; set; }
    }

    public class GetImprovementByIdQueryHandle : IRequestHandler<GetImprovementByIdQuery, ImprovementDto>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public GetImprovementByIdQueryHandle(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<ImprovementDto> Handle(GetImprovementByIdQuery request, CancellationToken cancellationToken)
        {
            var improvement = _mapper.Map<ImprovementDto>(await _improvementRepository.GetByIdAsync(request.Id));
            return improvement;
        }

    }
}
