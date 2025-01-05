using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.PropertyListings.Queries
{
    public class GetListingsByUserIdQuery : IRequest<Result<List<PropertyListingDto>>>
    {
        public Guid UserId { get; set; }
    }
}
