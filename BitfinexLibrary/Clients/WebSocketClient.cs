using BitfinexLibrary.Interfaces;
using BitfinexLibrary.Models;

namespace BitfinexLibrary.Clients
{
    using System;
    using System.Collections.Generic;
    using System.Net.WebSockets;
    using System.Text;
    using System.Text.Json;
    using System.Threading;
    using System.Threading.Tasks;

    public class WebSocketClient : IWebSocketClient
    {
        private readonly ClientWebSocket webSocket = new();
        private readonly Dictionary<string, int> channelMap = new();
        private readonly CancellationTokenSource cts = new();
        private readonly Uri uri = new("wss://api-pub.bitfinex.com/ws/2");

        public event Action<Trade> NewBuyTrade;
        public event Action<Trade> NewSellTrade;
        public event Action<Candle> CandleSeriesProcessing;

        public async Task ConnectAsync()
        {
            await webSocket.ConnectAsync(uri, cts.Token);
            _ = Task.Run(ReceiveMessagesAsync);
        }

        public async void SubscribeCandles(string pair, int periodInMin, DateTimeOffset? from = null, DateTimeOffset? to = null, long? count = 0)
        {
            string key = $"trade:1m:t{pair.ToUpper()}";
            var message = new { @event = "subscribe", channel = "candles", key };
            await SendAsync(JsonSerializer.Serialize(message));
        }

        public async void SubscribeTrades(string pair, int maxCount = 100)
        {
            var message = new { @event = "subscribe", channel = "trades", symbol = $"t{pair.ToUpper()}" };
            await SendAsync(JsonSerializer.Serialize(message));
        }

        public async void UnsubscribeCandles(string pair)
        {
            if (channelMap.TryGetValue($"candles:t{pair.ToUpper()}", out int chanId))
            {
                var message = new { @event = "unsubscribe", chanId };
                await SendAsync(JsonSerializer.Serialize(message));
            }
        }

        public async void UnsubscribeTrades(string pair)
        {
            if (channelMap.TryGetValue($"trades:t{pair.ToUpper()}", out int chanId))
            {
                var message = new { @event = "unsubscribe", chanId };
                await SendAsync(JsonSerializer.Serialize(message));
            }
        }

        private async Task SendAsync(string message)
        {
            if (webSocket.State == WebSocketState.Open)
            {
                var bytes = Encoding.UTF8.GetBytes(message);
                await webSocket.SendAsync(bytes, WebSocketMessageType.Text, true, cts.Token);
            }
        }

        private async Task ReceiveMessagesAsync()
        {
            var buffer = new byte[8192];

            while (!cts.Token.IsCancellationRequested && webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(buffer, cts.Token);
                if (result.MessageType == WebSocketMessageType.Close) break;

                string json = Encoding.UTF8.GetString(buffer, 0, result.Count);
                ProcessMessage(json);
            }
        }

        private void ProcessMessage(string json)
        {
            using var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;

            if (root.ValueKind == JsonValueKind.Object && root.TryGetProperty("event", out var evt))
            {
                if (evt.GetString() == "subscribed")
                {
                    int chanId = root.GetProperty("chanId").GetInt32();
                    string channel = root.GetProperty("channel").GetString();
                    string key = channel == "candles" ? root.GetProperty("key").GetString() : root.GetProperty("symbol").GetString();
                    channelMap[key] = chanId;
                }
                return;
            }

            if (root.ValueKind == JsonValueKind.Array && root.GetArrayLength() > 1)
            {
                int channelId = root[0].GetInt32();
                if (!channelMap.ContainsValue(channelId)) return;

                if (root[1].ValueKind == JsonValueKind.Array)
                {
                    var data = root[1];
                    if (data.GetArrayLength() == 6)
                    {
                        var candle = new Candle
                        {
                            OpenTime = DateTimeOffset.FromUnixTimeMilliseconds(data[0].GetInt64()),
                            OpenPrice = data[1].GetDecimal(),
                            ClosePrice = data[2].GetDecimal(),
                            HighPrice = data[3].GetDecimal(),
                            LowPrice = data[4].GetDecimal(),
                            TotalVolume = data[5].GetDecimal()
                        };
                        CandleSeriesProcessing?.Invoke(candle);
                    }
                    else if (data.GetArrayLength() == 4)
                    {
                        var trade = new Trade
                        {
                            Id = data[0].GetInt64().ToString(),
                            Time = DateTimeOffset.FromUnixTimeMilliseconds(data[1].GetInt64()),
                            Amount = data[2].GetDecimal(),
                            Price = data[3].GetDecimal()
                        };
                        if (trade.Amount > 0) NewBuyTrade?.Invoke(trade);
                        else NewSellTrade?.Invoke(trade);
                    }
                }
            }
        }
    }

}
