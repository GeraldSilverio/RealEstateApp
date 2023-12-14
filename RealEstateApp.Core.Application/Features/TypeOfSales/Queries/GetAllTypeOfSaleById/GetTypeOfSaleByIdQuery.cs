using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.TypeOfSale;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using System.Net;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Queries.GetAllTypeOfSaleById
{
    public class GetTypeOfSaleByIdQuery : IRequest<Response<TypeOfSaleDto>>
    {
        public int Id { get; set; }
    }
    public class GetTypeOfSaleByIdQueryHandler : IRequestHandler<GetTypeOfSaleByIdQuery, Response<TypeOfSaleDto>>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;
        private readonly IMapper _mapper;

        public GetTypeOfSaleByIdQueryHandler(ITypeOfSaleRepository typeOfSaleRepository, IMapper mapper)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
            _mapper = mapper;
        }
        public async Task<Response<TypeOfSaleDto>> Handle(GetTypeOfSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var typeOfSale = _mapper.Map<TypeOfSaleDto>(await _typeOfSaleRepository.GetByIdAsync(request.Id));

            if (typeOfSale is null) throw new ApiException("Type of sale not found",(int)HttpStatusCode.NoContent);

            return new Response<TypeOfSaleDto>(typeOfSale);
        }
    }
}
