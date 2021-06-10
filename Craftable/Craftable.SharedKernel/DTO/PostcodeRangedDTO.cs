namespace Craftable.SharedKernel.DTO
{
    public record PostcodeRangedDTO
    {
        public string Code { get; init; }
        public double DistanceFromHeathrowAirportInKilometer { get; init; }
        public double DistanceFromHeathrowAirportInMiles { get; init; }
    }
}