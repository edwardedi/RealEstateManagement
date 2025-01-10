using Application.DTOs;
using Application.Use_Cases.PropertyListings.Filtering;
using Application.Use_Cases.PropertyListings.Queries;
using Application.Utils;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using Gridify;
using MediatR;

namespace Application.Use_Cases.PropertyListings.QueryHandlers
{
    public class GetFilteredPropertyListingsQueryHandler : IRequestHandler<GetFilteredPropertyListingsQuery, Result<PagedResult<PropertyListingDto>>>
    {
        private readonly IPropertyListingRepository repository;
        private readonly IMapper mapper;
        private readonly IEnumerable<IPropertyListingFilterStrategy> filterStrategies;


        public GetFilteredPropertyListingsQueryHandler(IPropertyListingRepository repository, IMapper mapper, IEnumerable<IPropertyListingFilterStrategy> filterStrategies)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.filterStrategies = filterStrategies;
        }

        public async Task<Result<PagedResult<PropertyListingDto>>> Handle(GetFilteredPropertyListingsQuery request, CancellationToken cancellationToken)
        {
            var propertyListingsResult = await repository.GetAllListingsAsync();

            if (!propertyListingsResult.IsSuccess)
            {
                return Result<PagedResult<PropertyListingDto>>.Failure(propertyListingsResult.ErrorMessage);
            }

            var propertyListings = propertyListingsResult.Data;
            var query = propertyListings.AsQueryable();

            //apply filter
            foreach (var strategy in filterStrategies)
            {
                query = strategy.ApplyFilter(query, request);
            }

            //apply pagination
            var pagedPropertyListings = query.ApplyPaging(request.Page, request.PageSize);

            var PropertyListingDtos = mapper.Map<List<PropertyListingDto>>(pagedPropertyListings);

            var pagedResult = new PagedResult<PropertyListingDto>(PropertyListingDtos, query.Count());

            return Result<PagedResult<PropertyListingDto>>.Success(pagedResult);

        }
    }
}
