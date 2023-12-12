using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Accounts;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModel.RealEstateClient;

namespace RealEstateApp.Presentation.WebApp.Controllers
{
    [Authorize(Roles = "Client")]
    public class ClientController : Controller
    {
        private readonly AuthenticationResponse? user;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IRealEstateClientService _realEstateClientService;
        private readonly IRealEstateService _realEstateService;

        public ClientController(IHttpContextAccessor httpContextAccessor, IRealEstateClientService realEstateClientService, IRealEstateService realEstateService)
        {
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
            _realEstateClientService = realEstateClientService;
            _realEstateService = realEstateService;
        }
        public async Task<IActionResult> MakeFavorite(int id)
        {
            SaveRealEstateClientViewModel model = new()
            {
                IdCliente = user.Id,
                IdRealEstate = id
            };
            await _realEstateClientService.Add(model);
            foreach (var favorite in await _realEstateClientService.GetFavoritesByUserId(user.Id))
            {
                user.FavoritesRealEstate.Add(favorite.IdRealEstate);
            }
            _httpContextAccessor.HttpContext.Session.Set("user", user);

            return RedirectToRoute(new { controller = "Home", action = "PrincipalView" });
        }
        public async Task<IActionResult> MyRealEstates()
        {
            try
            {
                var favorites = await _realEstateService.GetFavoritesByClient();
                return View(favorites);

            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}
