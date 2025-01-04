using Application.DTOs;
using Application.Use_Cases.ClientInquiries.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;                                      

namespace Application.Use_Cases.ClientInquiries.QueryHandler
{
    public class GetInquiryByIdQueryHandler : IRequestHandler<GetInquiryByIdQuery, Result<ClientInquiryDto>>
    {
        private readonly IMapper mapper;
        private readonly IClientInquiryRepository repository;

        public GetInquiryByIdQueryHandler(IMapper mapper, IClientInquiryRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<Result<ClientInquiryDto>> Handle(GetInquiryByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetInquiryByIdAsync(request.InquiryId);
            if (result.IsSuccess)
            {
                var inquiryDto = mapper.Map<ClientInquiryDto>(result.Data);
                return Result<ClientInquiryDto>.Success(inquiryDto);
            }
            else
            {
                return Result<ClientInquiryDto>.Failure($"Client inquiry with Id {request.InquiryId} not found");
            }
        }
    }
}
