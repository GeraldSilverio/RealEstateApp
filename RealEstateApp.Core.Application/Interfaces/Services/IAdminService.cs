using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAdminService
    {
        Task<List<UserViewModel>> GetAllAdmin();
        Task<RegisterResponse> RegisterAdmin(SaveUserViewModel model);
    }
}
