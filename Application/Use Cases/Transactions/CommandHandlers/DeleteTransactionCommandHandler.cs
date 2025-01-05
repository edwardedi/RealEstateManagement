using MediatR;
using Domain.Repositories;
using RealEstateManagement.Application.Transactions.Commands;
using Domain.Common;

namespace RealEstateManagement.Application.Transactions.CommandHandlers
{
    public class DeleteTransactionCommandHandler : IRequestHandler<DeleteTransactionCommand, Result<Guid>>
    {
        private readonly ITransactionRepository repository;

        public DeleteTransactionCommandHandler(ITransactionRepository repository)
        {
            this.repository = repository;
        }

        public async Task<Result<Guid>> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteTransactionAsync(request.TransactionId);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);
        }
    }
}
