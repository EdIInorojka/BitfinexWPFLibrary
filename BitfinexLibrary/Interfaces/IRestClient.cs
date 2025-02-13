using BitfinexLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexLibrary.Interfaces
{
    public interface IRestClient
    {
        Task<IEnumerable<Trade>> GetNewTradesAsync(string pair, int maxCount);
        //PeriodInSec заменен на PeriodInMin т.к. свечи строятся в течении целого числа минут, а также предыдущего значения нет в Aviable values
        Task<IEnumerable<Candle>> GetCandleSeriesAsync(string pair, int periodInMin, DateTimeOffset? from, DateTimeOffset? to = null, long? count = 0);
    }
}
