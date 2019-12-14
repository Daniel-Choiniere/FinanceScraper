using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinanceScraper.Models
{
    public class MemberShipType
    {
        public byte Id { get; set; }
        public short SignUpFee { get; set; }
        public byte DurationInMonths { get; set; }
        public byte DiscountRate { get; set; }
    }
}