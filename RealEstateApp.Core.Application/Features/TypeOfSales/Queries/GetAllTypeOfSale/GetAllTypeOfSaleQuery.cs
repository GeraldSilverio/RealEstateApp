using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Queries.GetAllTypeOfSale
{
    public class GetAllTypeOfSaleQuery : IRequest<IEnumerable<TypeOfSaleDto>>
    {
    }
    public class GetAllTypeOfSaleQueryHandler : IRequestHandler<GetAllTypeOfSaleQuery, IEnumerable<TypeOfSaleDto>>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;
        private readonly IMapper _mapper;

        public GetAllTypeOfSaleQueryHandler(ITypeOfSaleRepository typeOfSaleRepository, IMapper mapper)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<TypeOfSaleDto>> Handle(GetAllTypeOfSaleQuery request, CancellationToken cancellationToken)
        {
            var typesOfSales = _mapper.Map<List<TypeOfSaleDto>>(await _typeOfSaleRepository.GetAllAsync());
            if (typesOfSales.Count == 0) throw new Exception("Type of sales not found");
            return typesOfSales;
        }
    }
}
