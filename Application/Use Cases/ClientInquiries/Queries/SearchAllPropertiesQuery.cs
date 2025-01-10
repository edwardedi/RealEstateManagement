using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.ClientInquiries.Queries
{
    public class SearchAllPropertiesQuery : IRequest<Result<List<PropertyListingDto>>>
    {
        public required string SearchQuery { get; set; }
    }
}
