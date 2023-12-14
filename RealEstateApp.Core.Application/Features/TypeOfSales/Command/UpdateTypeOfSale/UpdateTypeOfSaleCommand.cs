using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Command.UpdateTypeOfSale
{
    /// <summary>
    /// Parametros para modificar un tipo de venta
    /// </summary>
    public class UpdateTypeOfSaleCommand : IRequest <Response<Unit>>
    {
        [SwaggerParameter(Description = "ID del tipo de venta que desea modificar")]
        public int Id { get; set; }
        [SwaggerParameter(Description = "El nuevo nombre para el tipo de venta que desea modificar")]
        public string Name { get; set; } = null!;
        [SwaggerParameter(Description = "Una nueva descripcion para el tipo de venta que desea modificar")]
        public string Description { get; set; } = null!;
    }
    public class UpdateTypeOfSaleCommandHandler : IRequestHandler<UpdateTypeOfSaleCommand, Response<Unit>>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;
        private readonly IMapper _mapper;

        public UpdateTypeOfSaleCommandHandler(ITypeOfSaleRepository typeOfSaleRepository, IMapper mapper)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
            _mapper = mapper;
        }
        public async Task<Response<Unit>> Handle(UpdateTypeOfSaleCommand command, CancellationToken cancellationToken)
        {
            var typeOfSale = await _typeOfSaleRepository.GetByIdAsync(command.Id);

            if (typeOfSale is null) throw new ApiException("Type of sale not found",(int)HttpStatusCode.NoContent);

            typeOfSale = _mapper.Map<TypeOfSale>(command);
            await _typeOfSaleRepository.UpdateAsync(typeOfSale, typeOfSale.Id);
            return new Response<Unit>(Unit.Value);
        }
    }
}
