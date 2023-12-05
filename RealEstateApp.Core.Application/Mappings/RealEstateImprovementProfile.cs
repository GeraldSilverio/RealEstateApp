using AutoMapper;
using RealEstateApp.Core.Application.ViewModel.RealEstateImprovement;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Mappings
{
    public class RealEstateImprovementProfile:Profile
    {
        public RealEstateImprovementProfile()
        {
            CreateMap<RealEstateImprovements,SaveRealEstateImprovementViewModeL>()
                .ReverseMap()
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore());
        }
    }
}
