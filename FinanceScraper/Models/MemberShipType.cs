using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceScraper.Models
{
    public class MemberShipType
    {
        public int Id { get; set; }
        public short SignUpFree { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
    }
}