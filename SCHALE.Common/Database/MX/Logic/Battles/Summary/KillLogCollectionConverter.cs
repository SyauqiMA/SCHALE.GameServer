using MX.Logic.BattleEntities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MX.Logic.Battles.Summary
{
    public class KillLogCollectionConverter : JsonConverter<KillLogCollection>
    {
        public override KillLogCollection ReadJson(
            JsonReader reader,
            Type objectType,
            KillLogCollection? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer
        )
        {
            var collection = existingValue ?? [];
            var jsonObject = JObject.Load(reader);

            foreach (var property in jsonObject.Properties())
            {
                var entityId = EntityId.Parse(property.Name);
                var frame = property.Value.ToObject<int>();
                collection.Add(new KillLog { EntityId = entityId, Frame = frame });
            }

            return collection;
        }

        public override void WriteJson(
            JsonWriter writer,
            KillLogCollection? value,
            JsonSerializer serializer
        )
        {
            JObject jsonObject = [];

            foreach (var killLog in value ?? [])
            {
                jsonObject.Add(killLog.EntityId.ToString(), killLog.Frame);
            }

            jsonObject.WriteTo(writer);
        }
    }
}
