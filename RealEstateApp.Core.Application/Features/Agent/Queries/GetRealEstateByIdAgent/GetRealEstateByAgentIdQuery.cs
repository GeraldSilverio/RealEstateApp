using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstate;
using RealEstateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetRealEstateByIdAgent
{
    /// <summary>
    ///Obtener las propiedades del agente mediante su ID.
    /// </summary>
    public class GetRealEstateByAgentIdQuery : IRequest<Response<List<RealEstateViewModel>>>
    {
        [SwaggerParameter(Description = "Debe colocar el id del agente del que desea las propiedades")]

        public string IdAgent { get; set; }
    }
    public class GetRealEstateByAgentIdQueryHandler : IRequestHandler<GetRealEstateByAgentIdQuery, Response<List<RealEstateViewModel>>>
    {
        private readonly IRealEstateService _realEstateService;

        public GetRealEstateByAgentIdQueryHandler(IRealEstateService realEstateService)
        {
            _realEstateService = realEstateService;
        }

        public async Task<Response<List<RealEstateViewModel>>> Handle(GetRealEstateByAgentIdQuery request, CancellationToken cancellationToken)
        {
            var realEstate = await _realEstateService.GetAllByAgent(request.IdAgent);
            if (realEstate.Count == 0) throw new ApiException("RealEstate not found", (int)HttpStatusCode.NotFound);

            return new Response<List<RealEstateViewModel>>(realEstate);
        }
    }
}
