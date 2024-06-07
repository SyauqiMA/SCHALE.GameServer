using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace MX.Logic.Battles.Summary
{
    [ComVisible(false)]
    [JsonConverter(typeof(KillLogCollectionConverter))]
    public class KillLogCollection : Collection<KillLog>
    {
        // public void Add(int frame, EntityId targetId) { }
    }
}
