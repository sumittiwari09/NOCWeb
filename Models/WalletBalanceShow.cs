using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class WalletBalanceShow : ErrorBO
    {
        public decimal CIW { get; set; }
        public decimal COW { get; set; }
        public decimal UW { get; set; }
        public decimal PTW { get; set; }

    }
}