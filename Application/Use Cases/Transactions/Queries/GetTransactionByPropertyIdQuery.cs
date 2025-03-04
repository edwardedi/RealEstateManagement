﻿using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Transactions.Queries
{
    public class GetTransactionByPropertyIdQuery : IRequest<Result<TransactionDto>>
    {
        public Guid PropertyId{ get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
