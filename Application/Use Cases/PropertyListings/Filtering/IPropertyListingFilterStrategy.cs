using Application.Use_Cases.PropertyListings.Queries;
using Domain.Entities;

public interface IPropertyListingFilterStrategy
{
    IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request);
}
