using Application.DTOs;
using Application.Use_Cases.PropertyListings.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.PropertyListings.QueryHandlers
{
    public class GetListingsByUserIdQueryHandler : IRequestHandler<GetListingsByUserIdQuery, Result<List<PropertyListingDto>>>
    {
        private readonly IMapper mapper;
        private readonly IPropertyListingRepository repository;

        public GetListingsByUserIdQueryHandler(IMapper mapper, IPropertyListingRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<Result<List<PropertyListingDto>>> Handle(GetListingsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetListingsByUserId(request.UserId);
            if (result.IsSuccess)
            {
                var listingsDto = result.Data.Select(listing => mapper.Map<PropertyListingDto>(listing)).ToList();
                return Result<List<PropertyListingDto>>.Success(listingsDto);
            }
            else
            {
                return Result<List<PropertyListingDto>>.Failure(result.ErrorMessage);
            }
        }
    }
}
