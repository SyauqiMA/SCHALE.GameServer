using System.Collections.ObjectModel;
using MX.Logic.BattleEntities;

namespace MX.Logic.Battles.Summary
{
    public class HeroSummaryCollection : KeyedCollection<EntityId, HeroSummary>
    {
        // public HeroSummaryCollection() { }
        // public HeroSummaryCollection(EntityIdComparer iComparer) { }
        protected override EntityId GetKeyForItem(HeroSummary item)
        {
            return item.BattleEntityId;
        }
    }
}
