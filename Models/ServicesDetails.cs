using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class ServicesDetails
    {

        public string RateMasterId { get; set; }
        public string ServiceName { get; set; }
        public string CategoryID { get; set; }
        public string Name { get; set; }
        public string className { get; set; }
        public string ChargeType { get; set; }
        public string UnitType { get; set; }
        public string PaymentType { get; set; }
        public int Amount { get; set; }
        public int Isactive { get; set; }

    }


    public class Services
    {
        public string CategoryId { get; set; }
        public string Name { get; set; }
        public string ClassName { get; set; }
        public string varificationList { get; set; }
        public string DocumentList { get; set; }
        public string HardwareList { get; set; }

    }
    public class HardwareList
    {
        public string HardwareId { get; set; }
        public string Name { get; set; }
        public string GST { get; set; }
        public string Amount { get; set; }
        public string ChargeType { get; set; }
        public string UnitType { get; set; }
    }

    public class GetservicesetailsAndroidNew
    {
        public string CategoryId { get; set; }
        public string TypeName { get; set; }
        public decimal GstAmount { get; set; }
        public decimal IGST { get; set; }
        public decimal CGST { get; set; }
        public decimal Commision { get; set; }
        public decimal ServiceCharges { get; set; }
        public decimal CourierCharges { get; set; }
        public decimal TotalAmount { get; set; }
        public string CategoryName { get; set; }
        public DateTime Createddate { get; set; }
        public string services { get; set; }
        public string ServiceIcon { get; set; }
        public int IsActive { get; set; }
    }

    public class ServiceIDs
    {
        public string serviceList { get; set; }
    }

}