using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.UpdateTypeOfRealEstate
{
    /// <summary>
    /// Parametros para modificar un tipo de propiedad.
    /// </summary>
    public class UpdateTypeOfRealEstateCommand : IRequest
    {
        [SwaggerParameter(Description = "ID del tipo de propiedad que desea modificar")]
        public int Id { get; set; }
        [SwaggerParameter(Description = "El nuevo nombre para el tipo de propiedad que desea modificar")]
        public string Name { get; set; } = null!;
        [SwaggerParameter(Description = "Una nueva descripcion para el tipo de propiedad que desea modificar")]
        public string Description { get; set; } = null!;
    }
    public class UpdateTypeOfRealEstateCommandHandler : IRequestHandler<UpdateTypeOfRealEstateCommand>
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        private readonly IMapper _mapper;

        public UpdateTypeOfRealEstateCommandHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateTypeOfRealEstateCommand command, CancellationToken cancellationToken)
        {
            //Obtenemos el registro.
            var typeOfRealEstate = await _typeOfRealEstateRepository.GetByIdAsync(command.Id);
            //Validamos que no sea nulo.
            if (typeOfRealEstate is null)
            {
                throw new Exception("Type of Real Estate not found");
            }
            //Mapeamos el command que enviamos desde la API a la entidadad TypeOfRealEstate.
            typeOfRealEstate = _mapper.Map<TypeOfRealEstate>(command);
            //Actualizamos el registro.
            await _typeOfRealEstateRepository.UpdateAsync(typeOfRealEstate, typeOfRealEstate.Id);

            return Unit.Value;
        }
    }
}
