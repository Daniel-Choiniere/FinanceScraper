using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using FinanceScraper.Models;
using FinanceScraper.ViewModels;
using System.Data.Entity;

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

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return RedirectToAction("Index", "Users");
        }


        public ViewResult Index()
        {
            var users = _context.Users.Include(u => u.MerMemberShipType).ToList();

            return View(users);
        }

        public ActionResult Details(int id)
        {
            var user = _context.Users.Include(u => u.MerMemberShipType).SingleOrDefault(u => u.Id == id);

            if (user == null)
                throw new NotImplementedException();

            return View(user);
        }
    }
}