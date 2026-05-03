using System.Text;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace GameSelector.Web
{
    internal class WebEvent
    {

        [JsonPropertyName("id")]
        public long Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("has_games")]
        public bool HasGames { get; set; }
    }
}
