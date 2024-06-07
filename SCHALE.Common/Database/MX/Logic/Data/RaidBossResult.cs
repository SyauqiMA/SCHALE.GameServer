using Newtonsoft.Json;

namespace MX.Logic.Data
{
    public struct RaidBossResult : IEquatable<RaidBossResult>
    {
        [JsonIgnore]
        public int Index { get; set; }

        [JsonIgnore]
        public long GivenDamage { get; set; }

        [JsonIgnore]
        public long GivenGroggyPoint { get; set; }

        public RaidDamage RaidDamage { get; set; }

        public long EndHpRateRawValue { readonly get; set; }

        public long GroggyRateRawValue { readonly get; set; }

        public int GroggyCount { readonly get; set; }

        public List<long> SubPartsHPs { readonly get; set; }

        public long AIPhase { readonly get; set; }

        public bool Equals(RaidBossResult other)
        {
            return this.Index == other.Index;
        }
    }
}
