using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FinanceScraper.Models;
using FinanceScraper.ViewModels;
using System.Data.Entity;
using System.Web.Configuration;

namespace FinanceScraper.Controllers
{
    public class UsersController : Controller
    {
        private ApplicationDbContext _context;

        public UsersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ActionResult New()
        {
            var membershiptypes = _context.MemberShipTypes.ToList();
            var viewModel = new NewUserViewModel
            {
                MembershipTypes = membershiptypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(User user)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new NewUserViewModel()
                {
                    User = user,
                    MembershipTypes = _context.MemberShipTypes.ToList()
                };

                return View("CustomerForm", viewModel);
            }

            if (user.Id == 0)
              _context.Users.Add(user);
            else
            {
                var userInDb = _context.Users.Single(u => u.Id == user.Id);
                userInDb.Name = user.Name;
                userInDb.Birthdate = user.Birthdate;
                userInDb.MemberShipType = user.MemberShipType;
                userInDb.IsSubscribed = user.IsSubscribed;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }


        public ViewResult Index()
        {
            var users = _context.Users.Include(u => u.MemberShipType).ToList();

            return View(users);
        }

        public ActionResult Details(int id)
        {
            var user = _context.Users.Include(u => u.MemberShipType).SingleOrDefault(u => u.Id == id);

            if (user == null)
                throw new NotImplementedException();

            return View(user);
        }

        public ActionResult Edit(int id)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == id);

            if (user == null)
                return HttpNotFound();

            var viewModel = new NewUserViewModel()
            {
                User = user,
                MembershipTypes = _context.MemberShipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }
    }
}