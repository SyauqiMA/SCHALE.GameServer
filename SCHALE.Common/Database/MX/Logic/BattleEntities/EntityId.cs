using Newtonsoft.Json;

namespace MX.Logic.BattleEntities
{
    [Serializable]
    public struct EntityId : IComparable, IComparable<EntityId>, IEquatable<EntityId>
    {
        // Fields
        private const uint typeMask = 4278190080;
        private const int instanceIdMask = 16777215;

        [JsonProperty]
        private int uniqueId;

        // Properties
        public static EntityId Invalid { get; }

        [JsonIgnore]
        public BattleEntityType EntityType { get; }

        [JsonIgnore]
        public int InstanceId { get; }

        [JsonIgnore]
        public int UniqueId { get; }

        [JsonIgnore]
        public bool IsValid { get; }

        // Methods


        public static EntityId Parse(string value)
        {
            var id = int.Parse(value);
            return new EntityId(id);
        }

        // public static bool TryParse(string value, out EntityId outValue) { }


        // public static EntityId Clone(int value) { }


        // public BattleEntityType get_EntityType() { }


        // public int get_InstanceId() { }


        // public int get_UniqueId() { }

        [JsonConstructor]
        private EntityId(int id)
        {
            uniqueId = id;
        }

        // public EntityId(BattleEntityType typeId, int instanceId) { }

        // public bool get_IsValid() { }

        // public static bool op_LessThan(EntityId lhs, EntityId rhs) { }

        // public static bool op_GreaterThan(EntityId lhs, EntityId rhs) { }

        public int CompareTo(object? obj)
        {
            // TODO
            return 1;
        }

        public int CompareTo(EntityId other)
        {
            // TODO
            return uniqueId - other.uniqueId;
        }

        // public static bool op_LessThanOrEqual(EntityId left, EntityId right) { }

        // public static bool op_GreaterThanOrEqual(EntityId left, EntityId right) { }

        // public static bool op_Equality(EntityId lhs, EntityId rhs) { }

        // public static bool op_Inequality(EntityId lhs, EntityId rhs) { }

        public bool Equals(EntityId other)
        {
            // TODO
            return uniqueId == other.uniqueId;
        }

        // public override bool Equals(object obj) { }

        // public override int GetHashCode() { }

        public override string ToString()
        {
            return uniqueId.ToString();
        }

        // private static void .cctor() { }
    }
}
