using Application.Use_Cases.PropertyListings.Queries;
using Domain.Entities;

public class TypeFilterStrategy : IPropertyListingFilterStrategy
{
    public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
    {
        if (!string.IsNullOrEmpty(request.Type))
        {
            query = query.Where(x => x.Type.ToLower() == request.Type.ToLower());
        }
        return query;
    }
}

public class PriceFilterStrategy : IPropertyListingFilterStrategy
{
    public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
    {
        if (request.Price > 0)
        {
            query = query.Where(x => x.Price == request.Price);
        }
        return query;
    }
}

public class SquareFootageFilterStrategy : IPropertyListingFilterStrategy
{
    public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
    {
        if (request.SquareFootage > 0)
        {
            query = query.Where(x => x.SquareFootage == request.SquareFootage);
        }
        return query;
    }
}

public class NumberOfBedroomsFilterStrategy : IPropertyListingFilterStrategy
{
    public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
    {
        if (request.NumberOfBedrooms > 0)
        {
            query = query.Where(x => x.NumberOfBedrooms == request.NumberOfBedrooms);
        }
        return query;
    }
}

public class NumberOfBathroomsFilterStrategy : IPropertyListingFilterStrategy
{
    public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
    {
        if (request.NumberOfBathrooms > 0)
        {
            query = query.Where(x => x.NumberOfBathrooms == request.NumberOfBathrooms);
        }
        return query;
    }
}

public class StatusFilterStrategy : IPropertyListingFilterStrategy
{
    public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
    {
        if (!string.IsNullOrEmpty(request.Status))
        {
            query = query.Where(x => x.Status == request.Status);
        }
        return query;
    }
}
