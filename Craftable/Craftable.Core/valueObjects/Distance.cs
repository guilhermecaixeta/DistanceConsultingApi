using Craftable.Core.validators;
using System.Collections.Generic;

namespace Craftable.Core.valueObjects
{
    public class Distance : ValueObjectValidator<Distance, DistanceValidator>
    {
        public Distance(double distanceInKilometer, double distanceInMiles)
        {
            DistanceInKilometer = distanceInKilometer;
            DistanceInMiles = distanceInMiles;
            Validate(this);
        }

        public double DistanceInKilometer { get; init; }
        public double DistanceInMiles { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return DistanceInMiles;
            yield return DistanceInKilometer;
        }
    }
}