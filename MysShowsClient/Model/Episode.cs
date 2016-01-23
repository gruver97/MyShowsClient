using Newtonsoft.Json;

namespace MysShowsClient.Model
{
    public class Episode
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("airDate")]
        public string AirDate { get; set; }

        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("tvrageLink")]
        public string TvrageLink { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("seasonNumber")]
        public int SeasonNumber { get; set; }

        [JsonProperty("episodeNumber")]
        public int EpisodeNumber { get; set; }

        [JsonProperty("productionNumber")]
        public string ProductionNumber { get; set; }

        [JsonProperty("sequenceNumber")]
        public int SequenceNumber { get; set; }
    }
}