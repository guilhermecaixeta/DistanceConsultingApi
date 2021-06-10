using Craftable.Core.valueObjects;
using FluentValidation;

namespace Craftable.Core.validators
{
    public class DistanceValidator : AbstractValidator<Distance>
    {
        public DistanceValidator()
        {
            RuleFor(distance => distance.DistanceInKilometer).GreaterThanOrEqualTo(0);
            RuleFor(distance => distance.DistanceInMiles).GreaterThanOrEqualTo(0);
        }
    }
}