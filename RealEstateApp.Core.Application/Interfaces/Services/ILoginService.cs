

using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;
using static RealEstateApp.Core.Application.ViewModel.Login.ForgotPasswordViewModel;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ILoginService
    {
        Task<string> ConfirmEmailAsync(string userId, string origin);      
        Task<AuthenticationResponse> LoginAsync(LoginViewModel viewModel);      
        Task SignOutAsync();
    }
}
