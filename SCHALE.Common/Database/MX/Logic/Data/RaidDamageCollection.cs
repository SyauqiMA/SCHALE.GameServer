using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MX.Logic.Data
{
    public class RaidDamageCollection : KeyedCollection<int, RaidDamage>
    {
        protected override int GetKeyForItem(RaidDamage item)
        {
            return item.Index;
        }

        [JsonIgnore]
        public int MaxIndex { get; }

        [JsonIgnore]
        public long TotalDamage { get; }

        [JsonIgnore]
        public long CurrentDamage { get; }

        [JsonIgnore]
        public long TotalGroggyPoint { get; }

        [JsonIgnore]
        public long CurrentGroggyPoint { get; }
    }
}
