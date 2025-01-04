using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.ClientInquiries.Queries
{
    public class GetInquiryByIdQuery : IRequest<Result<ClientInquiryDto>>
    {
        public Guid InquiryId { get; set; }
    }
    
}
