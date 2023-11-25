using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModel.User
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un correo electrónico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña con caracteres especiales, numeros y al menos una mayúscula")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
        [Required(ErrorMessage = "Debe ingresar una contraseña con caracteres especiales, numeros y al menos una mayúscula")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
