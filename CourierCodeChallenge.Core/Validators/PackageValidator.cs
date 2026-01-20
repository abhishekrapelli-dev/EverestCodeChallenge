using CourierCodeChallenge.Core.Models;
using FluentValidation;

namespace CourierCodeChallenge.Core.Validators
{
    public sealed class PackageValidator : AbstractValidator<Package>
    {
        public PackageValidator(int maxLoad)
        {
            RuleFor(x => x.Weight)
             .GreaterThan(0).WithMessage("Package weight should be greater than 0");

            RuleFor(x => x.Weight)
             .LessThanOrEqualTo(maxLoad).WithMessage(x => $"Package weight({x.Weight}) should not be greater than max load({maxLoad})");

            RuleFor(x => x.Distance)
             .GreaterThan(0).WithMessage("Package delivery distance should be greater than 0");
        }
    }
}
