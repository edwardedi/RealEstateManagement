using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Queries
{
    public class GetListingByIdQuery : IRequest<Result<PropertyListingDto>>
    {
        public Guid PropertyId { get; set; }
    }
}
