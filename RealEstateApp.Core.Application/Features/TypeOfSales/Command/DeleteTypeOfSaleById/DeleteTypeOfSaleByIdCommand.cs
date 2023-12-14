using MediatR;
using RealEstateApp.Core.Application.Exceptions;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;
using RealEstateApp.Core.Domain.Entities;
using System.Net;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Command.DeleteTypeOfSaleById
{
    public class DeleteTypeOfSaleByIdCommand:IRequest <Response<Unit>>
    {
        public int Id { get; set; }
    }
    public class DeleteTypeOfSaleByIdCommandHandler : IRequestHandler<DeleteTypeOfSaleByIdCommand,  Response<Unit>>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;
        private readonly IRealEstateRepository _realEstateRepository;
        private readonly IRealEstateClientRepository _realEstateClientRepository;
        private readonly IRealEstateImageRepository _realEstateImageRepository;

        public DeleteTypeOfSaleByIdCommandHandler(ITypeOfSaleRepository typeOfSaleRepository, IRealEstateRepository realEstateRepository, IRealEstateClientRepository realEstateClientRepository, IRealEstateImageRepository realEstateImageRepository)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
            _realEstateRepository = realEstateRepository;
            _realEstateClientRepository = realEstateClientRepository;
            _realEstateImageRepository = realEstateImageRepository;
        }
        public async Task<Response<Unit>> Handle(DeleteTypeOfSaleByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfSale = await _typeOfSaleRepository.GetByIdAsync(command.Id);
            if (typeOfSale is null) throw new ApiException("Type of sale not found", (int)HttpStatusCode.NoContent);
            await DeleteRelationships(typeOfSale.Id);
            await _typeOfSaleRepository.DeleteAsync(typeOfSale);
            return new Response<Unit>(Unit.Value);
        }
        private async Task DeleteRelationships(int idTypeOfSale)
        {
            var realEstate = await _realEstateRepository.GetRealEstateByTypeOfSaleAsync(idTypeOfSale);
            foreach (var item in realEstate)
            {
                await _realEstateClientRepository.RemoveAllByIdRealEstate(item);
                await _realEstateImageRepository.RemoveAll(item);
            }
        }
    }
}
