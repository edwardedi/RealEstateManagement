using Domain.Common;
using MediatR;
namespace Application.Use_Cases.Authentication.Commands
{
    public class RegisterUserCommand : IRequest<Result<Guid>>
    {
        public required string Email { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Password { get; set; }
    }
}
