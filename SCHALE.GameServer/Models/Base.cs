using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SCHALE.GameServer.Models
{
    class BaseResponse
    {
        [JsonProperty("result")]
        [JsonPropertyName("result")]
        public int Result { get; set; }
    }
}
