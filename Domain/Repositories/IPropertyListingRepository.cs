﻿using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPropertyListingRepository
    {
        Task<IEnumerable<PropertyListing>> GetAllListingsAsync();
        Task<PropertyListing> GetListingByIdAsync(Guid id);
        Task<IEnumerable<PropertyListing>> GetListingsByUserId(Guid userId);
        Task<Guid> AddListingAsync(PropertyListing listing);
        Task UpdateListingAsync(PropertyListing listing);
        Task DeleteListingAsync(Guid id);
    }
}
