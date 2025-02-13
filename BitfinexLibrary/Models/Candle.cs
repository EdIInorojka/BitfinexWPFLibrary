using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexLibrary.Models
{
    public class Candle
    {
        /// <summary>
        /// Валютная пара
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Цена открытия
        /// </summary>
        public decimal OpenPrice { get; set; }

        /// <summary>
        /// Максимальная цена
        /// </summary>
        public decimal HighPrice { get; set; }

        /// <summary>
        /// Минимальная цена
        /// </summary>
        public decimal LowPrice { get; set; }

        /// <summary>
        /// Цена закрытия
        /// </summary>
        public decimal ClosePrice { get; set; }


        /// <summary>
        /// Partial (Общая сумма сделок) удалён т.к. нет возможности получить данное значение
        /// </summary>

        /// <summary>
        /// Partial (Общий объем)
        /// </summary>
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// Время
        /// </summary>
        public DateTimeOffset OpenTime { get; set; }

    }
}
