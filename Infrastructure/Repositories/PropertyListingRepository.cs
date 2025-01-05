using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PropertyListingRepository : IPropertyListingRepository
    {
        private readonly ApplicationDbContext context;

        public PropertyListingRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Result<IEnumerable<PropertyListing>>> GetAllListingsAsync()
        {
            try
            {
                var listings = await context.PropertyListings.ToListAsync();
                return Result<IEnumerable<PropertyListing>>.Success(listings);
            }
            catch(Exception ex)
            {
                return Result<IEnumerable<PropertyListing>>.Failure(ex.InnerException!.ToString());
            }
            
        }

        public async Task<Result<PropertyListing>> GetListingByIdAsync(Guid id)
        {
            try
            {
                var listing = await context.PropertyListings.FindAsync(id);
                return Result<PropertyListing>.Success(listing);
            }
            catch (Exception ex)
            {
                return Result<PropertyListing>.Failure(ex.InnerException!.ToString());
            }

        }

        public async Task<Result<IEnumerable<PropertyListing>>> GetListingsByUserId(Guid userId)
        {
            try
            {
                var listings = await context.PropertyListings
                                .Where(listing => listing.UserID == userId)
                                .ToListAsync();
                return Result<IEnumerable<PropertyListing>>.Success(listings);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<PropertyListing>>.Failure(ex.InnerException!.ToString());
            }
        }

        public async Task<Result<Guid>> AddListingAsync(PropertyListing listing)
        {
            try
            {
                await context.PropertyListings.AddAsync(listing);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(listing.PropertyId);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.InnerException!.ToString());
            }
        }

        public async Task<Result<Guid>> UpdateListingAsync(PropertyListing listing)
        {
            try
            {
                var existingListing = await context.PropertyListings.FindAsync(listing.PropertyId);
                if (existingListing != null)
                {
                    context.Entry(existingListing).CurrentValues.SetValues(listing);
                    await context.SaveChangesAsync();

                    return Result<Guid>.Success(listing.PropertyId);
                }
                else
                {
                    return Result<Guid>.Failure("Listing not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.InnerException!.ToString());
            }
            
        }

        public async Task<Result<Guid>> DeleteListingAsync(Guid id)
        {
            try
            {
                var propertyListing = await context.PropertyListings.FindAsync(id);
                if (propertyListing != null)
                {
                    context.PropertyListings.Remove(propertyListing);
                    await context.SaveChangesAsync();
                    return Result<Guid>.Success(id);
                }
                return Result<Guid>.Failure("Property Listing not found");
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.InnerException!.ToString());
            }
        }
    }
}
