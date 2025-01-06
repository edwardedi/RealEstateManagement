using FluentValidation;

namespace Application.Use_Cases.Commands
{
    public class CreatePropertyListingCommandValidator : AbstractValidator<CreatePropertyListingCommand>
    {
        public CreatePropertyListingCommandValidator()
        {
            RuleFor(x => x.Address).MaximumLength(200);
            RuleFor(x => x.Title).MaximumLength(100);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.SquareFootage).GreaterThan(0);
            RuleFor(x => x.NumberOfBedrooms).GreaterThanOrEqualTo(0);
            RuleFor(x => x.NumberOfBathrooms).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(400);
            RuleFor(x => x.Status).NotEmpty()
                                  .Must(status => status == "available" || status == "sold")
                                  .WithMessage("Status must be either 'available' or 'sold'.");
            RuleFor(x => x.ListingDate).NotEmpty();
            RuleFor(x => x.ImageURLs).NotEmpty();
            RuleFor(x => x.UserID).NotEmpty();
        }
    }
}
