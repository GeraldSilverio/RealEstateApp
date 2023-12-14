using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.DeleteTypeOfRealEstateById
{
    /// <summary>
    /// Parametros para eliminar un tipo de propiedad
    /// </summary>
    public class DeleteTypeOfRealEstateByIdCommand : IRequest<Response<Unit>>
    {
        [SwaggerParameter(Description = "ID del tipo de propiedad que desea eliminar")]
        public int Id { get; set; }
    }
    public class DeleteTypeOfRealEstateByIdCommandHandler : IRequestHandler<DeleteTypeOfRealEstateByIdCommand, Response<Unit>>
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;

        public DeleteTypeOfRealEstateByIdCommandHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteTypeOfRealEstateByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfRealEstate = await _typeOfRealEstateRepository.GetByIdAsync(command.Id);
            if (typeOfRealEstate is null) throw new ApiException("Type of Real Estate not found", (int)HttpStatusCode.NoContent);
            await _typeOfRealEstateRepository.DeleteAsync(typeOfRealEstate);
            return new Response<Unit>(Unit.Value);
        }
    }
}
