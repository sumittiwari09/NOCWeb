using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class SSOUserDetail
    {
        public string FirstName { get; set; }
        public string IpPhone { get; set; }
        public string TelephoneNumber { get; set; }
        public string State { get; set; }
        public string SSOID { get; set; }
        public string PostalCode { get; set; }
        public string PostalAddress { get; set; }
        public string Photo { get; set; }
        public string Mobile { get; set; }
        public string MailPersonal { get; set; }
        public string LastName { get; set; }
        public string MailOfficial { get; set; }
        public string EmployeeNumber { get; set; }
        public string DisplayName { get; set; }
        public string Designation { get; set; }
        public string DepartmentName { get; set; }
        public string DepartmentId { get; set; }
        public string DateOfBirth { get; set; }
        public string City { get; set; }
        public string BhamashahMemberId { get; set; }
        public string BhamashahId { get; set; }
        public string AadhaarId { get; set; }
        public string Gender { get; set; }
        public string[] OldSSOIDs { get; set; }

    }
}