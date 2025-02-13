using BitfinexWPFLibrary.Interfaces;
using BitfinexWPFLibrary.Models;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Text.Json;

namespace BitfinexWPFLibrary.Clients
{
    public class RestClient : IRestClient
    {
        private HttpClient httpClient = new ();

        public RestClient()
        {
            httpClient.BaseAddress = new Uri("https://api-pub.bitfinex.com/v2/");
        }
        public async Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInMin, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0)
        {
            string request = $"candles/trade%3A{periodInMin}m%3At{pair}/hist";

            HttpResponseMessage response = await httpClient.GetAsync(request);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            var candles = await JsonSerializer.DeserializeAsync<List<List<object>>>(stream);
            var result = new List<Candle>();

            foreach (var candle in candles)
            {
                result.Add(new Candle
                {
                    Pair = pair,
                    OpenTime = DateTimeOffset.FromUnixTimeMilliseconds((long)candle[0]),
                    OpenPrice = Convert.ToDecimal(candle[1]),
                    ClosePrice = Convert.ToDecimal(candle[2]),
                    HighPrice = Convert.ToDecimal(candle[3]),
                    LowPrice = Convert.ToDecimal(candle[4]),
                    TotalVolume = Convert.ToDecimal(candle[5])
                });
            }
            return result;
        }

        public async Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount)
        {
            string request = $"trades/t{pair}/hist?limit={maxCount}&sort=-1";

            HttpResponseMessage response = await httpClient.GetAsync(request);
            response.EnsureSuccessStatusCode();

            using var stream = await response.Content.ReadAsStreamAsync();
            var trades = await JsonSerializer.DeserializeAsync<List<List<object>>>(stream);

            var result = new List<Trade>();
            foreach (var trade in trades)
            {
                result.Add(new Trade
                {
                    Id = trade[0].ToString(),
                    Pair = pair,
                    Time = DateTimeOffset.FromUnixTimeMilliseconds((long)trade[1]),
                    Amount = Convert.ToDecimal(trade[2]),
                    Price = Convert.ToDecimal(trade[3]),
                });
            }
            return result;
        }
    }
}
