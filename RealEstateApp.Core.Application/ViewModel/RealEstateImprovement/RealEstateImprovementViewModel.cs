using RealEstateApp.Core.Application.ViewModel.Improvement;
using RealEstateApp.Core.Application.ViewModel.RealEstate;

namespace RealEstateApp.Core.Application.ViewModel.RealEstateImprovement
{
    public class RealEstateImprovementViewModel
    {
        public int IdRealEstate { get; set; }
        public int IdImprovement { get; set; }
        public RealEstateViewModel RealEstate { get; set; } = null!;
        public ImprovementViewModel Improvement { get; set; } = null!;
    }
}
