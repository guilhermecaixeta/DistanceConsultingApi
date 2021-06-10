using Craftable.Core.valueObjects;
using FluentValidation;

namespace Craftable.Core.validators
{
    public class CoordinatesValidator : AbstractValidator<Coordinates>
    {
        public CoordinatesValidator()
        {
            RuleFor(coordinates => coordinates).NotNull();
            RuleFor(coordinates => coordinates.Latitude).NotEmpty().NotNull().ExclusiveBetween(-90, 90);
            RuleFor(coordinates => coordinates.Longitude).NotEmpty().NotNull().ExclusiveBetween(-180, 180);
        }
    }
}