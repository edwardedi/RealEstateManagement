using Application.DTOs;
using Application.Use_Cases.PropertyListings.Queries;
using AutoMapper;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.PropertyListings.QueryHandlers
{
    public class GetListingsByUserIdQueryHandler : IRequestHandler<GetListingsByUserIdQuery, List<PropertyListingDto>>
    {
        private readonly IMapper mapper;
        private readonly IPropertyListingRepository repository;

        public GetListingsByUserIdQueryHandler(IMapper mapper, IPropertyListingRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<List<PropertyListingDto>> Handle(GetListingsByUserIdQuery request, CancellationToken token)
        {
            var listings = await repository.GetListingsByUserId(request.UserId);
            return listings.Select(listing => mapper.Map<PropertyListingDto>(listing)).ToList();
        }
    }
}
