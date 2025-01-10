using Application.Use_Cases.PropertyListings.Queries;
using Domain.Entities;

namespace Application.Use_Cases.PropertyListings.Filtering
{

    public interface IPropertyListingFilterStrategy
    {
        IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request);
    }
}
