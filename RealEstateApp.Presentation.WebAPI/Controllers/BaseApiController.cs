using Microsoft.AspNetCore.Mvc;

namespace RealEstateApp.Presentation.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController:ControllerBase
    {

    }
}
