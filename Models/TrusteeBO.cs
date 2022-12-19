using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class TrusteeBO
    {
        public class TrusteeInfo : ErrorBO
        {
            public string TrusteeInfoId { get; set; }
            public string ApplicantId { get; set; }
            public string Name { get; set; }
            public string TrustType { get; set; }
            public string CertiFiedBy { get; set; }
            public string RegistrationNo { get; set; }
            public string RegistrationDate { get; set; }
            public string ElectionDate { get; set; }
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

        public class Trustee : ErrorBO
        {
            public string Id { get; set; }
            public string TrustInfoId { get; set; }
            public string Name { get; set; }

            public string RoleId { get; set; }

            public string Mobile { get; set; }
            public string Email { get; set; }
            public string AadhaarNo { get; set; }
            public string PanNo { get; set; }
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
            public string Profilefile { get; set; }
        }

        public class CollageList
        {
            public string CollageId { get; set; }
            public string TrustInfoId { get; set; }
            public string TrustName { get; set; }
            public string TrustRegistrationNo { get; set; }
            public string CollageName { get; set; }
            public string MobileNo { get; set; }
            public string Email { get; set; }
            public string CollageLogo { get; set; }
            public string CollageContectType { get; set; }
            public string AddMobileNo { get; set; }
            public string AddPhoneNo { get; set; }
            public string WebsiteURL { get; set; }
            public string Degree { get; set; }
            public int DegreeID { get; set; }
            public string Courses { get; set; }
            public int CoursesID { get; set; }
        }

        public class CollageFacility
        {
            public int? TrustId { get; set; }
            public int? CollageId { get; set; }
            public List<Facilty> list { get; set; }
        }

        public class Facilty
        {
            public string FacilityId { get; set; }
            public bool IsSelect { get; set; }
            public string uploadFile { get; set; }
        }

        public class CollageFeeMst
        {
            public string TrustId { get; set; }
            public string CollageId { get; set; }
            public string DepartmentId { get; set; }
            public string CourseId { get; set; }
            public string FinancialYear { get; set; }

            public List<RateList> rateLists { get; set; }
            //public string Id { get; set; }
            //public decimal Rate { get; set; }
            //public string RateName { get; set; }
            //public int IsActive { get; set; }
        }

        public class RateList
        {
            public string RateId { get; set; }
            public string RateName { get; set; }
            public bool IsSelect { get; set; }
            public decimal Rate { get; set; }
        }


        #region Vivek Add Application Modal

        public class SaveApplicationModal
        {
            public int iPK_ID { get; set; }
            public string sApplNo { get; set; }
            public string iFK_Finyr { get; set; }
            public int iFKTst_ID { get; set; }
            public int iFKCLG_ID { get; set; }
            public int iFKDEPT_ID { get; set; }
            public int iFK_CORS_ID { get; set; }
            public int sSSO_ID { get; set; }
            public DateTime? dtCRT_On { get; set; }
            public DateTime? dtSubOn { get; set; }
            public string iSts { get; set; }

        }

        #endregion

    }
}