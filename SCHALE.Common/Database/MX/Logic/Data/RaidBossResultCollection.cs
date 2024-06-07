using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MX.Logic.Data
{
    public class RaidBossResultCollection : KeyedCollection<int, RaidBossResult>
    {
        [JsonIgnore]
        public int LastIndex { get; set; }

        [JsonIgnore]
        public long TotalDamage { get; set; }

        [JsonIgnore]
        public long CurrentDamage { get; set; }

        [JsonIgnore]
        public long TotalGroggyPoint { get; set; }

        [JsonIgnore]
        public long CurrentGroggyPoint { get; set; }

        [JsonIgnore]
        public int TotalGroggyCount { get; set; }

        protected override int GetKeyForItem(RaidBossResult item)
        {
            return item.Index;
        }
    }
}
