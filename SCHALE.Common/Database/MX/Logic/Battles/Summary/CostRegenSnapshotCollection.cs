using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace MX.Logic.Battles.Summary
{
    public class CostRegenSnapshotCollection : KeyedCollection<long, SkillCostRegenSnapshot>
    {
        [JsonIgnore]
        private SkillCostRegenSnapshot _lastSnapshot;

        protected override long GetKeyForItem(SkillCostRegenSnapshot item)
        {
            // TODO
            return item.Frame;
        }

        // public void WriteIfChanged(long frame, float regen) { }
    }
}
