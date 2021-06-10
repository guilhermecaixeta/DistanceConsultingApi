using Craftable.Core.validators;

namespace Craftable.Core.valueObjects
{
    public record Coordinates : BaseValueObject<Coordinates, CoordinatesValidator>
    {
        public Coordinates(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Validate(this);
        }

        public double Longitude { get; init; }
        public double Latitude { get; init; }
    }
}