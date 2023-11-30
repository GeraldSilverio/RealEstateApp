using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
     
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel viewModel);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin);
        Task<List<UserViewModel>> GetAllAsync(string entity);

        Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel model);
        Task<SaveUserViewModel> GetByUserIdAysnc(string id);
        Task UpdateAsync(UpdateUserRequest viewModel);
        Task DeleteAsync(string id);

        Task ChangeStatusAsync(string id, bool status);

        Task ChangePasswordAsync(ChangePasswordViewModel model);
        string UplpadFile(IFormFile file, string id, bool isEditMode = false, string imagePath = "");



    }
}
