﻿

using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.ViewModel.User;
using RealEstateApp.Core.Application.ViewModels.User;
using static RealEstateApp.Core.Application.ViewModel.Login.ForgotPassword;

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
    }
}
