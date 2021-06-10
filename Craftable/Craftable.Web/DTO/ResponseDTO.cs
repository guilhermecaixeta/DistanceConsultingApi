using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;

namespace Craftable.Web.DTO
{
    public record ResponseDTO<T>
        where T : class
    {
        [JsonPropertyName("data")]
        public T Data { get; set; }

        [JsonIgnore]
        public bool Success { get; set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }

        [JsonPropertyName("errors")]
        public IReadOnlyList<string> Errors { get; set; }
    }
}