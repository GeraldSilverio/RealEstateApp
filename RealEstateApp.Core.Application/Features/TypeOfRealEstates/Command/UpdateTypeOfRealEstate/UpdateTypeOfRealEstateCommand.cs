using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.UpdateTypeOfRealEstate
{
    /// <summary>
    /// Parametros para modificar un tipo de propiedad.
    /// </summary>
    public class UpdateTypeOfRealEstateCommand : IRequest <Response<Unit>>
    {
        [SwaggerParameter(Description = "ID del tipo de propiedad que desea modificar")]
        public int Id { get; set; }
        [SwaggerParameter(Description = "El nuevo nombre para el tipo de propiedad que desea modificar")]
        public string Name { get; set; } = null!;
        [SwaggerParameter(Description = "Una nueva descripcion para el tipo de propiedad que desea modificar")]
        public string Description { get; set; } = null!;
    }
    public class UpdateTypeOfRealEstateCommandHandler : IRequestHandler<UpdateTypeOfRealEstateCommand, Response<Unit>>
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        private readonly IMapper _mapper;

        public UpdateTypeOfRealEstateCommandHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _mapper = mapper;
        }
        public async Task<Response<Unit>> Handle(UpdateTypeOfRealEstateCommand command, CancellationToken cancellationToken)
        {
            //Obtenemos el registro.
            var typeOfRealEstate = await _typeOfRealEstateRepository.GetByIdAsync(command.Id);
            //Validamos que no sea nulo.
            if (typeOfRealEstate is null)
            {
                throw new ApiException("Type of Real Estate not found",(int)HttpStatusCode.NotFound);
            }
            //Mapeamos el command que enviamos desde la API a la entidadad TypeOfRealEstate.
            typeOfRealEstate = _mapper.Map<TypeOfRealEstate>(command);
            //Actualizamos el registro.
            await _typeOfRealEstateRepository.UpdateAsync(typeOfRealEstate, typeOfRealEstate.Id);

            return new Response<Unit>(Unit.Value);
        }
    }
}
