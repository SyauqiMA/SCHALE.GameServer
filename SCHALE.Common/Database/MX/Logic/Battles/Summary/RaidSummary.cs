using MX.Logic.Data;

namespace MX.Logic.Battles.Summary
{
    public class RaidSummary
    {
        public long RaidSeasonId { get; set; }

        public long GivenDamage { get; set; }

        public int TotalGroggyCount { get; set; }

        public int RaidBossIndex { get; set; }

        public RaidBossResultCollection RaidBossResults { get; set; }
    }
}
