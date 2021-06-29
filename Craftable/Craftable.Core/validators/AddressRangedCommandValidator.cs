using Craftable.Core.commands;
using FluentValidation;

namespace Craftable.Core.validators
{
    public class AddressRangedCommandValidator : AbstractValidator<AddressRangedCommand>
    {
        public AddressRangedCommandValidator()
        {
            RuleFor(coordinates => coordinates.Latitude).NotEmpty().NotNull().ExclusiveBetween(-90, 90);
            RuleFor(coordinates => coordinates.Longitude).NotEmpty().NotNull().ExclusiveBetween(-180, 180);
            RuleFor(address => address.DistanceInKilometer).GreaterThan(0);
            RuleFor(address => address.DistanceInMiles).GreaterThan(0);
            RuleFor(address => address.Postcode).NotNull().NotEmpty();
            RuleFor(address => address.Country).NotNull().NotEmpty();
            RuleFor(address => address).NotNull();
        }
    }
}