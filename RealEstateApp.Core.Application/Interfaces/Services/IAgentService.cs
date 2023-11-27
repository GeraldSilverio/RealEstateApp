using RealEstateApp.Core.Application.Dtos.Accounts;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAgentService
    {
        Task<List<AuthenticationResponse>> GetAllAgentAsync();
        Task<AuthenticationResponse> GetAgentByIdAsync(string id);
        Task<AuthenticationResponse> ChangeStatus(string id,bool status);
    }
}
