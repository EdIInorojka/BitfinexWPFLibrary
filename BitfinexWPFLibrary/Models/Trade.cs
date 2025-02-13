﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BitfinexWPFLibrary.Models
{
    public class Trade
    {
        /// <summary>
        /// Валютная пара
        /// </summary>
        public string Pair { get; set; }

        /// <summary>
        /// Цена трейда
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Объем трейда
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Направление (buy/sell) удалено т.к. невозможно получить значение
        /// </summary>

        /// <summary>
        /// Время трейда
        /// </summary>
        public DateTimeOffset Time { get; set; }


        /// <summary>
        /// Id трейда
        /// </summary>
        public string Id { get; set; }

    }
}
