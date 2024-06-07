using System.Collections.ObjectModel;

namespace MX.Logic.Data
{
    public class RaidMemberCollection : KeyedCollection<long, RaidMemberDescription>
    {
        public long TotalDamage { get; }

        protected override long GetKeyForItem(RaidMemberDescription item)
        {
            return item.CharacterId;
        }

        public IEnumerable<RaidDamage> RaidDamages { get; }
    }
}
