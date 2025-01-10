using Application.Use_Cases.PropertyListings.Queries;
using Domain.Entities;

namespace Application.Use_Cases.PropertyListings.Filtering
{
    public class TypeFilterStrategy : IPropertyListingFilterStrategy
    {
        public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
        {
            if (!string.IsNullOrEmpty(request.Type))
            {
                query = query.Where(x => string.Equals(x.Type.ToLower(),request.Type.ToLower()));
            }
            return query;
        }
    }

    public class PriceFilterStrategy : IPropertyListingFilterStrategy
    {
        readonly float epsilon = 0.0001f;

        public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
        {
            if (request.Price > 0)
            {
                query = query.Where(x => Math.Abs(x.Price - request.Price) < epsilon);
            }
            return query;
        }
    }


    public class SquareFootageFilterStrategy : IPropertyListingFilterStrategy
    {
        readonly float epsilon = 0.0001f;
        public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
        {
            if (request.SquareFootage > 0)
            {
                query = query.Where(x => Math.Abs(x.SquareFootage - request.SquareFootage) < epsilon);
            }
            return query;
        }
    }

    public class NumberOfBedroomsFilterStrategy : IPropertyListingFilterStrategy
    {
        readonly float epsilon = 0.0001f;
        public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
        {
            if (request.NumberOfBedrooms > 0)
            {
                query = query.Where(x => Math.Abs(x.NumberOfBedrooms - request.NumberOfBedrooms) < epsilon);
            }
            return query;
        }
    }

    public class NumberOfBathroomsFilterStrategy : IPropertyListingFilterStrategy
    {
        readonly float epsilon = 0.0001f;
        public IQueryable<PropertyListing> ApplyFilter(IQueryable<PropertyListing> query, GetFilteredPropertyListingsQuery request)
        {
            if (request.NumberOfBathrooms > 0)
            {
                query = query.Where(x => Math.Abs(x.NumberOfBathrooms - request.NumberOfBathrooms) < epsilon);
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
}
