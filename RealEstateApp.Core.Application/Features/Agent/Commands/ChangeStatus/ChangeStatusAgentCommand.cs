using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.Agent.Commands.ChangeStatus
{
    /// <summary>
    /// Parametros para cambiar el estado de un agente.
    /// </summary>
    public class ChangeStatusAgentCommand : IRequest<Response<string>>
    {
        [SwaggerParameter(Description = "Debe colocar el id del agente a actualizar")]
        public string IdAgent { get; set; }
        [SwaggerParameter(Description = "Debe colocar el estado que desea cambiar")]
        public bool Status { get; set; }
    }
    public class ChangeStatusAgentCommandHandler : IRequestHandler<ChangeStatusAgentCommand, Response<string>>
    {
        private readonly IAgentService _agentService;


        public ChangeStatusAgentCommandHandler(IAgentService agentService)
        {
            _agentService = agentService;
        }

        public async Task<Response<string>> Handle(ChangeStatusAgentCommand command, CancellationToken cancellationToken)
        {
            var agent = await _agentService.ChangeStatus(command.IdAgent, command.Status);
            if(agent.HasError) throw new ApiException("Agent not found", (int)HttpStatusCode.NoContent);
            return new Response<string>(agent.Id);
        }
    }
}
