using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Presentation.WebAPI.Controllers.V1
{
    [ApiVersion("1.0")]
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Authentication")]

        public async Task<IActionResult> Authentication(AuthenticationRequest request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Debes mandar toda la informacion");
                }
                var response = await _accountService.AuthenticateAsync(request);
                return Ok(response);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [Authorize(Roles = "Admin,Developer")]
        [HttpPost]

        public async Task<IActionResult> RegisterDeveloper(RegisterRequest register)
        {
            try
            {
                register.SelectRole = (int)Roles.Developer;
                if (!ModelState.IsValid)
                {
                    return BadRequest("Envie los datos correctamente");
                }
                await _accountService.RegisterApiAsync(register);
                return StatusCode(StatusCodes.Status201Created, "Developer creado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
