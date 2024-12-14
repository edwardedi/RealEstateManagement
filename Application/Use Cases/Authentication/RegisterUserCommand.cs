using MediatR;

public class RegisterUserCommand : IRequest<Guid>
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Password { get; set; }
}
