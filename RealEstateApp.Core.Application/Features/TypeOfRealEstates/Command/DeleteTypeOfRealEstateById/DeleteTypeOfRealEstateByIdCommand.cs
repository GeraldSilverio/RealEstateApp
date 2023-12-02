using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.DeleteTypeOfRealEstateById
{
    /// <summary>
    /// Parametros para eliminar un tipo de propiedad
    /// </summary>
    public class DeleteTypeOfRealEstateByIdCommand : IRequest
    {
        [SwaggerParameter(Description = "ID del tipo de propiedad que desea eliminar")]
        public int Id { get; set; }
    }
    public class DeleteTypeOfRealEstateByIdCommandHandler : IRequestHandler<DeleteTypeOfRealEstateByIdCommand>
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;

        public DeleteTypeOfRealEstateByIdCommandHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
        }

        public async Task<Unit> Handle(DeleteTypeOfRealEstateByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfRealEstate = await _typeOfRealEstateRepository.GetByIdAsync(command.Id);
            if (typeOfRealEstate is null) throw new Exception("Type of Real Estate not found");
            await _typeOfRealEstateRepository.DeleteAsync(typeOfRealEstate);
            return Unit.Value;
        }
    }
}
