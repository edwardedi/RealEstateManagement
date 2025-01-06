using Domain.Common;
using Domain.Entities;

namespace Domain.Repositories
{
    public interface IUserAuthRepository
    {
        Task<Result<Guid>> Register(User user, CancellationToken cancellationToken);
        Task<Result<string>> Login(User user);
    }
}
