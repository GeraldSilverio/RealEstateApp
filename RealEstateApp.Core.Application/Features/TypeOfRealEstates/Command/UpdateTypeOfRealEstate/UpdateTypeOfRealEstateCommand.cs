using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using System.Text.Json.Serialization;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.UpdateTypeOfRealEstate
{
    public class UpdateTypeOfRealEstateCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
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
            if (typeOfRealEstate is null) throw new Exception("Type of Real Estate not found");
            //Mapeamos el command que enviamos desde la API a la entidadad TypeOfRealEstate.
            typeOfRealEstate = _mapper.Map<TypeOfRealEstate>(command);
            //Actualizamos el registro.
            await _typeOfRealEstateRepository.UpdateAsync(typeOfRealEstate, typeOfRealEstate.Id);
            return Unit.Value;
        }
    }
}
