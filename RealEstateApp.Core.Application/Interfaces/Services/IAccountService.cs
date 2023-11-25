
using RealEstateApp.Core.Application.Dtos.Accounts;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterApiAsync(RegisterRequest request);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
        Task SignOutAsync();
        Task UpdateAdminAsync(UpdateUserRequest request, string id);
        Task UpdateStatusAsync(string id, bool status);
        Task UpdateUserAsync(UpdateUserRequest request, string id);
        List<AuthenticationResponse> GetAllUsersAsync();
        Task<AuthenticationResponse> GetUserByIdAsync(string id);


    }
}
