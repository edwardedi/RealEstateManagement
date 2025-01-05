using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Transactions.Commands
{
    public class CreateTransactionCommand : IRequest<Result<Guid>>
    {
        public Guid PropertyId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public double SalePrice { get; set; }
        public string Status { get; set; }
    }
}
