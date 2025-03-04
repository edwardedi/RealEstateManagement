using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using RealEstateManagement.Application.Transactions.Commands;

namespace Application.Use_Cases.Transactions.CommandHandlers
{
    public class UpdateTransactionCommandHandler : IRequestHandler<UpdateTransactionCommand, Result<Guid>>
    {
        private readonly ITransactionRepository repository;
        private readonly IMapper mapper;

        public UpdateTransactionCommandHandler(ITransactionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(UpdateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = mapper.Map<Transaction>(request);
            var result = await repository.UpdateTransactionAsync(transaction);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);
        }
    }
}
