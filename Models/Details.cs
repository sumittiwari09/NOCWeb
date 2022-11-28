using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class UserDetails
    {
        public string name { get; set; }
        public string firstName { get; set; }
        public string EmailId { get; set; }
        public int mobileVerified { get; set; }
        public int adhaarVerified { get; set; }
        public int panVerified { get; set; }
        public int emailVerified { get; set; }
        public string panCard { get; set; }
        public string adhaarCard { get; set; }
        public string mobileNumber { get; set; }
        public string janAadharNo { get; set; }
        public string partyId { get; set; }
        public string ParentService { get; set; }
        public string UserType { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string Status { get; set; }
        public string ParentName { get; set; }

    }

    public class ServiceName
    {
        public string serviceName { get; set; }
    }

    public class HarwareName
    {
        public string hardwareName { get; set; }
    }

    public class DocumentList
    {
        public string PartyId { get; set; }
        public string UploadDocumentUrl { get; set; }
        public string Name { get; set; }
    }
    public class PaymentDetails
    {
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string PaymentStatus { get; set; }
        public string PartyId { get; set; }
    }

    public class Data
    {
        public UserDetails userDetails { get; set; }
        public List<ServiceName> serviceName { get; set; }
        public List<HarwareName> harwareName { get; set; }
        public List<DocumentList> Documents { get; set; }
        public List<PaymentDetails> PayDetails { get; set; }
    }

    public class Details
    {
        public Data data { get; set; }
    }


    public class RegistredUser
    {
        public string userType { get; set; }
        public string RegistrationNo { get; set; }
        public string PartyId { get; set; }
        public string userName { get; set; }
        public string FirstName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailId { get; set; }
        public string ServiceList { get; set; }
        public string HardwareList { get; set; }
        public string PaymentStatus { get; set; }
        public int IsActive { get; set; }
    }

}