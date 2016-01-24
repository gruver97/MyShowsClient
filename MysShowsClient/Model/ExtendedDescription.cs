using System.Collections.Generic;
using Newtonsoft.Json;

namespace MysShowsClient.Model
{
    public class ExtendedDescription : ShortDescription
    {
        [JsonProperty("episodes")]
        [JsonIgnore]
        public List<Episode> Episodes { get; set; }
    }
}