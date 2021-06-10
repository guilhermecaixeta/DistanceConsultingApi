using System.Text.Json.Serialization;

namespace Craftable.Web.DTO
{
    public class PostalcodeDTO
    {
        [JsonPropertyName("postalcode")]
        public string Code { get; set; }

        [JsonPropertyName("date")]
        public string Date { get; set; }
    }
}