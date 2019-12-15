using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FinanceScraper.Models;

namespace FinanceScraper.ViewModels
{
    public class NewUserViewModel
    {
        public IEnumerable<MemberShipType> MembershipTypes { get; set; }
        public User User { get; set; }
    }
}