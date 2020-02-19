using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinanceScraper.Models
{
    public class Stock
    {
        public int id { get; set; }
        public string Symbol { get; set; }
        public string LastPrice { get; set; }
        public string Change { get; set; }
        public string Currency { get; set; }
        public string DataCollectedOn { get; set; }
        public string Volume { get; set; }
        public string AvgVol3m { get; set; }
        public string MarketCap { get; set; }
        public string FullName { get; set; }
        public string DatePublic { get; set; }
    }
}