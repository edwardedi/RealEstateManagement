using Application.DTOs;
using Application.Use_Cases.Transactions.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.Transactions.QueriesHandlers
{
    public class GetTransactionByPropertyIdQueryHandler : IRequestHandler<GetTransactionByPropertyIdQuery, Result<TransactionDto>>
    {
        private readonly IMapper mapper;
        private readonly ITransactionRepository repository;

        public GetTransactionByPropertyIdQueryHandler(IMapper mapper, ITransactionRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<Result<TransactionDto>> Handle(GetTransactionByPropertyIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetTransactionByPropertyIdAsync(request.PropertyId);

            if (result.IsSuccess)
            {
                var transactionDto = mapper.Map<TransactionDto>(result.Data);
                return Result<TransactionDto>.Success(transactionDto);
            }

            return Result<TransactionDto>.Failure(result.ErrorMessage);
        }
    }
}
