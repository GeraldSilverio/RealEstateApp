using MediatR;
using RealEstateApp.Core.Application.Dtos.API.RealState;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Wrappers;
using System.Net;

namespace RealEstateApp.Core.Application.Features.RealState.Queries.GetAllRealState
{
    public class GetAllRealEstateQuery : IRequest<Response<List<RealEstateDto>>>
    {
    }

    public class GetAllRealEstateQueryHandler : IRequestHandler<GetAllRealEstateQuery, Response<List<RealEstateDto>>>
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IAccountService _accountService;

        public GetAllRealEstateQueryHandler(IRealEstateRepository realEstateRepository,IAccountService accountService)
        {
            _realEstateRepository = realEstateRepository;
            _accountService = accountService;
        }

        public async Task<Response<List<RealEstateDto>>> Handle(GetAllRealEstateQuery request, CancellationToken cancellationToken)
        {
            var realStateDto = await GetRealEstatesAsync();

            if (realStateDto.Count == 0) throw new ApiException("RealState not found", (int)HttpStatusCode.NoContent);

            return new Response<List<RealEstateDto>>(realStateDto);
        }

        private async Task<List<RealEstateDto>> GetRealEstatesAsync()
        {
            var realEstateList = new List<RealEstateDto>();
            var realEstates = await _realEstateRepository.GetAllWithIncludeAsync(new List<string> { "TypeOfSale", "TypeOfRealEstate", "RealEstateImprovements.Improvement" });

            foreach (var realEstate in realEstates)
            {
                var user = await _accountService.GetUserByIdAsync(realEstate.IdAgent);
                var realEstateView = new RealEstateDto()
                {
                    Id = realEstate.Id,
                    Description = realEstate.Description,
                    BathRooms = realEstate.BathRooms,
                    BedRooms = realEstate.BedRooms,
                    Size = realEstate.Size,
                    Code = realEstate.Code,
                    IdAgent = realEstate.IdAgent,
                    AgentEmail = user.Email,
                    AgentName = user.FirstName + " " + user.LastName,
                    Price = realEstate.Price,
                    Address = realEstate.Address,
                    TypeOfRealEstateName = realEstate.TypeOfRealEstate.Name,
                    TypeOfSaleName = realEstate.TypeOfSale.Name,
                    ImprovementName = realEstate.RealEstateImprovements.Select(x => x.Improvement.Name).ToList(),
                };
                realEstateList.Add(realEstateView);
            }
            return realEstateList;
        }
    }
}
