using System.Linq;
using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;


namespace FinanceScraper.Models
{
//    public class MyContext : DbContext
//    {
//        public MyContext(DbContextOptions<MyContext> options)
//            : base(options)
//        { }
//
//        public DbSet<Stock> Stocks { get; set; }
//    }

    public class Stock
    {
        public int id { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Symbol { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string LastPrice { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Change { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Currency { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string DataCollectedOn { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string Volume { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string AvgVol3m { get; set; }
        [Column(TypeName = "nvarchar(200)")]
        public string MarketCap { get; set; }
    }
}