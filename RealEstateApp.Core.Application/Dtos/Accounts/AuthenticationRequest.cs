using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Dtos.Accounts
{
   /// <summary>
   /// Parametros para iniciar sesion
   /// </summary>
    public class AuthenticationRequest
    {
        [SwaggerParameter(Description = "El nombre de usuario para poder iniciar sesion")]
        public string UserName { get; set; } = null!;
        [SwaggerParameter(Description = "La contraseña del usuario para poder iniciar sesion")]
        public string Password { get; set; } = null!;
    }
}
