using Application.DTOs;
using Domain.Common;
using MediatR;

namespace Application.Use_Cases.ClientInquiries.Queries
{
    public class GetAllInquiriesQuery : IRequest<Result<List<ClientInquiryDto>>>
    {   

    }
}
