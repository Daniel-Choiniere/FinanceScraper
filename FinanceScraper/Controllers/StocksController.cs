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
        private ApplicationDbContext _context;

        public StocksController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var stocks = _context.Stocks.ToList();

            return View(stocks);
        }

        public ActionResult Details(int id)
        {
            var stock = _context.Stocks.SingleOrDefault(s => s.id == id);

            if (stock == null)
                return HttpNotFound();

            return View(stock);
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