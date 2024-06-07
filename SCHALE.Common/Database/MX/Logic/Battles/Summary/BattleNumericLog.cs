using MX.Logic.BattleEntities;

namespace MX.Logic.Battles.Summary
{
    public class BattleNumericLog : IEquatable<BattleNumericLog>
    {
        public BattleEntityType EntityType { get; set; }
        public BattleLogCategory Category { get; set; }
        public BattleLogSourceType Source { get; set; }
        public long CalculatedSum { get; set; }
        public long AppliedSum { get; set; }
        public long Count { get; set; }
        public long CriticalMultiplierMax { get; set; }
        public long CriticalCount { get; set; }
        public long CalculatedMin { get; set; }
        public long CalculatedMax { get; set; }
        public long AppliedMin { get; set; }
        public long AppliedMax { get; set; }

        public bool Equals(BattleNumericLog? other)
        {
            // TODO
            return EntityType == other?.EntityType;
        }
    }
}
