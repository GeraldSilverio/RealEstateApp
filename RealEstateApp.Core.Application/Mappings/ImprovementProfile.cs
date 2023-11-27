using AutoMapper;
using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApp.Core.Application.Dtos.API.TypeOfRealEstate;
using RealEstateApp.Core.Application.Features.Improvements.Commands.CreateImprovement;
using RealEstateApp.Core.Application.Features.Improvements.Commands.UpdateImprovement;
using RealEstateApp.Core.Application.ViewModel.Improvement;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Mappings
{
    public class ImprovementProfile :Profile
    {
        public ImprovementProfile()
        {
            CreateMap<Improvement, SaveImprovementDto>()
               .ReverseMap()
               .ForMember(x => x.LastModified, opt => opt.Ignore())
               .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
               .ForMember(x => x.Created, opt => opt.Ignore())
               .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Improvement, ImprovementDto>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Improvement, ImprovementViewModel>()
                .ReverseMap()
                .ForMember(x => x.RealEstateImprovements, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore()); 
            
            CreateMap<Improvement, CreateImprovementCommand>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());
            
            CreateMap<Improvement, UpdateImprovementCommand>()
                .ReverseMap()
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<Improvement, SaveImprovementViewModel>()
                .ReverseMap()
                .ForMember(x => x.RealEstateImprovements, opt => opt.Ignore())
                .ForMember(x => x.LastModified, opt => opt.Ignore())
                .ForMember(x => x.LastModifiedBy, opt => opt.Ignore())
                .ForMember(x => x.Created, opt => opt.Ignore())
                .ForMember(x => x.CreatedBy, opt => opt.Ignore());

            CreateMap<ImprovementDto, ImprovementViewModel>()
                .ReverseMap();

            CreateMap<ImprovementDto, SaveImprovementViewModel>()
                .ReverseMap();
        }
    }
}
