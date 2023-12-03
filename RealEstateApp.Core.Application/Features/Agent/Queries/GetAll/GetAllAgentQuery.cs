using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Application.Wrappers;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetAll
{
    public class GetAllAgentQuery :IRequest<Response<IList<UserViewModel>>> 
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

            return null;
        }

    }
}
