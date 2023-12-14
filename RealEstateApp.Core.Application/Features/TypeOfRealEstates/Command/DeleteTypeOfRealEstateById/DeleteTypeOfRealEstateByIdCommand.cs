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
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IRealEstateClientRepository _realEstateClientRepository;
        private readonly IRealEstateImageRepository _realEstateImageRepository;

        public DeleteTypeOfRealEstateByIdCommandHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IRealEstateRepository realEstateRepository, IRealEstateClientRepository realEstateClientRepository, IRealEstateImageRepository realEstateImageRepository)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _realEstateRepository = realEstateRepository;
            _realEstateClientRepository = realEstateClientRepository;
            _realEstateImageRepository = realEstateImageRepository;
        }

        public async Task<Response<Unit>> Handle(DeleteTypeOfRealEstateByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfRealEstate = await _typeOfRealEstateRepository.GetByIdAsync(command.Id);
            if (typeOfRealEstate is null) throw new ApiException("Type of Real Estate not found", (int)HttpStatusCode.NoContent);
            await DeleteRelationships(typeOfRealEstate.Id);
            await _typeOfRealEstateRepository.DeleteAsync(typeOfRealEstate);
            return new Response<Unit>(Unit.Value);
        }
        private async Task DeleteRelationships(int idTypeOfRealEstate)
        {
            var realEstate = await _realEstateRepository.GetRealEstateByTypeAsync(idTypeOfRealEstate);
            foreach (var item in realEstate)
            {
                await _realEstateClientRepository.RemoveAllByIdRealEstate(item);
                await _realEstateImageRepository.RemoveAll(item);
            }
        }
    }
}
