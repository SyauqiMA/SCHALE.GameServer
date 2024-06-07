using Newtonsoft.Json;

namespace MX.Logic.Data
{
    public class RaidMemberDescription : IEquatable<RaidMemberDescription>
    {
        public long AccountId { get; set; }

        public string AccountName { get; set; }

        public long CharacterId { get; set; }

        [JsonIgnore]
        public long DamageGiven { get; }

        [JsonIgnore]
        public long GroggyGiven { get; }

        public RaidDamageCollection DamageCollection { get; set; }

        public bool Equals(RaidMemberDescription? other)
        {
            return CharacterId == other?.CharacterId;
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as RaidMemberDescription);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
