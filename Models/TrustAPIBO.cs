using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class TrustAPIBO
    {
        public string RegistrationNo { get; set; }
        public string SocietyName { get; set; }
        public object BRN { get; set; }
        public string RegistrationDate { get; set; }
        public string SSOID { get; set; }
        public string TotalNumberOfMembers { get; set; }
        public string District { get; set; }
        public string Act { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public List<Address> Address { get; set; }
        public List<AdministrativeDatum> AdministrativeData { get; set; }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Address
    {
        public string PinCode { get; set; }
        public string City { get; set; }
        public string Block { get; set; }
        public string GramPanchayat { get; set; }
        public string Village { get; set; }
        public string District { get; set; }
    }

    public class AdministrativeDatum
    {
        public string Name { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string PostName { get; set; }
        public string Occupation { get; set; }
    }    

    public class TrustRoot
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public TrustAPIBO Data { get; set; }
    }


}