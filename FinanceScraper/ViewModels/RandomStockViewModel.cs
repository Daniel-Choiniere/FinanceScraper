using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinanceScraper.Models;

namespace FinanceScraper.ViewModels
{
    public class RandomStockViewModel
    {
        public Stock Stock { get; set; }
        public List<User> Users { get; set; }
    }
}