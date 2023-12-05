using AutoMapper;
using Microsoft.AspNetCore.Http;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.Login;
using RealEstateApp.Core.Application.ViewModel.User;

namespace RealEstateApp.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;

        public UserService(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        #region PasswordMethods
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel viewModel)
        {
            ResetPasswordRequest forgotRequest = _mapper.Map<ResetPasswordRequest>(viewModel);
            return await _accountService.ResetPasswordAsync(forgotRequest);
        }

        public async Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel model, string origin)
        {
            ForgotPasswordRequest forgotRequest = _mapper.Map<ForgotPasswordRequest>(model);
            return await _accountService.ForgotPasswordAsync(forgotRequest, origin);
        }
        #endregion

        //Debes enviarle un rol para el buscarte todos los usuarios con ese rol.
        public async Task<List<UserViewModel>> GetAllAsync(string entity)
        {
            var users = _mapper.Map<List<UserViewModel>>(await _accountService.GetAllAsync(entity));
            return users;
        }

        public async Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel model)
        {
            var request = _mapper.Map<RegisterRequest>(model);
            var response = await _accountService.RegisterAsync(request, null);
            var viewModel = _mapper.Map<SaveUserViewModel>(response);
            return viewModel;
        }

        public async Task<SaveUserViewModel> GetByUserIdAysnc(string id)
        {
            var user = await _accountService.GetUserByIdAsync(id);
            return _mapper.Map<SaveUserViewModel>(user);
        }

        public async Task UpdateAsync(UpdateUserRequest viewModel)
        {
            await _accountService.UpdateAsync(viewModel, viewModel.Id);
        }

        public async Task DeleteAsync(string id)
        {
            await _accountService.DeleteAsync(id);
        }

        public async Task ChangeStatusAsync(string id, bool status)
        {
            await _accountService.ChangeStatusAsync(id, status);
        }

        public async Task ChangePasswordAsync(ChangePasswordViewModel model)
        {
            await _accountService.ChangePasswordAsync(model.Id, model.Password);
        }

        public string UploadFile(IFormFile file, string id, bool isEditMode = false, string imagePath = "")
        {
            if (isEditMode)
            {
                if (file == null)
                {
                    return imagePath;
                }
            }
            string basePath = $"/Images/Register/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            //create folder if not exist
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            //get file extension
            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new(file.FileName);
            string fileName = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = imagePath.Split("/");
                string oldImagePath = oldImagePart[^1];
                string completeImageOldPath = Path.Combine(path, oldImagePath);

                if (File.Exists(completeImageOldPath))
                {
                    File.Delete(completeImageOldPath);
                }
            }
            return $"{basePath}/{fileName}";
        }
    }
}
