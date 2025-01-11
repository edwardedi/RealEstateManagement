using Domain.Common;
using MediatR;

namespace Application.Use_Cases.Users.Commands
{
    public class UpdateUserCommand : IRequest<Result<Guid>>
    {
        public Guid UserId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Password { get; set; }
    }
}
