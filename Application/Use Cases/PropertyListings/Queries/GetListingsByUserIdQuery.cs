using Application.DTOs;
using MediatR;

namespace Application.Use_Cases.PropertyListings.Queries
{
    public class GetListingsByUserIdQuery : IRequest<List<PropertyListingDto>>
    {
        public Guid UserId { get; set; }
    }
}
