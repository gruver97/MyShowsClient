using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MysShowsClient.Model.Parser
{
    public class Parser : IParser
    {
        public async Task<IEnumerable<ShortDescription>> DeserializeObjectAsync(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException("Argument is null or whitespace", nameof(jsonString));
            try
            {
                var result = await Task.Factory.StartNew(() =>
                {
                    var jObject = JObject.Parse(jsonString);
                    return
                        (from property in jObject.Properties()
                            from child in property.Children()
                            select child.ToObject<ShortDescription>()).ToList();
                });
                return result;
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}