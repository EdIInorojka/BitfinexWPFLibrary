using BitfinexLibrary.Clients;
using Xunit;

namespace BitfinexLibrary.Tests
{   
    public class WebSocketTests
    {
        [Fact]
        public async Task WebSocketClient_CanConnectAndReceiveCandles_BTCUSD()
        {
            var client = new WebSocketClient();
            bool eventTriggered = false;

            client.CandleSeriesProcessing += (candle) => eventTriggered = true;
            await client.ConnectAsync();
            client.SubscribeCandles("BTCUSD", 1);

            await Task.Delay(5000);
            Assert.True(eventTriggered);
        }

        [Fact]
        public async Task WebSocketClient_CanConnectAndReceiveCandles_XRPUSDT()
        {
            var client = new WebSocketClient();
            bool eventTriggered = false;

            client.CandleSeriesProcessing += (candle) => eventTriggered = true;
            await client.ConnectAsync();
            client.SubscribeCandles("XRPUSDT", 1);

            await Task.Delay(5000);
            Assert.True(eventTriggered);
        }

        [Fact]
        public async Task WebSocketClient_CanConnectAndReceiveCandles_XMRUSDT()
        {
            var client = new WebSocketClient();
            bool eventTriggered = false;

            client.CandleSeriesProcessing += (candle) => eventTriggered = true;
            await client.ConnectAsync();
            client.SubscribeCandles("XMRUSDT", 1);

            await Task.Delay(5000);
            Assert.True(eventTriggered);
        }

        [Fact]
        public async Task WebSocketClient_CanConnectAndReceiveCandles_DASHUSDT()
        {
            var client = new WebSocketClient();
            bool eventTriggered = false;

            client.CandleSeriesProcessing += (candle) => eventTriggered = true;
            await client.ConnectAsync();
            client.SubscribeCandles("DASHUSDT", 1);

            await Task.Delay(5000);
            Assert.True(eventTriggered);
        }

        [Fact]
        public async Task WebSocketClient_CanReceiveTrades_BTCUSD()
        {
            var client = new WebSocketClient();
            bool eventTriggered = false;

            client.NewBuyTrade += (trade) => eventTriggered = true;
            await client.ConnectAsync();
            client.SubscribeTrades("BTCUSD");

            await Task.Delay(5000);
            Assert.True(eventTriggered);
        }

        [Fact]
        public async Task WebSocketClient_CanReceiveTrades_XRPUSDT()
        {
            var client = new WebSocketClient();
            bool eventTriggered = false;

            client.NewBuyTrade += (trade) => eventTriggered = true;
            await client.ConnectAsync();
            client.SubscribeTrades("XRPUSDT");

            await Task.Delay(5000);
            Assert.True(eventTriggered);
        }

        [Fact]
        public async Task WebSocketClient_CanReceiveTrades_XMRUSDT()
        {
            var client = new WebSocketClient();
            bool eventTriggered = false;

            client.NewBuyTrade += (trade) => eventTriggered = true;
            await client.ConnectAsync();
            client.SubscribeTrades("XMRUSDT");

            await Task.Delay(5000);
            Assert.True(eventTriggered);
        }

        [Fact]
        public async Task WebSocketClient_CanReceiveTrades_DASHUSDT()
        {
            var client = new WebSocketClient();
            bool eventTriggered = false;

            client.NewBuyTrade += (trade) => eventTriggered = true;
            await client.ConnectAsync();
            client.SubscribeTrades("DASHUSDT");

            await Task.Delay(5000);
            Assert.True(eventTriggered);
        }

    }
}
