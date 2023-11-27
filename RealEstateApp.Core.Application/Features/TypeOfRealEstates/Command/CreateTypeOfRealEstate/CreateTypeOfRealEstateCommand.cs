using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using System.Text.Json.Serialization;

namespace RealEstateApp.Core.Application.Features.TypeOfRealEstates.Command.CreateTypeOfRealEstate
{
    public class CreateTypeOfRealEstateCommand:IRequest
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    public class CreateTypeOfRealEstateCommandHandler : IRequestHandler<CreateTypeOfRealEstateCommand>
    {
        private readonly ITypeOfRealEstateRepository _typeOfRealEstateRepository;
        private readonly IMapper _mapper;

        public CreateTypeOfRealEstateCommandHandler(ITypeOfRealEstateRepository typeOfRealEstateRepository, IMapper mapper)
        {
            _typeOfRealEstateRepository = typeOfRealEstateRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(CreateTypeOfRealEstateCommand commnand, CancellationToken cancellationToken)
        {
            var typeOfRealEstate = _mapper.Map<TypeOfRealEstate>(commnand);
            await _typeOfRealEstateRepository.AddAsync(typeOfRealEstate);
            return Unit.Value;
        }
    }
}
