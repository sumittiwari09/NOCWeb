using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class CommissionRates
    {
        public decimal CommissionPercentage { get; set; }
        public string PartyId { get; set; }
        public int UserType { get; set; }
        public string CommissionFor { get; set; }
        public int OperatorId { get; set; }
        public string OperatorName { get; set; }
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
    }
}