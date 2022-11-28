using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class PartyProfile
    {
        public string OrderId { get; set; }

        public string StringType { get; set; }
        public string PartyId { get; set; }
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
    public class PandingPaymentList
    {
        public int PayTrackingId { get; set; }
        public string OrderId { get; set; }
        public string RegistrationNo { get; set; }
        public string StringType { get; set; }
        public string PartyId { get; set; }
        public string Party { get; set; }
        public string Name { get; set; }
        public string StateCity { get; set; }
        public string Service { get; set; }
        public decimal Devices { get; set; }
        public decimal Transactionamt { get; set; }
        public decimal BalanceAmt { get; set; }
        public decimal Charges { get; set; }

        public string ChequeNo { get; set; }

    }

    public class Partsummary
    {

        public int payTrackingId { get; set; }
        public string OrderId { get; set; }
        public string PartyId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public string Address { get; set; }
        public decimal BalanceAmount { get; set; }
    }

    public class ServiesDetails
    {
        public string PartyId { get; set; }
        public string ServiceType { get; set; }
        public string Service { get; set; }
        public string ChargeType { get; set; }
        public decimal Charge { get; set; }
        public decimal GST { get; set; }
        public decimal IGST { get; set; }
        public decimal CGST { get; set; }
        public decimal Commision { get; set; }
        public decimal ServiceCharge { get; set; }
        public decimal CourierCharge { get; set; }
        public decimal TotalAmount { get; set; }

    }
    public class PaymentCollectionModel
    {
        public int payTrackingId { get; set; }
        public string OrderId { get; set; }
        public string PartyId { get; set; }
        public string PaymentMode { get; set; }
        public string ChequeNumber { get; set; }
        public string Bank { get; set; }
        public string AccountNumber { get; set; }
        public decimal CollectedAmt { get; set; }
        public decimal BalanceAmt { get; set; }
        public string RecivedBy { get; set; }
    }
    public class PartyPaymentCollect
    {
        public int? Flag { get; set; }
        public string Message { get; set; }
        public Partsummary partsummary { get; set; }

        public decimal serviceGst { get; set; }
        public List<ServiesDetails> ServiesDetailsList { get; set; }

        public PaymentCollectionModel collectMoney { get; set; }
        public decimal GrandTotal { get; set; }

    }

    public class ChangeRquestDataList
    {
        public string State { get; set; }
        public string SequestFor { get; set; }
        public string DoYouNowFor { get; set; }
        public string PartyType { get; set; }

        public ClientRequestData ExistingParentDetails { get; set; }
        public ClientRequestData RequestedParentDetails { get; set; }
        public List<ClientRequestData> ParentDetails { get; set; }
    }

    public class ClientRequestData
    {
        public string SequestFor { get; set; }
        public string DoYouNowFor { get; set; }
        public string PartyId { get; set; }
        public string ParentId { get; set; }
        public string CompanyId { get; set; }
        public int IsActive { get; set; }
        public string Name { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }

        public string Mobilenumber { get; set; }
        public string EmailId { get; set; }
        public string RegistrationId { get; set; }

        public string State { get; set; }
        public string District { get; set; }
        public string PartyType { get; set; }
        public string Currentstatus { get; set; }


        public string FromPartyId { get; set; }
        public string FromParentId { get; set; }
        public string FromCompanyId { get; set; }
        public int FromIsActive { get; set; }
        public string FromName { get; set; }
        public string FromCompanyName { get; set; }
        public string FromCompanyAddress { get; set; }

        public string FromMobilenumber { get; set; }
        public string FromEmailId { get; set; }
        public string FromRegistrationId { get; set; }

        public string FromState { get; set; }
        public string FromDistrict { get; set; }
        public string FromPartyType { get; set; }
        public string Narration { get; set; }
    }

    public class ClientSwitchRequestData
    {
        public string PartyId { get; set; }
        public string PartyName { get; set; }
        public string RegistrationNo { get; set; }
        public string MobileNo { get; set; }
        public string PartyType { get; set; }
        public string State { get; set; }
        public string District { get; set; }
        public double MainWalletBalance { get; set; }
        public string CompanyAddress { get; set; }


        public string FromParentId { get; set; }
        public string FromParentName { get; set; }
        public string FromParentRegistrationNo { get; set; }
        public string FromParentMobileNo { get; set; }
        public string FromParentPartyType { get; set; }
        public string FromParentState { get; set; }
        public string FromParentDistrict { get; set; }
        public double FromMainWalletBalance { get; set; }

        public string ToParentId { get; set; }
        public string ToParentName { get; set; }
        public string ToParentRegistrationNo { get; set; }
        public string ToParentMobileNo { get; set; }
        public string ToParentPartyType { get; set; }
        public string ToParentState { get; set; }
        public string ToParentDistrict { get; set; }
        public double ToMainWalletBalance { get; set; }

        public string Currentstatus { get; set; }

    }
}