using System.Collections.Generic;
using Newtonsoft.Json;

namespace MysShowsClient.Model
{
    public class ExtendedDescription : Description
    {
        [JsonProperty("episodes")]
        public List<Episode> Episodes { get; set; }
    }
}