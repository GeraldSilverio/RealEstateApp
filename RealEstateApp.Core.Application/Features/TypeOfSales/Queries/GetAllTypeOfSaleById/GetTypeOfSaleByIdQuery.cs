using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Queries.GetAllTypeOfSaleById
{
    public class GetTypeOfSaleByIdQuery : IRequest<TypeOfSaleDto>
    {
        public int Id { get; set; }
    }
    public class GetTypeOfSaleByIdQueryHandler : IRequestHandler<GetTypeOfSaleByIdQuery, TypeOfSaleDto>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;
        private readonly IMapper _mapper;

        public GetTypeOfSaleByIdQueryHandler(ITypeOfSaleRepository typeOfSaleRepository, IMapper mapper)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
            _mapper = mapper;
        }
        public async Task<TypeOfSaleDto> Handle(GetTypeOfSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var typeOfSale = _mapper.Map<TypeOfSaleDto>(await _typeOfSaleRepository.GetByIdAsync(request.Id));
            if (typeOfSale is null) throw new Exception("Type of sale not found");
            return typeOfSale;
        }
    }
}
