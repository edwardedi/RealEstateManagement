using Application.DTOs;
using Application.Use_Cases.Queries;
using AutoMapper;
using Domain.Common;
using Domain.Repositories;
using MediatR;


namespace Application.Use_Cases.QueryHandlers
{
    public class GetListingByIdQueryHandler : IRequestHandler<GetListingByIdQuery, Result<PropertyListingDto>>
    {
        private readonly IMapper mapper;
        private readonly IPropertyListingRepository repository;

        public GetListingByIdQueryHandler(IMapper mapper, IPropertyListingRepository repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public async Task<Result<PropertyListingDto>> Handle(GetListingByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await repository.GetListingByIdAsync(request.PropertyId);
            if ( result.IsSuccess)
            {
                var listingDto = mapper.Map<PropertyListingDto>(result.Data);
                return Result<PropertyListingDto>.Success(listingDto);
            }
            else
            {
                return Result<PropertyListingDto>.Failure(result.ErrorMessage);
            }
        }
    }
}
