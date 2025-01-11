using FluentValidation;

namespace Application.Use_Cases.Users.Commands
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.UserId).NotEmpty().Must(BeAValidGuid);
            RuleFor(x => x.Name).MaximumLength(50);
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x => x.PhoneNumber).MaximumLength(15);
        }
        private static bool BeAValidGuid(Guid guid)
        {
            return Guid.TryParse(guid.ToString(), out _);
        }
    }
}
