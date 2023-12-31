﻿using Microsoft.AspNetCore.Http;
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

        Task<RegisterResponse> RegisterAsync(SaveUserViewModel model, string origin);
        Task<SaveUserViewModel> GetByUserIdAysnc(string id);
        Task<RegisterResponse> UpdateAsync(SaveUserViewModel viewModel);
        Task DeleteAsync(string id);

        Task ChangeStatusAsync(string id, bool status);

        Task ChangePasswordAsync(ChangePasswordViewModel model);
        string UploadFile(IFormFile file, string id, bool isEditMode = false, string imagePath = "");

        Task<int> CountUser(bool status, string rol);


        Task<bool> CheckOldPassword(string oldPassword, string userId);

    }
}
