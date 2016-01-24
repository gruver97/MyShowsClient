using System.Collections.Generic;
using Newtonsoft.Json;

namespace MysShowsClient.Model
{
    public class ShortDescription
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("ruTitle")]
        public string RuTitle { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("started")]
        public string Started { get; set; }

        [JsonProperty("ended")]
        public string Ended { get; set; }

        [JsonProperty("year")]
        public int? Year { get; set; }

        [JsonProperty("kinopoiskId")]
        public int? KinopoiskId { get; set; }

        [JsonProperty("tvrageId")]
        public int? TvrageId { get; set; }

        [JsonProperty("imdbId")]
        public int? ImdbId { get; set; }

        [JsonProperty("watching")]
        public int? Watching { get; set; }

        [JsonProperty("voted")]
        public int? Voted { get; set; }

        [JsonProperty("rating")]
        public double? Rating { get; set; }

        [JsonProperty("runtime")]
        public int? Runtime { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("genres")]
        public List<int> Genres { get; set; }

        [JsonProperty("images")]
        public List<string> Images { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }
    }
}