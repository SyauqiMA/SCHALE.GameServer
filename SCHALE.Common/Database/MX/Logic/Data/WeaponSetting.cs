using Newtonsoft.Json;

namespace MX.Logic.Data
{
    public struct WeaponSetting : IEquatable<WeaponSetting>
    {
        // Fields
        public const int InvalidId = -1;

        // Properties
        [JsonIgnore]
        public bool IsValid { get; }
        public long UniqueId { get; set; }
        public int StarGrade { get; set; }
        public int Level { get; set; }

        public bool Equals(WeaponSetting other)
        {
            return UniqueId == other.UniqueId;
        }

        public override bool Equals(object? obj)
        {
            return obj is WeaponSetting setting && Equals(setting);
        }

        public static bool operator ==(WeaponSetting left, WeaponSetting right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(WeaponSetting left, WeaponSetting right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
