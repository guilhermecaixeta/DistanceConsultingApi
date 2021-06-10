using Refit;

namespace Craftable.Infrastructure.models
{
    public class PostcodeAddress
    {
        [AliasAs("postcode")]
        public string Postcode { get; set; }

        [AliasAs("country")]
        public string Country { get; set; }

        [AliasAs("longitude")]
        public double Longitude { get; set; }

        [AliasAs("latitude")]
        public double Latitude { get; set; }
    }
}