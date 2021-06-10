using System.Text.Json.Serialization;

namespace Craftable.Web.DTO
{
    public class PostcodeDistanceDTO
    {
        [JsonPropertyName("postalcode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("distance_kilometer")]
        public double DistanceInKilometer { get; set; }

        [JsonPropertyName("distance_miles")]
        public double DistanceInMiles { get; set; }
    }
}