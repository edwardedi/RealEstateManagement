using MediatR;

public class LoginUserCommand : IRequest<string>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
