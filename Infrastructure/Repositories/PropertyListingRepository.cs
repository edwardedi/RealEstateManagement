﻿using Domain.Common;
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
        public async Task<IEnumerable<PropertyListing>> GetAllListingsAsync()
        {
            return await context.PropertyListings.ToListAsync();
        }

        public async Task<PropertyListing> GetListingByIdAsync(Guid id)
        {
            return await context.PropertyListings.FindAsync(id);
        }

        public Task<IEnumerable<PropertyListing>> GetListingsByUserId(Guid userId)
        {
            throw new NotImplementedException();
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
                    existingListing.Address = listing.Address;
                    existingListing.Type = listing.Type;
                    existingListing.Price = listing.Price;
                    existingListing.SquareFootage = listing.SquareFootage;
                    existingListing.NumberOfBedrooms = listing.NumberOfBedrooms;
                    existingListing.NumberOfBathrooms = listing.NumberOfBathrooms;
                    existingListing.Description = listing.Description;
                    existingListing.Status = listing.Status;
                    existingListing.ListingDate = listing.ListingDate;
                    existingListing.ImageURLs = listing.ImageURLs;

                    context.PropertyListings.Update(existingListing);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"Property listing with ID {listing.PropertyId} not found.");
                }

                return Result<Guid>.Success(listing.PropertyId);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure(ex.InnerException!.ToString());
            }
            
        }

        public Task DeleteListingAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
