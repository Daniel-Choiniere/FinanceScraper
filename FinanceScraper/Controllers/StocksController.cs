using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using FinanceScraper.Models;
using System;
using System.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


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

            var firstFifty = stocks.OrderByDescending(s => s.id).Take(20).Reverse();

            return View(firstFifty);
        }

        public ActionResult Details(int id)
        {
            var stock = _context.Stocks.SingleOrDefault(s => s.id == id);

            if (stock == null)
                return HttpNotFound();

            return View(stock);
        }

        public ViewResult SearchForStock()
        {
            var stocks = _context.Stocks.ToList();

            var firstFifty = stocks.OrderByDescending(s => s.id).Take(20).Reverse();

            return View(firstFifty);
        }

    public ActionResult Scrape()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("--window-size=1400, 600");
            options.AddArgument("--headless");
            options.AddArgument("--disable-gpu");
            options.AddArgument("--no-sandbox");
            options.AddArgument("--log-path=chromedriver.log");
            options.AddArgument("--verbose");

            string DRIVER_PATH = "C:/Users/Dan/Desktop";

            IWebDriver driver = new ChromeDriver(DRIVER_PATH, options);

            driver.Navigate().GoToUrl("https://finance.yahoo.com/");

            driver.Manage().Window.Maximize();

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            driver.FindElement(By.Id("uh-signedin")).Click();

            driver.FindElement(By.Id("login-username")).SendKeys("daniel.choiniere@yahoo.com");
            driver.FindElement(By.Id("login-signin")).Click();

            driver.FindElement(By.Id("login-passwd")).SendKeys("iLOVEcareerdevs");
            driver.FindElement(By.Id("login-signin")).Click();

            Console.WriteLine("We are logged in!");

            driver.FindElement(By.XPath("//*[@id='Nav-0-DesktopNav']/div/div[3]/div/nav/ul/li[2]/a")).Click();

            driver.FindElement(By.XPath("//*[@id='Col1-0-Portfolios-Proxy']/main/table/tbody/tr[1]/td[1]/div[2]/a"))
                .Click();

            InsertDataToDb(driver);

            var stockList = _context.Stocks.ToList();

            var lastTen = stockList.OrderByDescending(s => s.id).Take(10).Reverse();

            return View(lastTen);
        }


        public static void InsertDataToDb(IWebDriver driver)
        {
            var elemTable = driver.FindElement(By.XPath("//*[@id='pf-detail-table']/div[1]/table"));

            // Get all Rows of the table
            List<IWebElement> tableRowList = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));
            tableRowList.RemoveAt(0);

            string connection =
                @"Data Source=(LocalDb)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\aspnet-FinanceScraper-20191213062914.mdf;Initial Catalog=aspnet-FinanceScraper-20191213062914;Integrated Security=True";

            using (SqlConnection dbConnection = new SqlConnection(connection))
            {
                dbConnection.Open();

                for (var i = 0; i < tableRowList.Count; i++)
                {
                    string[] splitRows = tableRowList[i].Text.Split(' ');

                    string[] splitUpPriceFromSymbol = splitRows[0].Split('\n');

                    string[] splitUpMarketcapFromTrade = splitRows[9].Split('\n');

                    SqlCommand insertCommand = new SqlCommand(
                        "INSERT into dbo.Stocks (Symbol, LastPrice, Change, Currency, DataCollectedOn, Volume, AvgVol3m, MarketCap) VALUES (@symbol, @lastprice, @change, @currency, @datacollectiontime, @volume, @avgvol, @marketcap)",
                        dbConnection);

                    insertCommand.Parameters.AddWithValue("@symbol", splitUpPriceFromSymbol[0]);
                    insertCommand.Parameters.AddWithValue("@lastprice", splitUpPriceFromSymbol[1]);
                    insertCommand.Parameters.AddWithValue("@change", splitRows[2]);
                    insertCommand.Parameters.AddWithValue("@currency", splitRows[3]);
                    insertCommand.Parameters.AddWithValue("@datacollectiontime", DateTime.Now);
                    insertCommand.Parameters.AddWithValue("@volume", splitRows[6]);
                    insertCommand.Parameters.AddWithValue("@avgvol", splitRows[8]);
                    insertCommand.Parameters.AddWithValue("@marketcap", splitUpMarketcapFromTrade[0]);

                    insertCommand.ExecuteNonQuery();

                }

                Console.WriteLine("Data Collection Successful");
                dbConnection.Close();
            }
        }
    }
}