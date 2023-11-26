using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Core.Application.Features.TypeOfSales.Command.DeleteTypeOfSaleById
{
    public class DeleteTypeOfSaleByIdCommand:IRequest 
    {
        public int Id { get; set; }
    }
    public class DeleteTypeOfSaleByIdCommandHandler : IRequestHandler<DeleteTypeOfSaleByIdCommand>
    {
        private readonly ITypeOfSaleRepository _typeOfSaleRepository;

        public DeleteTypeOfSaleByIdCommandHandler(ITypeOfSaleRepository typeOfSaleRepository)
        {
            _typeOfSaleRepository = typeOfSaleRepository;
        }
        public async Task<Unit> Handle(DeleteTypeOfSaleByIdCommand command, CancellationToken cancellationToken)
        {
            var typeOfSale = await _typeOfSaleRepository.GetByIdAsync(command.Id);
            await _typeOfSaleRepository.DeleteAsync(typeOfSale);
            return Unit.Value;
        }
    }
}
