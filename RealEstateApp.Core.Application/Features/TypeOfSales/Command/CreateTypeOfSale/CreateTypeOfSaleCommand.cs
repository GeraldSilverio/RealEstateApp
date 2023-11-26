using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using System.Text.Json.Serialization;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Command.CreateTypeOfSale
{
    public class CreateTypeOfSaleCommand : IRequest<int>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    public class CreateTypeOfSaleCommandHandler : IRequestHandler<CreateTypeOfSaleCommand, int>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;
        private readonly IMapper _mapper;

        public CreateTypeOfSaleCommandHandler(ITypeOfSaleRepository typeOfSaleRepository, IMapper mapper)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
            _mapper = mapper;
        }
        public async Task<int> Handle(CreateTypeOfSaleCommand command, CancellationToken cancellationToken)
        {
            var typeOfSale = _mapper.Map<TypeOfSale>(command);
            typeOfSale = await _typeOfSaleRepository.AddAsync(typeOfSale);
            return typeOfSale.Id;
        }
    }
}
