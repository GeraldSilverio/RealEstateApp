using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Wrappers;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Command.DeleteTypeOfSaleById
{
    public class DeleteTypeOfSaleByIdCommand:IRequest <Response<Unit>>
    {
        public int Id { get; set; }
    }
    public class DeleteTypeOfSaleByIdCommandHandler : IRequestHandler<DeleteTypeOfSaleByIdCommand,  Response<Unit>>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;

        public DeleteTypeOfSaleByIdCommandHandler(ITypeOfSaleRepository typeOfSaleRepository)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
        }
        public async Task<Response<Unit>> Handle(DeleteTypeOfSaleByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfSale = await _typeOfSaleRepository.GetByIdAsync(command.Id);
            await _typeOfSaleRepository.DeleteAsync(typeOfSale);
            return new Response<Unit>(Unit.Value);
        }
    }
}
