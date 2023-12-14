using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.Improvements.Queries.GetImprovementsById
{
    /// <summary>
    /// Parametros para obtener una mejora por Id.
    /// </summary>
    public class GetImprovementByIdQuery : IRequest<Response<ImprovementDto>>
    {
        /// <example>
        /// 1
        /// </example>
        [SwaggerParameter(Description = "Debe colocar el id de la mejora que quiere obtener")]

        public int Id { get; set; }
    }

    public class GetImprovementByIdQueryHandle : IRequestHandler<GetImprovementByIdQuery, Response<ImprovementDto>>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public GetImprovementByIdQueryHandle(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<Response<ImprovementDto>> Handle(GetImprovementByIdQuery request, CancellationToken cancellationToken)
        {
            var improvement = _mapper.Map<ImprovementDto>(await _improvementRepository.GetByIdAsync(request.Id));
            if (improvement is null) throw new ApiException("Improvements not found", (int)HttpStatusCode.NoContent);
            return new Response<ImprovementDto>(improvement);
        }

    }
}
