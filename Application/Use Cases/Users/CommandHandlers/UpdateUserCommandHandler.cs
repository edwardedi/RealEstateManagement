using Application.Use_Cases.Users.Commands;
using AutoMapper;
using Domain.Common;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Use_Cases.Users.CommandHandlers
{
    public  class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Result<Guid>>
    {
        private readonly IUserRepository repository;
        private readonly IMapper mapper;

        public UpdateUserCommandHandler(IUserRepository repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<Result<Guid>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request);

            if (!string.IsNullOrEmpty(request.Password))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            }

            var result = await repository.UpdateUserAsync(user);
            if (result.IsSuccess)
            {
                return Result<Guid>.Success(result.Data);
            }
            return Result<Guid>.Failure(result.ErrorMessage);
        }

    }
}
