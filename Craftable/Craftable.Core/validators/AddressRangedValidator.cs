using Craftable.Core.aggregate.postcode;
using FluentValidation;

namespace Craftable.Core.validators
{
    public class AddressRangedValidator : AbstractValidator<AddressRegister>
    {
        public AddressRangedValidator()
        {
            RuleFor(address => address).NotNull();
            RuleFor(address => address.Postcode).NotEmpty().NotNull();
            RuleFor(address => address.Distance).NotEmpty().SetValidator(new DistanceValidator());
            RuleFor(address => address.Coordinates).NotEmpty().SetValidator(new CoordinatesValidator());
        }
    }
}