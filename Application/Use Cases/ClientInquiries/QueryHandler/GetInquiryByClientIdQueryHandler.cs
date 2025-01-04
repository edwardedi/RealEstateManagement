using Application.DTOs;
using Application.Use_Cases.ClientInquiries.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.ClientInquiries.QueryHandler
{
    public class GetInquiryByClientIdQueryHandler : IRequestHandler<GetInquiryByClientIdQuery, Result<List<ClientInquiryDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IClientInquiryRepository _repository;

        public GetInquiryByClientIdQueryHandler(IMapper mapper, IClientInquiryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<List<ClientInquiryDto>>> Handle(GetInquiryByClientIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetInquiriesByClientId(request.ClientId);
            if (result.IsSuccess)
            {
                var inquiriesDto = result.Data.Select(inquiry => _mapper.Map<ClientInquiryDto>(inquiry)).ToList();
                return Result<List<ClientInquiryDto>>.Success(inquiriesDto);
            }
            else
            {
                return Result<List<ClientInquiryDto>>.Failure(result.ErrorMessage);
            }
        }
    }
}
