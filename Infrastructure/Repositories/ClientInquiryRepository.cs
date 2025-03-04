﻿using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClientInquiryRepository : IClientInquiryRepository
    {
        private readonly ApplicationDbContext context;
        public ClientInquiryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public async Task<Result<IEnumerable<ClientInquiry>>> GetAllInquiriesAsync()
        {
            try
            {
                var inquiries = await context.ClientInquiries.ToListAsync();
                return Result<IEnumerable<ClientInquiry>>.Success(inquiries);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ClientInquiry>>.Failure($"An error occurred while retrieving inquiries: {ex.Message}");
            }

        }
        public async Task<Result<ClientInquiry>> GetInquiryByIdAsync(Guid id)
        {
            try{
                var inquiry = await context.ClientInquiries.FindAsync(id);
                if (inquiry == null)
                {
                    return Result<ClientInquiry>.Failure("Inquiry not found.");
                }

                return Result<ClientInquiry>.Success(inquiry);
            }
            catch (Exception ex)
            {
                return Result<ClientInquiry>.Failure($"An error occurred while retrieving inquiry: {ex.Message}");
            }
        }
        public async Task<Result<IEnumerable<ClientInquiry>>> GetInquiriesByClientId(Guid clientId)
        {
            try
            {
                var inquiries = await context.ClientInquiries
                                                    .Where(inquiry => inquiry.ClientId == clientId)
                                                    .ToListAsync();
                return Result<IEnumerable<ClientInquiry>>.Success(inquiries);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ClientInquiry>>.Failure($"An error occurred while retrieving inquiries: {ex.Message}");
            }
        }
        public async Task<Result<Guid>> AddInquiryAsync(ClientInquiry inquiry)
        {
            try
            {
                await context.ClientInquiries.AddAsync(inquiry);
                await context.SaveChangesAsync();
                return Result<Guid>.Success(inquiry.InquiryId);
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure($"An error occurred while adding inquiry: {ex.Message}");
            }
        }
        public async Task<Result<Guid>> UpdateInquiryAsync(ClientInquiry inquiry)
        {
            try
            {
                var existingInquiry = await context.ClientInquiries.FindAsync(inquiry.InquiryId);
                if (existingInquiry != null)
                {
                    context.Entry(existingInquiry).CurrentValues.SetValues(inquiry);
                    await context.SaveChangesAsync();

                    return Result<Guid>.Success(inquiry.InquiryId);
                }
                else
                {
                    return Result<Guid>.Failure("Inquiry not found.");
                }
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure($"An error occurred while updating inquiry: {ex.Message}");
            }
        }
        public async Task<Result<Guid>> DeleteInquiryAsync(Guid id)
        {
            try
            {
                var inquiry = await context.ClientInquiries.FindAsync(id);
                if (inquiry != null)
                {
                    context.ClientInquiries.Remove(inquiry);
                    await context.SaveChangesAsync();
                    return Result<Guid>.Success(id);
                }
                return Result<Guid>.Failure("Inquiry not found");
            }
            catch (Exception ex)
            {
                return Result<Guid>.Failure($"An error occurred while deleting inquiry: {ex.Message}");
            }
        }

        public async Task<Result<IEnumerable<PropertyListing>>> SearchAllPropertiesAsync(string searchQuery)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchQuery))
                {
                    return Result<IEnumerable<PropertyListing>>.Failure("Search query cannot be null or empty.");
                }

                var properties = await context.PropertyListings
                                        .Where(property => (property.Title !=null && property.Title.ToUpper().Contains(searchQuery.ToUpper())) || (property.Description !=null && property.Description.Contains(searchQuery)))
                                        .ToListAsync();
                return Result<IEnumerable<PropertyListing>>.Success(properties);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<PropertyListing>>.Failure($"An error occurred while retrieving properties: {ex.Message}");
            }
        }
    }
}
