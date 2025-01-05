using Application.DTOs;
using Application.Use_Cases.Transactions.Queries;
using Application.Utils;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;
using Gridify;

namespace Application.Use_Cases.Transactions.QueriesHandlers
{
    internal class GetTransactionByBuyerIdQueryHandler : IRequestHandler<GetTransactionsByBuyerIdQuery, Result<PagedResult<TransactionDto>>>
    {
        private readonly IMapper mapper;
        private readonly ITransactionRepository repository;

        public GetTransactionByBuyerIdQueryHandler(IMapper mapper, ITransactionRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<Result<PagedResult<TransactionDto>>> Handle(GetTransactionsByBuyerIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetTransactionsByBuyerId(request.BuyerId);
            if (!result.IsSuccess)
            {
                return Result<PagedResult<TransactionDto>>.Failure(result.ErrorMessage);
            }
            var transactions = result.Data;
            var query = transactions.AsQueryable();

            var pagedTransactions = query.ApplyPaging(request.Page, request.PageSize);
            var transactionsDtos = mapper.Map<List<TransactionDto>>(pagedTransactions);
            var pagedResult = new PagedResult<TransactionDto>(transactionsDtos, query.Count());
            return Result<PagedResult<TransactionDto>>.Success(pagedResult);
        }
    }
}
