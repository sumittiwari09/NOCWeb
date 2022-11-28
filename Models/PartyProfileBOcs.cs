using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class PartyProfileBO 
    {
        public string StringType { get; set; }
        public int PartyId { get; set; }
        public int? CompanyId { get; set; }
        public int? PartyCode { get; set; }

        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }

        public DateTime? DateofBirth { get; set; }

        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? CityId { get; set; }
        public int? RoleId { get; set; }
        public int? Type { get; set; }
        public string CurrentAddressLine1 { get; set; }
        public string CurrentAddressLine2 { get; set; }
        public string CurrentAddressLine3 { get; set; }
        public string CurrentAddressPincode { get; set; }
        public string ParmentAddressLine1 { get; set; }
        public string ParmentAddressLine2 { get; set; }
        public string ParmentAddressLine3 { get; set; }
        public string ParmentAddressPincode { get; set; }

        public string CompanyContactNumber { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyGSTNo { get; set; }
        public string CompanyTanNo { get; set; }
        public string CompanyAddressLine1 { get; set; }
        public string CompanyAddressLine2 { get; set; }
        public string CompanyAddressLine3 { get; set; }
        public string CompanyAddressPincode { get; set; }

        public int? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }

    }
}