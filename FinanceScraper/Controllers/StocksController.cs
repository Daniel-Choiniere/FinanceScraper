using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FinanceScraper.Models;
using FinanceScraper.ViewModels;

namespace FinanceScraper.Controllers
{
    // GET: stocks/   abstract list of all stocks
    public class StocksController : Controller
    {
        public ViewResult Index()
        {
            var stocks = GetStocks();

            return View(stocks);
        }

        private IEnumerable<Stock> GetStocks()
        {
            return new List<Stock>
            {
                new Stock { id = 1, Symbol = "BTC" },
                new Stock { id = 2, Symbol = "ETH" }
            };
        }


        // GET: stocks/random
        public ActionResult Random()
        {
            var stock = new Stock() { Symbol = "BTC" };
            var users = new List<User>
            {
                new User { Name = "Dan" },
                new User { Name = "Bean" },
                new User { Name = "Sean" },
                new User { Name = "Al" },
                new User { Name = "Christian" },
                new User { Name = "Joe" }
            };

            var viewModel = new RandomStockViewModel
            {
                Stock = stock,
                Users = users
            };

            return View(viewModel);
        }
    }
}