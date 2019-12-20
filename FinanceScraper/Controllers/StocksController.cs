using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web.Mvc;
using FinanceScraper.Models;
using FinanceScraper.ViewModels;
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

            return View(stocks);
        }

        public ActionResult Details(int id)
        {
            var stock = _context.Stocks.SingleOrDefault(s => s.id == id);

            if (stock == null)
                return HttpNotFound();

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
            LogTable(driver);

            return View(stock);
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
            LogTable(driver);

            var stocks = _context.Stocks.ToList();

            return View(stocks);
        }

        //        Save The scraped stock data to the database
        public static void StartWebDriver()
        {
            
        }

        public static void InsertDataToDb(IWebDriver driver)
        {
            var elemTable = driver.FindElement(By.XPath("//*[@id='pf-detail-table']/div[1]/table"));

            // Get all Rows of the table
            List<IWebElement> tableRowList = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));
            tableRowList.RemoveAt(0);

            string connection =
                @"Server=tcp:finance-scraper.database.windows.net,1433;Initial Catalog=YahooFinanceScraper;Persist Security Info=False;User ID=Dan;Password=iLOVEcareerdevs1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30";

            using (SqlConnection dbConnection = new SqlConnection(connection))
            {
                dbConnection.Open();

                for (var i = 0; i < tableRowList.Count; i++)
                {
                    string[] splitRows = tableRowList[i].Text.Split(' ');

                    string[] splitUpPriceFromSymbol = splitRows[0].Split('\n');

                    string[] splitUpMarketcapFromTrade = splitRows[9].Split('\n');

                    SqlCommand insertCommand = new SqlCommand(
                        "INSERT into dbo.FinanceData (Symbol, LastPrice, Change, Currency, DataCollectedOn, Volume, AvgVol3m, MarketCap) VALUES (@symbol, @lastprice, @change, @currency, @datacollectiontime, @volume, @avgvol, @marketcap)",
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

        private static void LogTable(IWebDriver driver)
        {
            var elemTable = driver.FindElement(By.XPath("//*[@id='pf-detail-table']/div[1]/table"));

            List<IWebElement> tableRowList = new List<IWebElement>(elemTable.FindElements(By.TagName("tr")));
            String strRowData = "";

            foreach (var tableRowElement in tableRowList)
            {
                List<IWebElement> tableColList = new List<IWebElement>(tableRowElement.FindElements(By.TagName("td")));
                if (tableColList.Count > 0)
                {
                    foreach (var tableColElement in tableColList)
                    {
                        strRowData = strRowData + tableColElement.Text + "\t\t";
                    }
                }
                else
                {
                    Console.WriteLine("YAHOO FINANCE STOCK WATCHLIST");
                    Console.WriteLine(tableRowList[0].Text.Replace(" ", "      "));
                }

                Console.WriteLine(strRowData);
                strRowData = String.Empty;
            }

            Console.WriteLine("");

            driver.Close();
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