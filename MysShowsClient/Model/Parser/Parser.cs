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
        public async Task<IEnumerable<ShortDescription>> DeserializeShortDescriptionAsync(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException("Argument is null or whitespace", nameof(jsonString));
            try
            {
                var result = await Task.Factory.StartNew(() =>
                {
                    var container = JToken.Parse(jsonString);
                    //вместо 404 сервер возвращает пустой массив
                    if (container.Type == JTokenType.Array)
                    {
                        var jArray = container as JArray;
                        if (!jArray.Any())
                        {
                            return new List<ShortDescription>();
                        }
                    }
                    if (container.Type != JTokenType.Object) return null;
                    var jObject = JObject.Parse(jsonString);
                    return
                        (from property in jObject.Properties()
                            from child in property.Children()
                            select child.ToObject<ShortDescription>()).ToList();
                }).ConfigureAwait(false);
                return result;
            }
            catch (JsonException)
            {
                return null;
            }
        }

        public async Task<ExtendedDescription> DeserializeExtendedDescriptionAsync(string jsonString)
        {
            if (string.IsNullOrWhiteSpace(jsonString))
                throw new ArgumentException("Argument is null or whitespace", nameof(jsonString));
            try
            {
                var result = await Task.Factory.StartNew(() =>
                {
                    var jObject = JObject.Parse(jsonString);
                    var extendedDescription = jObject.ToObject<ExtendedDescription>();
                    extendedDescription.Episodes = new List<Episode>();
                    foreach (
                        var episode in
                            jObject["episodes"].Select(property => property.FirstOrDefault().ToObject<Episode>()))
                    {
                        extendedDescription.Episodes.Add(episode);
                    }
                    return extendedDescription;
                }).ConfigureAwait(false);
                return result;
            }
            catch (JsonException)
            {
                return null;
            }
        }
    }
}