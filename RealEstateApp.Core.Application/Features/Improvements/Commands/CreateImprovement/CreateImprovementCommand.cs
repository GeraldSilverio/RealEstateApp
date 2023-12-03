using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Text.Json.Serialization;

namespace RealEstateApp.Core.Application.Features.Improvements.Commands.CreateImprovement
{
    /// <summary>
    /// Parametros para la creacion de una mejora.
    /// </summary>
    public class CreateImprovementCommand : IRequest<Response<int>>
    {

        ///<example>Baño</example>
        [SwaggerParameter(Description = "El nombre de la mejora")]
        public string Name { get; set; } = null!;
        ///<example>Baño en marmol</example>
        [SwaggerParameter(Description = "La descripcion de la mejora")]
        public string Description { get; set; } = null!;
    }

    public class CreateImprovementCommandHandler : IRequestHandler<CreateImprovementCommand, Response<int>>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public CreateImprovementCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateImprovementCommand command, CancellationToken cancellationToken)
        {
            var imrproment = _mapper.Map<Improvement>(command);
            imrproment = await _improvementRepository.AddAsync(imrproment);
            return new Response<int>(imrproment.Id);
        }
    }
}
