using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetById
{
    /// <summary>
    /// Obtener un agente por su id.
    /// </summary>
    public class GetAgentByIdQuery : IRequest<Response<UserViewModel>>
    {
        [SwaggerParameter(Description = "Debe colocar el id del paciente que quiere obtener")]
        public string IdAgent { get; set; }
    }
    public class GetAgentByIdQueryHandler : IRequestHandler<GetAgentByIdQuery, Response<UserViewModel>>
    {
        private readonly IAgentService _agentService;

        public GetAgentByIdQueryHandler(IAgentService agentService)
        {
            _agentService = agentService;
        }

        public async Task<Response<UserViewModel>> Handle(GetAgentByIdQuery request, CancellationToken cancellationToken)
        {
            var agent = await _agentService.GetAgentByIdAsync(request.IdAgent);
            if (agent is null) throw new ApiException("Agents not found", (int)HttpStatusCode.NoContent);
            return new Response<UserViewModel>(agent);
        }


    }
}
