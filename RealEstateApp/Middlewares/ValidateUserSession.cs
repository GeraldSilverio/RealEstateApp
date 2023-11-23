using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Helpers;

namespace RealEstateApp.Presentation.WebApp.Middlewares;

public class ValidateUserSession
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ValidateUserSession(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public bool HasUser()
    {
        AuthenticationResponse authenticationResponse = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");

        if(authenticationResponse == null)
        {
            return false;
        }
        return true;
    }
}
