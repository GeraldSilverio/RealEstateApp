using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Command.CreateTypeOfSale
{
    /// <summary>
    /// Parametros para crear un tipo de venta
    /// </summary>
    public class CreateTypeOfSaleCommand : IRequest<int>
    {
        
        [SwaggerParameter(Description = "El nombre del tipo de venta que desea crear")]
        public string Name { get; set; } = null!;
        [SwaggerParameter(Description = "Una descripcion del tipo de venta que desea crear")]
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
