using Domain.Common;
using MediatR;
using System;

namespace RealEstateManagement.Application.Transactions.Commands
{
    public class DeleteTransactionCommand : IRequest<Result<Guid>>
    {
        public Guid TransactionId { get; set; }
    }
}
