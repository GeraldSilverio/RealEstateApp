using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Improvements.Commands.DeleteImprovementById
{
    /// <summary>
    /// Parametros para la elimiacion de una mejora.
    /// </summary>
    public class DeleteImprovementByIdCommand : IRequest<int>
    {
        ///<example> 1</example>
        [SwaggerParameter(Description = "El id de la mejora que se quiere eliminar")]

        public int Id { get; set; }
    }
    public class DeleteImprovementByIdCommandHandler : IRequestHandler<DeleteImprovementByIdCommand, int>
    {
        private readonly IImprovementRepository _improvementRepository;
        private readonly IMapper _mapper;

        public DeleteImprovementByIdCommandHandler(IImprovementRepository improvementRepository, IMapper mapper)
        {
            _improvementRepository = improvementRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteImprovementByIdCommand command, CancellationToken cancellationToken)
        {
            var improvement = await _improvementRepository.GetByIdAsync(command.Id);
            if (improvement == null) throw new Exception("Improvement not found");
            await _improvementRepository.DeleteAsync(improvement);
            return improvement.Id;
        }
    }
}
