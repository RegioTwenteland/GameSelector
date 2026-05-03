using System.Text;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace GameSelector.Web
{
    internal class WebGame
    {

        [JsonPropertyName("spelcode")]
        public string Code { get; set; }

        [JsonPropertyName("naam")]
        public string Description { get; set; }

        [JsonPropertyName("is_mogelijk")]
        public bool IsFeasible { get; set; }

        [JsonPropertyName("gekozen")]
        public long TimesChosen { get; set; }
    }
}
