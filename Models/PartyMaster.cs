using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{

    public class UserAddress
    {
        public string CUAdd1 { get; set; }
        public string CUAdd2 { get; set; }
        public string CUAdd3 { get; set; }
        public string CULandMark { get; set; }
        public string CUPinCode { get; set; }
        public string PAAdd1 { get; set; }
        public string PAAdd2 { get; set; }
        public string PAAdd3 { get; set; }
        public string PALandMark { get; set; }
        public string PAPinCode { get; set; }
        public string COAdd1 { get; set; }
        public string COAdd2 { get; set; }
        public string COAdd3 { get; set; }
        public string COLandMark { get; set; }
        public string COPinCode { get; set; }
        public string Aadhaarhouse { get; set; }
        public string Aadhaarlandmark { get; set; }
        public string AadhaarDist { get; set; }
        public string AadhaarLoc { get; set; }
        public string AadhaarPO { get; set; }
        public string AadhaarStreet { get; set; }
        public string PanStatus { get; set; }
        public string AdhaarCardStatus { get; set; }
        public string EmailStatus { get; set; }
        public string MobileStatus { get; set; }
        public string StateName { get; set; }
        public string DistrictName { get; set; }
        public string CityName { get; set; }
        public string AreaName { get; set; }

        public string CountryName { get; set; }
    }
    public partial class PartyMaster : UserAddress
    {
        public string PartyId { get; set; }
        public int? CompanyId { get; set; }
        public string ParentId { get; set; }
        public int? Type { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public int? PinCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string MobileNumber { get; set; }
        public int? StateId { get; set; }
        public string DistrictId { get; set; }
        public string CityId { get; set; }
        public int? CountryId { get; set; }
        public int? AreaId { get; set; }
        public string PanCard { get; set; }
        public string AdhaarCard { get; set; }
        public int? PoliceVerification { get; set; }
        public int? PanVerified { get; set; }
        public int? AdhaarVerified { get; set; }
        public int? EmailVerified { get; set; }
        public string MobileVerified { get; set; }
        public int? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string RegistrationNo { get; set; }
        public string GST { get; set; }
        public string Salt { get; set; }
        public int? Gender { get; set; }
        public string DateofBirth { get; set; }
        public int? RoleId { get; set; }
        public int? DepartmentId { get; set; }
        public int? GroupID { get; set; }
        public int? LevelId { get; set; }
        public string ServicesCollection { get; set; }
        public string AddressLine3 { get; set; }
        public string JanAadharNo { get; set; }
        public string DOB { get; set; }
        public long? CompanyContactNo { get; set; }
        public string CompanyEmailId { get; set; }
        public byte[] PanCardImg { get; set; }
        public byte[] AdhaarCardImg { get; set; }
        public List<PurchaseList> PurchaseLists { get; set; }
        public string ProfileImage { get; set; }
        public int IsGST_Applicable { get; set; }
        public int IsTDS_Applicable { get; set; }
        public int appTandCId { get; set; }
        public int loclvl { get; set; }

    }

    public class PurchaseList
    {
        public string CategoryID { get; set; }
        public string ServiceHardwareName { get; set; }
        public string TotalPrice { get; set; }
        public string GSTPercentage { get; set; }
        public string GSTPrice { get; set; }
        public int Type { get; set; }
    }

    #region PaymentCollection
    #endregion
}