using MX.Logic.BattleEntities;

namespace MX.Logic.Battles.Summary
{
    [Serializable]
    public struct KillLog : IEquatable<KillLog>
    {
        public int Frame { get; set; }
        public EntityId EntityId { get; set; }

        public bool Equals(KillLog other)
        {
            return EntityId.Equals(other.EntityId);
        }

        public override bool Equals(object? obj)
        {
            return obj is KillLog log && Equals(log);
        }

        public static bool operator ==(KillLog left, KillLog right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(KillLog left, KillLog right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
