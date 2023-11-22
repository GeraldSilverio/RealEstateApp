

using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.Login;
using static RealEstateApp.Core.Application.ViewModel.Login.ForgotPassword;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ILoginService
    {
        Task<string> ConfirmEmailAsync(string userId, string origin);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel viewModel, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel viewModel);
        Task SignOutAsync();
    }
}
