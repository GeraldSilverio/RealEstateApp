using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Features.Agent.Queries.GetAll
{
    public class GetAllAgentQuery :IRequest<IList<UserViewModel>>
    {

    }

    public class GetAllAgentQueryHandler : IRequestHandler<GetAllAgentQuery, IList<UserViewModel>>
    {
        private readonly IAgentService _agentService;

        public GetAllAgentQueryHandler(IAgentService agentService)
        {
            _agentService = agentService;
        }

        public async Task<IList<UserViewModel>> Handle(GetAllAgentQuery request, CancellationToken cancellationToken)
        {

            return null;
        }

    }
}
