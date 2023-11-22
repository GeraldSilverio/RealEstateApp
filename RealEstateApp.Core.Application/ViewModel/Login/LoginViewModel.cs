using System.ComponentModel.DataAnnotations;


namespace RealEstateApp.Core.Application.ViewModel.Login
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Debe ingresar un nombre de usuario")]
        [DataType(DataType.EmailAddress)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Debe ingresar una contraseña con caracteres especiales, numeros y al menos una mayúscula")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool HasError { get; set; }
        public string? Error { get; set; }
    }
}
