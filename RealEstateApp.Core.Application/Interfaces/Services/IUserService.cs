using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Application.ViewModels.User;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<RegisterResponse> RegisterAsync(SaveUserViewModel saveViewModel, string origin);
        Task<RegisterResponse> RegisterAdminAsync(SaveUserViewModel saveViewModel, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel viewModel);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin);
        List<UserViewModel> GetAllAsync();
        Task<UserStatusViewModel> GetUserById(string id);
        Task<SaveUserViewModel> GetUserViewModelById(string id);
        Task UpdateStatus(string id, bool status);
        Task UpdateUser(SaveUserViewModel vm, string id);
        Task UpdateAdmin(SaveUserViewModel vm, string id);
    }
}
