using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Improvements.Commands.UpdateImprovement
{
    /// <summary>
    /// Parametros para la actualizacion de una mejora.
    /// </summary>
    public class UpdateImprovementCommand:IRequest<SaveImprovementDto>
    {
        [SwaggerParameter(Description = "Id de la mejora")]
        public int Id { get; set; }
       
        [SwaggerParameter(Description = "El nuevo nombre de la mejora")]
        public string Name { get; set; } = null!;
        
        [SwaggerParameter(Description = "La nueva descripcion de la mejora")]
        public string Description { get; set; } = null!;
    }

    public class UpdateImprovementCommandHandler : IRequestHandler<UpdateImprovementCommand, SaveImprovementDto>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public UpdateImprovementCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<SaveImprovementDto> Handle(UpdateImprovementCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _improvementRepository.GetByIdAsync(command.Id);
            if (improvement is null) throw new Exception("Improvement not found");

            improvement = _mapper.Map<Improvement>(command);

            await _improvementRepository.UpdateAsync(improvement, improvement.Id);
            
            var improvementResponse = _mapper.Map<SaveImprovementDto>(improvement);

            return improvementResponse;
        }
    }
}
