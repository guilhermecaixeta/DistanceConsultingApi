using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Craftable.Web.DTO
{
    public class PostcodeRequestDTO
    {
        [JsonPropertyName("code")]
        [Required]
        public string Code { get; set; }
    }
}
