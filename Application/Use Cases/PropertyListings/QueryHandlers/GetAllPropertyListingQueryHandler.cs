using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.QueryHandlers
{
    public class GetAllPropertyListingQueryHandler : IRequestHandler<GetAllPropertyListingQuery, Result<List<PropertyListingDto>>>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyListingRepository _repository;

        public GetAllPropertyListingQueryHandler(IMapper mapper, IPropertyListingRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<Result<List<PropertyListingDto>>> Handle(GetAllPropertyListingQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllListingsAsync();
            if (result.IsSuccess)
            {
                var listingsDto = result.Data.Select(listing => _mapper.Map<PropertyListingDto>(listing)).ToList();
                return Result<List<PropertyListingDto>>.Success(listingsDto);
            }
            else
            {
                return Result<List<PropertyListingDto>>.Failure(result.ErrorMessage); 
            }

        }
    }
}
