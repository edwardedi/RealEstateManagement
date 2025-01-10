using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
namespace Application.Use_Cases.Authentication.Commands
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, Result<string>>
    {
        private readonly IUserAuthRepository userRepository;

        public LoginUserCommandHandler(IUserAuthRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<Result<string>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Email = request.Email,
                PasswordHash = request.Password
            };
            var result = await userRepository.Login(user);
            if (result.IsSuccess)
            {
                return Result<string>.Success(result.Data);
            }
            return Result<string>.Failure(result.ErrorMessage);
        }
    }
}