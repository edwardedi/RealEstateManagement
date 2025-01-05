using Application.Use_Cases.Transactions.Commands;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.Transactions.CommandHandlers
{
    public class CreateTransactionCommandHandler : IRequestHandler<CreateTransactionCommand, Result<Guid>>
    {
        private readonly ITransactionRepository repository;
        private readonly IMapper mapper;

        public CreateTransactionCommandHandler(ITransactionRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(CreateTransactionCommand request, CancellationToken cancellationToken)
        {
            var transaction = mapper.Map<Transaction>(request);
            var result = await repository.AddTransactionAsync(transaction);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);
        }
    }
}
