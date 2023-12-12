using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace RealEstateApp.Core.Application.ViewModel.User
{
    public class MyProfileViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "Debe ingresar un nombre.")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Debe ingresar un apellido.")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Debe ingresar un numero telefonico")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = "0000000";

        [DataType(DataType.Upload)]
        public IFormFile? File { get; set; }
        public string? ImageUser { get; set; }
        public string? Email { get; set; }
        public string? IdentityCard { get; set; }
        public string? UserName { get; set; }


       
    }
}
