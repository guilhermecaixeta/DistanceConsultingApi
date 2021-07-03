using Craftable.Core.validators;
using System.Collections.Generic;

namespace Craftable.Core.valueObjects
{
    public class Coordinates : ValueObjectValidator<Coordinates, CoordinatesValidator>
    {
        public Coordinates(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Validate(this);
        }

        public double Longitude { get; init; }
        public double Latitude { get; init; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Latitude;
            yield return Longitude;
        }
    }
}