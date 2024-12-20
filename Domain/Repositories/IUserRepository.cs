﻿using Domain.Common;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Result<IEnumerable<User>>> GetAllUsersAsync();
        Task<Result<User>> GetUserByIdAsync(Guid id);
        Task<Result<IEnumerable<PropertyListing>>> GetListingsByUserId(Guid userId);
        Task<Result<IEnumerable<ClientInquiry>>> GetClientInquiriesByUserId(Guid userId);
        Task<Result<Guid>> AddUserAsync(User user);
        Task<Result<Guid>> UpdateUserAsync(User user);
        Task<Result<Guid>> DeleteUserAsync(Guid id);
    }
}
