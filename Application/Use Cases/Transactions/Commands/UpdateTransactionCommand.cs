using Domain.Common;
using MediatR;
using Domain.Entities;

namespace RealEstateManagement.Application.Transactions.Commands
{
    public class UpdateTransactionCommand : IRequest<Result<Guid>>
    {
        public Guid TransactionId { get; set; }
        public Guid PropertyId { get; set; }
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public double SalePrice { get; set; }
    }
}
