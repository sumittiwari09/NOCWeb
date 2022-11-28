using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class CompanyConsumption
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string PartyId { get; set; }
        public string ServiceProvider { get; set; }
        public string ServiceType { get; set; }
        public decimal AvgDailyConsumption { get; set; }
        public decimal LeadTimeInDay { get; set; }
        public decimal Safetyfactor { get; set; }
        public decimal maxConsumption { get; set; }
        public string CreatedBy { get; set; }
        public decimal CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public decimal ModifiedOn { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
    }
    public class CompanyConsumptionList : ErrorBO
    {
        public List<CompanyConsumption> CompanyConsumptionLst { get; set; }
    }
}