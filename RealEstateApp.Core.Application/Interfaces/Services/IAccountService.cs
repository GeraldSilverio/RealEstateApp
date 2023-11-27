
using RealEstateApp.Core.Application.Dtos.Accounts;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();

        #region Register
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterApiAsync(RegisterRequest request);
        Task<RegisterResponse> RegisterAdminUserAsync(RegisterRequest request, string origin);
        #endregion

        #region Update
        Task UpdateAdminAsync(UpdateUserRequest request, string id);
        Task UpdateStatusAsync(string id, bool status);
        Task UpdateUserAsync(UpdateUserRequest request, string id);
        #endregion

        #region Gets
        List<AuthenticationResponse> GetAllUsersAsync();
        Task<AuthenticationResponse> GetUserByIdAsync(string id);
        #endregion



    }
}
