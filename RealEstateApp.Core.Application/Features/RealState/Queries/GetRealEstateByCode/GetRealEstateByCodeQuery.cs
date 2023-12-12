using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.RealState;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.RealState.Queries.GetRealStateByCode
{
    public class GetRealEstateByCodeQuery : IRequest<Response<RealEstateDto>>
    {
        [SwaggerParameter(Description = "Debe colocar el codigo de la propiedad que quiere obtener")]
        public int Code { get; set; }
    }

    public class GetRealEstateByCodeQueryHandler : IRequestHandler<GetRealEstateByCodeQuery, Response<RealEstateDto>>
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IMapper _mapper;

        public GetRealEstateByCodeQueryHandler(IRealEstateRepository realEstateRepository, IMapper mapper)
        {
            _mapper = mapper;
            _realEstateRepository = realEstateRepository;
        }

        public async Task<Response<RealEstateDto>> Handle(GetRealEstateByCodeQuery request, CancellationToken cancellationToken)
        {
            var realState = _mapper.Map<RealEstateDto>(await _realEstateRepository.GetByIdAsync(request.Code));
            return new Response<RealEstateDto>(realState);
        }
    }
}
