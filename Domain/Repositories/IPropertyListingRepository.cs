using Domain.Common;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IPropertyListingRepository
    {
        Task<Result<IEnumerable<PropertyListing>>> GetAllListingsAsync();
        Task<Result<PropertyListing>> GetListingByIdAsync(Guid id);
        Task<Result<IEnumerable<PropertyListing>>> GetListingsByUserId(Guid userId);
        Task<Result<Guid>> AddListingAsync(PropertyListing listing);
        Task<Result<Guid>> UpdateListingAsync(PropertyListing listing);
        Task<Result<Guid>> DeleteListingAsync(Guid id);
    }
}
