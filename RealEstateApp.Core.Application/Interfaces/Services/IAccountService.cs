
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateWebApiAsync(AuthenticationRequest request);
        Task<AuthenticationResponse> AuthenticateWebAppAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task ChangePassword(string userid, string password);
        Task SignOutAsync();

        #region Register
        Task<RegisterResponse> RegisterAsync(RegisterRequest request, string? origin);
     
        #endregion

        #region Update
        Task ChangeStatusAsync(string id, bool status);
        Task UpdateAsync(UpdateUserRequest request, string id);
        #endregion

        #region Gets
        Task<AuthenticationResponse> GetUserByIdAsync(string id);
        #endregion



    }
}
