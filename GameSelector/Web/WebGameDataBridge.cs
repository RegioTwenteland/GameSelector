using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using GameSelector.Model;

namespace GameSelector.Web
{
    // https://www.regiotwenteland.nl/wp-json/sip/v1/evenementen
    // https://www.regiotwenteland.nl/wp-json/sip/v1/activiteiten?event_id=2955
    internal class WebGameDataBridge
    {
        private readonly HttpClient _httpClient;
        private readonly long _eventId;

        public WebGameDataBridge(HttpClient httpClient, long eventId)
        {
            _httpClient = httpClient;
            _eventId = eventId;
        }

        public IEnumerable<Game> GetGames()
        {
            var response = _httpClient.GetAsync($"activiteiten?event_id={_eventId}").Result;

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("ERROR!");
                return [];
            }

            var strResponse = response.Content.ReadAsStringAsync().Result;

            return
                JsonSerializer.Deserialize<WebGame[]>(strResponse)
                .Select(wg => new Game
                {
                    Code = wg.Code,
                    Description = wg.Description,
                    Category = string.Empty,
                    Active = wg.IsFeasible,
                    Priority = 0,
                    Remarks = string.Empty,
                    MultiplePlayersRequired = false,
                    MaxPlayerAmount = wg.TimesChosen,
                });
        }
    }
}
