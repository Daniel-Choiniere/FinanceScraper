using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FinanceScraper.Models;
using FinanceScraper.ViewModels;

namespace FinanceScraper.Controllers
{
    public class UsersController : Controller
    {
        public ViewResult Index()
        {
            var users = GetUsers();

            return View(users);
        }

        public ActionResult Details(int id)
        {
            var user = GetUsers().SingleOrDefault(c => c.Id == id);

            if (user == null)
                throw new NotImplementedException();

            return View(user);
        }


        private IEnumerable<User> GetUsers()
        {
            return new List<User>
            {
                new User { Id = 1, Name = "Mark Appleyard" },
                new User { Id = 2, Name = "Bam Margera" }
            };
        }
    }
}