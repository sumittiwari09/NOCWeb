using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class GSTApplicable
    {
        public long Id { get; set; }
        public int TypeId { get; set; }
        public long ServiceHardwareId { get; set; }
        public decimal GST { get; set; }
        public string ApplicableChargesType { get; set; }
        //public decimal TotalAmount { get; set; }
        //public decimal Tax { get; set; }
        //public decimal GrandTotal { get; set; }
        public decimal ApplicableTotal { get; set; }
        public int IsActive { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string TypeName { get; set; }
        public string ServiceName { get; set; }
        //public string ChargesType { get; set; }
        public int TaxType { get; set; }
        public string TaxTypeName { get; set; }
        public int UnitId { get; set; }
        public string UnitName { get; set; }
    }
    public class ListGSTApplicable : ResponseData
    {
        public List<GSTApplicable> ListRequest { get; set; }
    }
}