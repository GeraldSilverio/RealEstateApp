using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
     
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel viewModel);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin);
        Task<List<UserViewModel>> GetAllAsync();
        Task<UserStatusViewModel> GetUserById(string id);
        Task<SaveUserViewModel> GetUserViewModelById(string id);
       
    }
}
