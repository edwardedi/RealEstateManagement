using Domain.Common;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPropertyListingRepository
    {
        Task<IEnumerable<PropertyListing>> GetAllListingsAsync();
        Task<PropertyListing> GetListingByIdAsync(Guid id);
        Task<IEnumerable<PropertyListing>> GetListingsByUserId(Guid userId);
        Task<Result<Guid>> AddListingAsync(PropertyListing listing);
        Task<Result<Guid>> UpdateListingAsync(PropertyListing listing);
        Task<Result<Guid>> DeleteListingAsync(Guid id);
    }
}
