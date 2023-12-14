
using RealEstateApp.Core.Application.Dtos.Accounts;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        #region ChangePassword
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task ChangePasswordAsync(string userid, string password);

        #endregion

        #region Register and Authentication
        Task<RegisterResponse> RegisterAsync(RegisterRequest request, string? origin);
        Task<AuthenticationResponse> AuthenticateWebApiAsync(AuthenticationRequest request);
        Task<AuthenticationResponse> AuthenticateWebAppAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);

        Task SignOutAsync();

        #endregion

        #region Update
        Task ChangeStatusAsync(string id, bool status);
        Task UpdateAsync(UpdateUserRequest request, string id);
        #endregion

        #region Gets
        Task<List<AuthenticationResponse>> GetAllAsync(string entity);
        Task<AuthenticationResponse> GetUserByIdAsync(string id);
        #endregion

        #region Delete
        Task DeleteAsync(string id);
        #endregion

        #region CheckPassword
        Task<bool> CheckOldPassword(string oldPassword, string userId);
        #endregion
        Task<int> CountUser(bool status, string rol);
    }
}
