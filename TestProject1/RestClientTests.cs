using BitfinexLibrary.Clients;

namespace UnitTests
{   
    public class RestClientTests
    {
        [Fact]
        public async Task RestClient_CanFetchCandles_BTCUSDT()
        {
            var client = new RestClient();
            var candles = await client.GetCandleSeriesAsync("BTCUSD", 1, DateTimeOffset.UtcNow.AddMinutes(-10), DateTimeOffset.UtcNow);

            Assert.NotNull(candles);
        }

        [Fact]
        public async Task RestClient_CanFetchCandles_XRPUSDT()
        {
            var client = new RestClient();
            var candles = await client.GetCandleSeriesAsync("XRPUSD", 1, DateTimeOffset.UtcNow.AddMinutes(-10), DateTimeOffset.UtcNow);

            Assert.NotNull(candles);
        }

        [Fact]
        public async Task RestClient_CanFetchCandles_XRMUSDT()
        {
            var client = new RestClient();
            var candles = await client.GetCandleSeriesAsync("XRMUSD", 1, DateTimeOffset.UtcNow.AddMinutes(-10), DateTimeOffset.UtcNow);

            Assert.NotNull(candles);
        }

        [Fact]
        public async Task RestClient_CanFetchCandles_DASHUSDT()
        {
            var client = new RestClient();
            var candles = await client.GetCandleSeriesAsync("DASHUSD", 1, DateTimeOffset.UtcNow.AddMinutes(-10), DateTimeOffset.UtcNow);

            Assert.NotNull(candles);
        }

        [Fact]
        public async Task RestClient_CanFetchTrades_BTCUSD()
        {
            var client = new RestClient();
            var trades = await client.GetNewTradesAsync("BTCUSD", 10);

            Assert.NotNull(trades);
        }

        [Fact]
        public async Task RestClient_CanFetchTrades_XRPUSD()
        {
            var client = new RestClient();
            var trades = await client.GetNewTradesAsync("XRPUSD", 10);

            Assert.NotNull(trades);
        }

        [Fact]
        public async Task RestClient_CanFetchTrades_XRMUSD()
        {
            var client = new RestClient();
            var trades = await client.GetNewTradesAsync("XRMUSD", 10);

            Assert.NotNull(trades);
        }

        [Fact]
        public async Task RestClient_CanFetchTrades_DASHUSD()
        {
            var client = new RestClient();
            var trades = await client.GetNewTradesAsync("DASHUSD", 10);

            Assert.NotNull(trades);
        }
    }
}
