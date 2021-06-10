namespace Craftable.SharedKernel.DTO
{
    public record PostcodeAddressRangedDTO
    {
        public string Postcode { get; init; }
        public string Country { get; init; }
        public double Longitude { get; init; }
        public double Latitude { get; init; }
        public double DistanceFromHeathrowAirportInKilometers { get; init; }
        public double DistanceFromHeathrowAirportInMiles { get; init; }
    }
}