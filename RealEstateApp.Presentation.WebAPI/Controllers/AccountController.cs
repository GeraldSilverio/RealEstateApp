using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Presentation.WebAPI.Controllers
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
        [HttpPost("RegisterDeveloper")]

        public async Task<IActionResult> RegisterDeveloper(RegisterRequest register)
        {
            try
            {
                register.SelectRole = (int)Roles.Developer;
                if (!ModelState.IsValid)
                {
                    return BadRequest("Envie los datos correctamente");
                }
                var response = await _accountService.RegisterApiAsync(register);
                if (response.HasError)
                {
                    return BadRequest(response.Error);
                }
                return StatusCode(StatusCodes.Status201Created, "Developer creado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterAdmin")]

        public async Task<IActionResult> RegisterAdmin(RegisterRequest register)
        {
            try
            {
                register.SelectRole = (int)Roles.Admin;
                if (!ModelState.IsValid)
                {
                    return BadRequest("Envie los datos correctamente");
                }
                var response = await _accountService.RegisterApiAsync(register);
                if (response.HasError)
                {
                    return BadRequest(response.Error);
                }
                return StatusCode(StatusCodes.Status201Created, "Admin creado con exito");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
