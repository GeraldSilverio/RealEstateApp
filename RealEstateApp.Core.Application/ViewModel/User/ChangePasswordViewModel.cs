using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModel.User
{
    public class ChangePasswordViewModel
    {
        public string Id { get; set; } = null!;
        [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;
        [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "LAS CONTRASEÑAS DEBE COINCIDIR")]
        public string ConfirmPassword { get; set; } = null!;

        public bool HasError { get; set; }
        public string? Error {  get; set; }
    }
}
