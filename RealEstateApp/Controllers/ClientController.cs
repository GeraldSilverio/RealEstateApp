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

        public async Task<IActionResult> Index()
        {
            await _realEstateClientService.SetFavorites();
            var realEstates = await _realEstateService.GetAll();
            return View(realEstates);
        }
        [HttpPost]
        public async Task<IActionResult> Index(string name, int? toilets, int? bedrooms, decimal? minPrice, decimal? maxPrice)
        {
            try
            {
                var realEstates = await _realEstateService.GetAll();

                // Aplica los filtros de nombre, baños y habitaciones si se proporcionan
                if (!string.IsNullOrEmpty(name))
                {
                    realEstates = realEstates.Where(x => x.TypeOfRealEstateName == name).ToList();
                }

                if (toilets.HasValue)
                {
                    realEstates = realEstates.Where(x => x.BathRooms == toilets).ToList();
                }

                if (bedrooms.HasValue)
                {
                    realEstates = realEstates.Where(x => x.BedRooms == bedrooms).ToList();
                }

                // Aplica los filtros de precio mínimo y máximo si se proporcionan
                if (minPrice.HasValue)
                {
                    realEstates = realEstates.Where(x => x.Price >= minPrice).ToList();
                }

                if (maxPrice.HasValue)
                {
                    realEstates = realEstates.Where(x => x.Price <= maxPrice).ToList();
                }

                return View(realEstates);
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public async Task<IActionResult> MakeFavorite(int id)
        {
            SaveRealEstateClientViewModel model = new()
            {
                IdCliente = user.Id,
                IdRealEstate = id
            };
            await _realEstateClientService.Add(model);
            return RedirectToRoute(new { controller = "Client", action = "Index" });
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
        public async Task<IActionResult> DeleteFavorite(int id)
        {
            try
            {
                await _realEstateClientService.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
    }
}
