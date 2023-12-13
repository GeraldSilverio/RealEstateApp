using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModel.User
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "LAS CONTRASEÑAS DEBE COINCIDIR")]
        public string ConfirmPassword { get; set; }

        public string? OldPassword { get; set; }
        public bool HasError { get; set; }
        public string? Error {  get; set; }
    }
}
