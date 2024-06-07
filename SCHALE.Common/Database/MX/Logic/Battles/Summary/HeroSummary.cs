using MX.Logic.BattleEntities;
using MX.Logic.Data;
using Newtonsoft.Json;
using SCHALE.Common.FlatData;

namespace MX.Logic.Battles.Summary
{
    public class HeroSummary : IEquatable<HeroSummary>
    {
        public long ServerId { get; set; }
        public long OwnerAccountId { get; set; }
        public EntityId BattleEntityId { get; set; }

        [Obsolete("CharacterId로 교체중입니다.")]
        public long HeroId { get; set; }
        public long CharacterId { get; set; }
        public long CostumeId { get; set; }
        public int CombatStyleIndex { get; set; }
        public int Grade { get; set; }
        public int Level { get; set; }
        public IDictionary<StatType, int> PotentialStatLevel { get; set; }
        public int ExSkillLevel { get; set; }
        public int PublicSkillLevel { get; set; }
        public int PassiveSkillLevel { get; set; }
        public int ExtraPassiveSkillLevel { get; set; }
        public int FavorRank { get; set; }
        public StatSnapshotCollection StatSnapshotCollection { get; set; }
        public long HPRateBefore { get; set; }
        public long HPRateAfter { get; set; }
        public int CrowdControlCount { get; set; }
        public int CrowdControlDuration { get; set; }
        public int EvadeCount { get; set; }
        public int DamageImmuneCount { get; set; }
        public int CrowdControlImmuneCount { get; set; }
        public long MaxAttackPower { get; set; }
        public int AverageCriticalRate { get; set; }
        public int AverageStabilityRate { get; set; }
        public int AverageAccuracyRate { get; set; }
        public int DeadFrame { get; set; }
        public long DamageGivenAbsorbedSum { get; set; }
        public TacticEntityType TacticEntityType { get; set; }

        [JsonIgnore]
        public HeroSummaryDetailFlag DetailFlag { get; }

        [JsonIgnore]
        public bool IsDead { get; }
        public List<BattleNumericLog> GivenNumericLogs { get; set; }
        public List<BattleNumericLog> TakenNumericLogs { get; set; }
        public List<BattleNumericLog> ObstacleBattleNumericLogs { get; set; }

        public List<EquipmentSetting> Equipments { get; set; }
        public Nullable<WeaponSetting> CharacterWeapon { get; set; }

        [JsonIgnore]
        public IDictionary<int, long> HitPointByFrame { get; set; }

        public IDictionary<SkillSlot, int> SkillCount { get; set; }

        [JsonIgnore]
        public int ExSkillUseCount { get; }
        public KillLogCollection KillLog { get; set; }

        [JsonIgnore]
        public int KillCount { get; }

        [JsonIgnore]
        public Dictionary<int, string> FullSnapshot { get; set; }
        public static IEqualityComparer<HeroSummary> HeroSummaryAlmostEqualityComparer { get; }

        public bool Equals(HeroSummary? other)
        {
            return BattleEntityId.Equals(other?.BattleEntityId);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object? obj)
        {
            return obj is HeroSummary v && Equals(v);
        }
    }
}
