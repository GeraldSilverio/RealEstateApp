using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using System.Net;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Queries.GetAllTypeOfSale
{
    public class GetAllTypeOfSaleQuery : IRequest<Response<IEnumerable<TypeOfSaleDto>>>
    {
    }
    public class GetAllTypeOfSaleQueryHandler : IRequestHandler<GetAllTypeOfSaleQuery, Response<IEnumerable<TypeOfSaleDto>>>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;
        private readonly IMapper _mapper;

        public GetAllTypeOfSaleQueryHandler(ITypeOfSaleRepository typeOfSaleRepository, IMapper mapper)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
            _mapper = mapper;
        }


        public async Task<Response<IEnumerable<TypeOfSaleDto>>> Handle(GetAllTypeOfSaleQuery request, CancellationToken cancellationToken)
        {
            var typesOfSales = _mapper.Map<List<TypeOfSaleDto>>(await _typeOfSaleRepository.GetAllAsync());

            if (typesOfSales.Count == 0) throw new ApiException("Type of sales not found",(int)HttpStatusCode.NotFound);

            return new Response<IEnumerable<TypeOfSaleDto>>(typesOfSales);
        }
    }
}
