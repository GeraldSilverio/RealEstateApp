using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.RealState;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.RealState.Queries.GetRealStateById
{
    public class GetRealEstateByIdQuery : IRequest<Response<RealEstateDto>>
    {
        [SwaggerParameter(Description = "Debe colocar el id de la propiedad que quiere obtener")]
        public int Id { get; set; }
    }

    public class GetRealEstateByIdQueryHandler : IRequestHandler<GetRealEstateByIdQuery, Response<RealEstateDto>>
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IMapper _mapper;

        public GetRealEstateByIdQueryHandler(IRealEstateRepository realEstateRepository, IMapper mapper)
        {
            _mapper = mapper;
            _realEstateRepository = realEstateRepository;
        }

        public async Task<Response<RealEstateDto>> Handle(GetRealEstateByIdQuery request, CancellationToken cancellationToken)
        {
            var realState = _mapper.Map<RealEstateDto>(await _realEstateRepository.GetByIdAsync(request.Id));
            return new Response<RealEstateDto>(realState);
        }
    }
}
