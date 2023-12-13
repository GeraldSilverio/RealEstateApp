using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Dtos.API.RealState;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Wrappers;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace RealEstateApp.Core.Application.Features.RealState.Queries.GetRealStateById
{
    public class GetRealEstateByIdQuery : IRequest<Response<RealEstateDto>>
    {
        [SwaggerParameter(Description = "Debe colocar el id de la propiedad que quiere obtener")]
        public int Id { get; set; }
    }

    public class GetRealEstateByIdQueryHandler : IRequestHandler<GetRealEstateByIdQuery, Response<RealEstateDto>>
    {
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IAccountService _accountService;

        public GetRealEstateByIdQueryHandler(IRealEstateRepository realEstateRepository, IMapper mapper, IAccountService accountService)
        {
            _realEstateRepository = realEstateRepository;
            _accountService = accountService;
        }

        public async Task<Response<RealEstateDto>> Handle(GetRealEstateByIdQuery request, CancellationToken cancellationToken)
        {
            var realStateDto = await GetRealEstateByIdAsync(request.Id);

            if (realStateDto == null)
            {
                throw new ApiException("RealEstate not found",(int)HttpStatusCode.NoContent);
            }

            return new Response<RealEstateDto>(realStateDto);
        }
        private async Task<RealEstateDto> GetRealEstateByIdAsync(int id)
        {
            var realEstateList = new List<RealEstateDto>();
            var realEstates = await _realEstateRepository.GetAllWithIncludeAsync(new List<string> { "TypeOfSale", "TypeOfRealEstate", "RealEstateImprovements.Improvement" });
            var realEstateView = realEstates.FirstOrDefault(x => x.Id == id);
            if (realEstateView is null)
            {
                return null;
            }

            var user = await _accountService.GetUserByIdAsync(realEstateView.IdAgent);
            var realEstateDto = new RealEstateDto()
            {
                Id = realEstateView.Id,
                Description = realEstateView.Description,
                BathRooms = realEstateView.BathRooms,
                BedRooms = realEstateView.BedRooms,
                Size = realEstateView.Size,
                Code = realEstateView.Code,
                IdAgent = realEstateView.IdAgent,
                AgentEmail = user.Email,
                AgentName = user.FirstName + " " + user.LastName,
                Price = realEstateView.Price,
                Address = realEstateView.Address,
                TypeOfRealEstateName = realEstateView.TypeOfRealEstate.Name,
                TypeOfSaleName = realEstateView.TypeOfSale.Name,
                ImprovementName = realEstateView.RealEstateImprovements.Select(x => x.Improvement.Name).ToList(),
            };

            return realEstateDto;
        }
    }
}
