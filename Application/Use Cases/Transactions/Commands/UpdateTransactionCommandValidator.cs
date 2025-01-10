using FluentValidation;
using RealEstateManagement.Application.Transactions.Commands;

namespace Application.Use_Cases.Transactions.Commands
{
    public class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
    {
        public UpdateTransactionCommandValidator()
        {
            RuleFor(x => x.TransactionId).NotEmpty().Must(BeAValidGuid);
            RuleFor(x => x.PropertyId).NotEmpty().Must(BeAValidGuid);
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
