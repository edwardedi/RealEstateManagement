using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.ClientInquiries.Queries
{
    public class GetInquiryByClientIdQuery : IRequest<Result<List<ClientInquiryDto>>>
    {
        public Guid ClientId { get; set; }
    }
}
