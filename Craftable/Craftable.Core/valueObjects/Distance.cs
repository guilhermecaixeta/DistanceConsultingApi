using Craftable.Core.validators;

namespace Craftable.Core.valueObjects
{
    public record Distance : BaseValueObject<Distance, DistanceValidator>
    {
        public Distance(double distanceInKilometer, double distanceInMiles)
        {
            DistanceInKilometer = distanceInKilometer;
            DistanceInMiles = distanceInMiles;
            Validate(this);
        }

        public double DistanceInKilometer { get; init; }
        public double DistanceInMiles { get; init; }
    }
}