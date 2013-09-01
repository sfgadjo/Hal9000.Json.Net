using System;
using Hal9000.Json.Net;

namespace HalWebApiExample.Models {

    /// <summary>
    /// An archive of a customer order.
    /// </summary>
    public class OrderLog : IHalResource {

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public uint NumberOfItemsPurchase { get; set; }
    }
}