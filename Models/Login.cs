using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{

    public class ResetPassword : Login
    {
        public string Type { get; set; }
    }
    public class Login
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }
    public class MobileVerification
    {
        public string Mobile { get; set; }
        public string Message { get; set; }
    }

    public class UserModelSession
    {
        public string PartyId { get; set; }
        public string Hirarchy { get; set; }
        public string Type { get; set; }
        public string UserType { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string MobileNumber { get; set; }
        public string ServicesCollection { get; set; }
        public string IsActive { get; set; }
        public string PartialOrderID { get; set; }
        public string RegistrationNo { get; set; }
        public string profileImage { get; set; }
        public int RoleId { get; set; }
        public int DepartmentId { get; set; }
        public int StateId { get; set; }
        public int DistrictId { get; set; }
        public int CityId { get; set; }
        public int iLocLvl { get; set; }
    }

}