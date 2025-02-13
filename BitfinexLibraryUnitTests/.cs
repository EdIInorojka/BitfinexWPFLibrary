using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace BitfinexWPFLibrary.UnitTests
{
    [TestClass]
    public class RestClientTests
    {
        [TestMethod]
        public async Task RestClient_CanFetchCandles()
        {
            var client = new RestClient();
            var candles = await client.GetCandleSeriesAsync("BTCUSD", 1, DateTimeOffset.UtcNow.AddMinutes(-10), DateTimeOffset.UtcNow);

            Assert.IsNotNull(candles);
        }

        [TestMethod]
        public async Task RestClient_CanFetchTrades()
        {
            var client = new RestClient();
            var trades = await client.GetNewTradesAsync("BTCUSD", 10);

            Assert.IsNotNull(trades);
        }
    }
}
