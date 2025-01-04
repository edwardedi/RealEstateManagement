using Application.DTOs;
using Application.Use_Cases.ClientInquiries.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.ClientInquiries.QueryHandler
{
    public class SearchAllPropertiesQueryHandler : IRequestHandler<SearchAllPropertiesQuery, Result<List<PropertyListingDto>>>
    {
        private readonly IMapper mapper;
        private readonly IClientInquiryRepository repository;

        public SearchAllPropertiesQueryHandler(IMapper mapper, IClientInquiryRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<Result<List<PropertyListingDto>>> Handle(SearchAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.SearchAllPropertiesAsync(request.SearchQuery);
            if (result.IsSuccess)
            {
                var propertiesDto = result.Data.Select(property => mapper.Map<PropertyListingDto>(property)).ToList();
                return Result<List<PropertyListingDto>>.Success(propertiesDto);
            }
            else
            {
                return Result<List<PropertyListingDto>>.Failure(result.ErrorMessage);
            }
        }
    }
}
