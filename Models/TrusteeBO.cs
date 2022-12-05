using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class TrusteeBO
    {
        public class TrusteeInfo:ErrorBO
        {
            public string TrusteeInfoId { get; set; }
            public string ApplicantId { get; set; }
            public string Name { get; set; }
            public string TrustType { get; set; }
            public string CertiFiedBy { get; set; }
            public string RegistrationNo { get; set; }
            public DateTime RegistrationDate { get; set; }
            public DateTime ElectionDate { get; set; }
            public string Certified { get; set; }
            public string CeritifiedExtension { get; set; }
            public string CeritifiedbyContenttype { get; set; }
            public string Registration { get; set; }
            public string RegistrationExtension { get; set; }
            public string RegistrationContenttype { get; set; }
            public string TrusteeLogo { get; set; }
            public string TrusteeExtension { get; set; }
            public string TrusteeContentType { get; set; }

        }
        public class Trustee :ErrorBO
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string RoleId { get; set; }
            public string Mobile { get; set; }
            public string Email { get; set; }
            public string Aadhaar { get; set; }
            public string Pan { get; set; }
            public string ProfileImg { get; set; }
            public string AadhaarExtension { get; set; }
            public string AadhaarContentType { get; set; }           
            public string PanExtension { get; set; }
            public string PanContentType { get; set; }            
            public string ProfileExtension { get; set; }
            public string ProfileContentType { get; set; }
            public byte[] DocumentByte { get; set; }
        }
    }
}