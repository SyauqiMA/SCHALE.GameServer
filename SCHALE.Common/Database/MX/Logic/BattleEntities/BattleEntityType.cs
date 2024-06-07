using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MX.Logic.BattleEntities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum BattleEntityType
    {
        None = 0,
        Character = 16777216,
        SkillActor = 33554432,
        Obstacle = 67108864,
        Point = 134217728,
        Projectile = 268435456,
        EffectArea = 536870912,
        Supporter = 1073741824,
        BattleItem = -2147483648,
    }
}
