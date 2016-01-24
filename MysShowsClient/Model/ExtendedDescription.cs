using System.Collections.Generic;
using Newtonsoft.Json;

namespace MysShowsClient.Model
{
    public class ExtendedDescription : ShortDescription
    {
        [JsonProperty("episodes")]
        public List<Episode> Episodes { get; set; }
    }
}