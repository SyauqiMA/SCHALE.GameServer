using MX.Logic.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MX.Logic.Battles.Summary
{
    public class BattleSummary : IEquatable<BattleSummary>
    {
        [JsonProperty(Order = 1)]
        public long HashKey { get; set; }

        [JsonProperty(Order = 3)]
        public bool IsBossBattle { get; set; }

        [JsonProperty(Order = 3)]
        public BattleTypes BattleType { get; set; }

        [JsonProperty(Order = 3)]
        public long StageId { get; set; }

        [JsonProperty(Order = 3)]
        public long GroundId { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        [JsonProperty(Order = 3)]
        public GroupTag Winner { get; set; }

        [JsonIgnore]
        public bool IsPlayerWin { get; }

        [JsonProperty(Order = 3)]
        public BattleEndType EndType { get; set; }

        [JsonProperty(Order = 3)]
        public int EndFrame { get; set; }

        [JsonProperty(
            Order = 3,
            NullValueHandling = (NullValueHandling)1,
            DefaultValueHandling = (DefaultValueHandling)1
        )]
        public GroupSummary Group01Summary { get; set; }

        [JsonProperty(
            Order = 3,
            NullValueHandling = (NullValueHandling)1,
            DefaultValueHandling = (DefaultValueHandling)1
        )]
        public GroupSummary Group02Summary { get; set; }

        // [JsonProperty(
        //     Order = 3,
        //     NullValueHandling = (NullValueHandling)1,
        //     DefaultValueHandling = (DefaultValueHandling)1
        // )]
        // public WeekDungeonSummary WeekDungeonSummary { get; set; }

        [JsonProperty(
            Order = 3,
            NullValueHandling = (NullValueHandling)1,
            DefaultValueHandling = (DefaultValueHandling)1
        )]
        public RaidSummary RaidSummary { get; set; }

        [JsonProperty(
            Order = 3,
            NullValueHandling = (NullValueHandling)1,
            DefaultValueHandling = (DefaultValueHandling)1
        )]
        public ArenaSummary ArenaSummary { get; set; }

        [JsonIgnore]
        public TimeSpan EndTime { get; }

        [JsonProperty(Order = 3, DefaultValueHandling = (DefaultValueHandling)1)]
        public int ContinueCount { get; set; }

        [JsonProperty(Order = 3)]
        public float ElapsedRealtime { get; set; }

        [JsonIgnore]
        public string FindGiftClearText { get; }

        [JsonIgnore]
        public long EventContentId { get; set; }

        [JsonIgnore]
        public long FixedEchelonId { get; set; }
        public bool IsAbort { get; set; }
        public bool IsDefeatBattle { get; set; }

        [JsonIgnore]
        public bool IsDefeatFailure { get; }

        public bool Equals(BattleSummary? other)
        {
            return HashKey == other?.HashKey;
        }
    }
}
