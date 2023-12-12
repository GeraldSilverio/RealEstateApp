using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Application.Wrappers;
using System.Net;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetAll
{
    /// <summary>
    ///Listado de todos los agentes
    /// </summary>
    public class GetAllAgentQuery : IRequest<Response<IList<UserViewModel>>>
    {

    }

    public class GetAllAgentQueryHandler : IRequestHandler<GetAllAgentQuery, Response<IList<UserViewModel>>>
    {
        private readonly IAgentService _agentService;

        public GetAllAgentQueryHandler(IAgentService agentService)
        {
            _agentService = agentService;
        }

        public async Task<Response<IList<UserViewModel>>> Handle(GetAllAgentQuery request, CancellationToken cancellationToken)
        {

            var agentsDto = await _agentService.GetAllAgentAsync();
            if (agentsDto.Count == 0) throw new ApiException("Agents not found", (int)HttpStatusCode.NotFound);
            return new Response<IList<UserViewModel>>(agentsDto);
        }

    }
}
