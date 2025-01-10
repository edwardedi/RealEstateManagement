using FluentValidation;

namespace Application.Use_Cases.Transactions.Commands
{
    public class CreateTransactionCommandValidator : AbstractValidator<CreateTransactionCommand>
    {
        public CreateTransactionCommandValidator() 
        { 
            RuleFor(x=>x.PropertyId).NotEmpty().Must(BeAValidGuid);
            RuleFor(x => x.BuyerId).NotEmpty().Must(BeAValidGuid);
            RuleFor(x => x.SellerId).NotEmpty().Must(BeAValidGuid);
            RuleFor(x => x.SalePrice).NotEmpty().GreaterThan(0);
        }
        private static bool BeAValidGuid(Guid guid)
        {
            return Guid.TryParse(guid.ToString(), out _);
        }
    }
}
