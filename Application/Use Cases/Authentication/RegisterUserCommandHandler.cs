using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
namespace Application.Use_Cases.Authentication.Commands
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<Guid>>
    {
        private readonly IUserAuthRepository repository;

        public RegisterUserCommandHandler(IUserAuthRepository repository) => this.repository = repository;

        public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                Name = request.Name,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            var result = await repository.Register(user, cancellationToken);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);

        }
    }
}
