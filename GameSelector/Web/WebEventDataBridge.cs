using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using Windows.Foundation.Collections;

namespace GameSelector.Web
{
    internal class WebEventDataBridge
    {
        private readonly HttpClient _httpClient;

        private Dictionary<string, long> _eventNameToId;

        private Dictionary<string, long> EventNameToId => _eventNameToId ??= GetEventsFromWeb();

        private Dictionary<string, WebGameDataBridge> _webGameDataBridges;

        public WebEventDataBridge()
        {
            _httpClient = new()
            {
                BaseAddress = new Uri("https://www.regiotwenteland.nl/wp-json/sip/v1/"),
            };

            _webGameDataBridges = [];
        }

        private Dictionary<string, long> GetEventsFromWeb()
        {
            var response = _httpClient.GetAsync("evenementen").Result;

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("ERROR!");
                return null;
            }

            var strResponse = response.Content.ReadAsStringAsync().Result;

            var events = JsonSerializer.Deserialize<WebEvent[]>(strResponse);

            return events
                .Where(we => we.HasGames)
                .ToDictionary(we => we.Title, we => we.Id);
        }


        public IEnumerable<string> GetEvents() =>
            EventNameToId?.Select(e => e.Key) ?? [];

        public WebGameDataBridge GetWebGameDataBridge(string eventName)
        {
            if (!_webGameDataBridges.TryGetValue(eventName, out WebGameDataBridge value))
            {
                if (!_eventNameToId.TryGetValue(eventName, out var id))
                {
                    return null;
                }

                value = new(_httpClient, _eventNameToId[eventName]);
                _webGameDataBridges.Add(eventName, value);
            }

            return value;
        }
    }
}
