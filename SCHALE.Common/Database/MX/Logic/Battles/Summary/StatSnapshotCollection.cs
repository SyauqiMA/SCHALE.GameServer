using System.Collections.ObjectModel;
using SCHALE.Common.FlatData;

namespace MX.Logic.Battles.Summary
{
    public class StatSnapshotCollection : KeyedCollection<StatType, StatSnapshot>
    {
        // public bool ShouldWriteStatType(StatType statType) { }

        protected override StatType GetKeyForItem(StatSnapshot item)
        {
            return item.Stat;
        }

        // public void WriteAtInit(StatType statType, long value) { }

        // public void WriteAtFinalize(StatType statType, long value) { }
    }
}
