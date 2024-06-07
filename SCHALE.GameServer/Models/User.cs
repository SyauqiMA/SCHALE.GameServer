using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace SCHALE.GameServer.Models
{
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    class UserAgreementResponse
    {
        [JsonProperty("version")]
        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonProperty("data")]
        [JsonPropertyName("data")]
        public List<string> Data { get; set; }
    }

    class UserLoginResponse : BaseResponse
    {
        [JsonProperty("accessToken")]
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("birth")]
        [JsonPropertyName("birth")]
        public dynamic? Birth { get; set; }

        [JsonProperty("transcode")]
        [JsonPropertyName("transcode")]
        public string Transcode { get; set; }

        [JsonProperty("current_timestamp_ms")]
        [JsonPropertyName("current_timestamp_ms")]
        public long CurrentTimestampMs { get; set; }

        [JsonProperty("check7until")]
        [JsonPropertyName("check7until")]
        public int Check7Until { get; set; }

        [JsonProperty("migrated")]
        [JsonPropertyName("migrated")]
        public bool Migrated { get; set; }

        [JsonProperty("show_migrate_page")]
        [JsonPropertyName("show_migrate_page")]
        public bool ShowMigratePage { get; set; }

        [JsonProperty("channelId")]
        [JsonPropertyName("channelId")]
        public string ChannelId { get; set; }

        [JsonProperty("kr_kmc_status")]
        [JsonPropertyName("kr_kmc_status")]
        public int KrKmcStatus { get; set; }
    }

    class UserCreateResponse : BaseResponse
    {
        [JsonProperty("uid")]
        [JsonPropertyName("uid")]
        public long Uid { get; set; }

        [JsonProperty("token")]
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonProperty("isNew")]
        [JsonPropertyName("isNew")]
        public int IsNew { get; set; }
    }
}
