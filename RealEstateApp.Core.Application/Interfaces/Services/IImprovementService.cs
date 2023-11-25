using RealEstateApp.Core.Application.Dtos.API.Improvement;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IImprovementService:IGenericService<Improvement,SaveImprovementDto, ImprovementDto>
    {
    }
}
