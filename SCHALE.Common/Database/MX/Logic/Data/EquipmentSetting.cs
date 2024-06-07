using Newtonsoft.Json;

namespace MX.Logic.Data
{
    public struct EquipmentSetting : IEquatable<EquipmentSetting>
    {
        public const int InvalidId = -1;

        [JsonIgnore]
        public bool IsValid { get; }
        public long ServerId { get; set; }
        public long UniqueId { get; set; }
        public int Level { get; set; }
        public int Tier { get; set; }

        public bool Equals(EquipmentSetting other)
        {
            return UniqueId == other.UniqueId;
        }

        public override bool Equals(object obj)
        {
            return obj is EquipmentSetting setting && Equals(setting);
        }
    }
}
