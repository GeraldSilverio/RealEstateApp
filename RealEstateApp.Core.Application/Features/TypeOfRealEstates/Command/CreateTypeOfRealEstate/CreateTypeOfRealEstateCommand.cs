using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.CreateTypeOfRealEstate
{
    /// <summary>
    /// Parametros para crear un tipo de propiedad.
    /// </summary>
    public class CreateTypeOfRealEstateCommand: IRequest <Response<Unit>>
    {
        [SwaggerParameter(Description ="El nombre del tipo de propiedad que desea crear")]
        public string Name { get; set; } = null!;
        [SwaggerParameter(Description = "Una descripcion tipo de propiedad que desea crear")]
        public string Description { get; set; } = null!;
    }
    public class CreateTypeOfRealEstateCommandHandler : IRequestHandler<CreateTypeOfRealEstateCommand, Response<Unit>>
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        private readonly IMapper _mapper;

        public CreateTypeOfRealEstateCommandHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _mapper = mapper;
        }
        public async Task<Response<Unit>> Handle(CreateTypeOfRealEstateCommand commnand, CancellationToken cancellationToken)
        {
            var typeOfRealEstate = _mapper.Map<TypeOfRealEstate>(commnand);
            await _typeOfRealEstateRepository.AddAsync(typeOfRealEstate);
            return new Response<Unit>(Unit.Value);
        }
    }
}
