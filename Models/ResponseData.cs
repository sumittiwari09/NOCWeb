using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewZapures_V2.Models
{
    //public class ResponseData
    //{
    //    public string ResponseCode { get; set; }
    //    public int statusCode { get; set; }
    //    public string Message { get; set; }
    //    public int UserID { get; set; }
    //    public int ID { get; set; }
    //    public JObject Data { get; set; }
    //    public string Body { get; set; }
    //}
    public class RequestData : ErrorBO
    {
        public string PartyId { get; set; }
        public string Token { get; set; }
        public string URL { get; set; }
    }
    public class ErrorBO
    {
        public string ResponseCode { get; set; }
        public string Messsage { get; set; }
        public string Id { get; set; }
    }
    public class Response
    {
        public string StatusCode { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
        public int Total { get; set; }
    }

    public class ResponseData
    {
        public string ResponseCode { get; set; }
        public int statusCode { get; set; }
        public string Message { get; set; }
        public string UserID { get; set; }
        public int ID { get; set; }
        public object Data { get; set; }
        public string Body { get; set; }
        public string JWT { get; set; }
        public string imagename { get; set; }
        public string RegistrationId { get; set; }
    }
    public class AddPurchase
    {
        public string PartyId { get; set; }
        public string OrderId { get; set; }
        public int appTandCId { get; set; }
        public List<PurchaseList> PurchaseLists { get; set; }
    }
    public class ContentPage
    {
        public string Id { get; set; }
        public string PageTitle { get; set; }
        public string PageImage { get; set; }
        public string PageKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string PageContent { get; set; }
        public string PageURL { get; set; }
        public string Type { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IsActive { get; set; }
        public string Message { get; set; }
        public int? EnumId { get; set; }
    }

    public class MyPayment
    {
        public string OrderNo { get; set; }
        public string OrderDate { get; set; }
        public string item { get; set; }
        public string TransectionAmount { get; set; }
        public string BalanceAmount { get; set; }
        public string PaymentMode { get; set; }
        public string PaymentStatus { get; set; }
    }

    public class UploadDoc
    {
        public string PartyId { get; set; }
        public byte[] Image { get; set; }
        public string ImageURL { get; set; }
        public string DocumentID { get; set; }
        public string DocumentName { get; set; }


    }


    #region Aadhar Full Details
    public class AadharAddress
    {
        public string country { get; set; }
        public string dist { get; set; }
        public string state { get; set; }
        public string po { get; set; }
        public string loc { get; set; }
        public string vtc { get; set; }
        public string subdist { get; set; }
        public string street { get; set; }
        public string house { get; set; }
        public string landmark { get; set; }
    }

    public class AadharData
    {
        public string client_id { get; set; }
        public string full_name { get; set; }
        public string aadhaar_number { get; set; }
        public string dob { get; set; }
        public string gender { get; set; }
        public AadharAddress address { get; set; }
        public bool face_status { get; set; }
        public int face_score { get; set; }
        public string zip { get; set; }
        public string profile_image { get; set; }
        public bool has_image { get; set; }
        public string email_hash { get; set; }
        public string mobile_hash { get; set; }
        public string raw_xml { get; set; }
        public string zip_data { get; set; }
        public string care_of { get; set; }
        public string share_code { get; set; }
        public bool mobile_verified { get; set; }
        public string reference_id { get; set; }
        public object aadhaar_pdf { get; set; }
    }

    public class AadharFullDetails
    {
        public AadharData data { get; set; }
        public int status_code { get; set; }
        public bool success { get; set; }
        public bool Failure { get; set; }
        public object message { get; set; }
        public string message_code { get; set; }
    }

    public class AadharWithPartyId
    {
        public string PartyId { get; set; }
        public AadharFullDetails Aadhaar { get; set; }
    }


    public class AadhaarDetails
    {
        public string PartyId { get; set; }
        public string Mobile { get; set; }
        public string AadhaarDist { get; set; }
        public string AadhaarPO { get; set; }
        public string AadhaarLoc { get; set; }
        public string AadhaarVtc { get; set; }
        public string AadhaarSubdist { get; set; }
        public string AadhaarStreet { get; set; }
        public string Aadhaarhouse { get; set; }
        public string Aadhaarlandmark { get; set; }
        public string AadhaarDOB { get; set; }
        public string AadhaarCareOf { get; set; }
        public string AadhaarName { get; set; }
        public string AadhaarPincode { get; set; }

    }

    #endregion

    #region AadharAndPAN Model
    public class AdharPANVerification
    {
        public string id_number { get; set; }
    }
    public class AdharVerificationWithOTP
    {
        public string client_id { get; set; }
        public string otp { get; set; }
    }
    #endregion

    #region PANcard And AdharOTP Response


    public class ResponsePAN
    {
        public string client_id { get; set; }
        public string pan_number { get; set; }
        public string full_name { get; set; }
        public string category { get; set; }
    }

    public class PANRoot
    {
        public ResponsePAN data { get; set; }
        public int status_code { get; set; }
        public bool success { get; set; }
        public object message { get; set; }
        public string message_code { get; set; }
    }




    #endregion

    #region MobileOTPVerification
    public class MessageData
    {
        public string Number { get; set; }
        public string MessageId { get; set; }
        public object Message { get; set; }
    }

    public class MobileOTPResponse
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string JobId { get; set; }
        public string OTP { get; set; }
        public List<MessageData> MessageData { get; set; }
    }
    #endregion

    #region Operator List
    public class OperatorsList
    {
        public string OperatorId { get; set; }
        public string OperatorName { get; set; }
        public string OperatorImage { get; set; }
        public string Type { get; set; }

    }
    #endregion

    #region Mobile Operator Check

    public class MobileOperator
    {
        public int status { get; set; }
        public string Operator { get; set; }
    }

    public class MobileOperatorResponse
    {
        public string tel { get; set; }
        public MobileOperator records { get; set; }
    }


    #endregion

    #region Mobile Recharge_ROffer
    public class ROffers
    {
        public string rs { get; set; }
        public string desc { get; set; }
    }
    public class ROfferr_Response
    {
        public string tel { get; set; }
        public string @operator { get; set; }
        public List<ROffers> records { get; set; }
        public int status { get; set; }
    }

    #endregion

    #region MobileRecharge Simple


    public class _3G4G
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class COMBO
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class FRC
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class FULLTT
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class RATECUTTER
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class SimpalPlans
    {
        public List<FULLTT> FULLTT { get; set; }
        public List<TOPUP> TOPUP { get; set; }

        [JsonProperty("3G/4G")]
        public List<_3G4G> _3G4G { get; set; }

        [JsonProperty("RATE CUTTER")]
        public List<RATECUTTER> RATECUTTER { get; set; }
        public List<SM> SMS { get; set; }
        public List<Romaing> Romaing { get; set; }
        public List<COMBO> COMBO { get; set; }
        public List<FRC> FRC { get; set; }
        public List<_3G4G> three_four_g { get; set; }
        public List<RATECUTTER> rate_cutter { get; set; }
    }

    public class Romaing
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }
    public class SM
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }
    public class TOPUP
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }
    public class SimpalPlansResponse
    {
        public SimpalPlans records { get; set; }
        public int status { get; set; }
    }


    #endregion

    #region DTH Customer Info

    public class DTHCustomerInfo
    {
        public object MonthlyRecharge { get; set; }
        public string Balance { get; set; }
        public string customerName { get; set; }
        public object status { get; set; }
        public string NextRechargeDate { get; set; }
        public string planname { get; set; }
    }

    public class DTHCustomerInfoResponse
    {
        public string tel { get; set; }
        public string @operator { get; set; }
        public List<DTHCustomerInfo> records { get; set; }
        public int status { get; set; }
    }


    #endregion

    #region DTH Plans Without Channel
    public class AddOnPackWOC
    {
        public DTHWOCRs rs { get; set; }
        public string desc { get; set; }
        public string plan_name { get; set; }
        public string last_update { get; set; }
    }
    public class DTHPlanWOC
    {
        public DTHWOCRs rs { get; set; }
        public string desc { get; set; }
        public string plan_name { get; set; }
        public string last_update { get; set; }
    }
    public class DTHPlanWOCData
    {
        public List<DTHPlanWOC> Plan { get; set; }

        [JsonProperty("Add-On Pack")]
        public List<AddOnPackWOC> AddOnPack { get; set; }
    }
    public class DTHPlanWOCResponse
    {
        public DTHPlanWOCData records { get; set; }
        public int status { get; set; }
    }
    public class DTHWOCRs
    {
        [JsonProperty("1 MONTHS")]
        public string _1MONTHS { get; set; }

        [JsonProperty("3 MONTHS")]
        public string _3MONTHS { get; set; }

        [JsonProperty("6 MONTHS")]
        public string _6MONTHS { get; set; }

        [JsonProperty("1 YEAR")]
        public string _1YEAR { get; set; }
    }
    #endregion

    #region DTH Plans With Channel
    public class AddOnPackWC
    {
        public DTHWCRs rs { get; set; }
        public string desc { get; set; }
        public string plan_name { get; set; }
        public DTHWCChannel channel { get; set; }
        public string last_update { get; set; }
    }

    public class DTHWCChannel
    {
        [JsonProperty("HINDI ENTERTAINMENT")]
        public List<string> HINDIENTERTAINMENT { get; set; }
        public List<string> DOORDARSHAN { get; set; }
        public List<string> SHOPPING { get; set; }

        [JsonProperty("HINDI MOVIES")]
        public List<string> HINDIMOVIES { get; set; }

        [JsonProperty("HINDI NEWS")]
        public List<string> HINDINEWS { get; set; }

        [JsonProperty("ENGLISH NEWS")]
        public List<string> ENGLISHNEWS { get; set; }
        public List<string> SPORTS { get; set; }
        public List<string> KNOWLEDGE { get; set; }
        public List<string> LIFESTYLE { get; set; }
        public List<string> MUSIC { get; set; }
        public List<string> KIDS { get; set; }
        public List<string> GUJARATI { get; set; }
        public List<string> DEVOTIONAL { get; set; }
        public List<string> BENGALI { get; set; }
        public List<string> ORIYA { get; set; }
        public List<string> PUNJABI { get; set; }
        public List<string> MARATHI { get; set; }
        public List<string> TAMIL { get; set; }
        public List<string> TELUGU { get; set; }
        public List<string> KANNADA { get; set; }
        public List<string> MALAYALAM { get; set; }

        [JsonProperty("OTHER REGIONAL")]
        public List<string> OTHERREGIONAL { get; set; }

        [JsonProperty("ENGLISH ENTERTAINMENT")]
        public List<string> ENGLISHENTERTAINMENT { get; set; }

        [JsonProperty("ENGLISH MOVIES")]
        public List<string> ENGLISHMOVIES { get; set; }
    }

    public class DTHWCPlan
    {
        public DTHWCRs rs { get; set; }
        public string desc { get; set; }
        public string plan_name { get; set; }
        public DTHWCChannel channel { get; set; }
        public string last_update { get; set; }
    }

    public class DTHWCData
    {
        public List<DTHWCPlan> Plan { get; set; }

        [JsonProperty("Add-On Pack")]
        public List<AddOnPackWC> AddOnPack { get; set; }
    }

    public class DTHWCResponse
    {
        public DTHWCData records { get; set; }
        public int status { get; set; }
    }

    public class DTHWCRs
    {
        [JsonProperty("1 MONTHS")]
        public string _1MONTHS { get; set; }

        [JsonProperty("3 MONTHS")]
        public string _3MONTHS { get; set; }

        [JsonProperty("6 MONTHS")]
        public string _6MONTHS { get; set; }

        [JsonProperty("1 YEAR")]
        public string _1YEAR { get; set; }
    }



    #endregion

    #region Electricity Customer Info
    public class ElectricityCustomerInfo
    {
        public string CustomerName { get; set; }
        public string Billamount { get; set; }
        public string Billdate { get; set; }
        public string Duedate { get; set; }
    }
    public class ElectricityCustomerInfoResponse
    {
        public string tel { get; set; }
        public string @operator { get; set; }
        public List<ElectricityCustomerInfo> records { get; set; }
        public int status { get; set; }
    }



    #endregion

    #region Shakti Code

    public class CheckWalletResponse
    {
        public int STATUS { get; set; }
        public string MESSAGE { get; set; }
        public string BALANCE { get; set; }
        public string ERRORCODE { get; set; }
        public int HTTPCODE { get; set; }
    }
    public class RechargeStatusResponse
    {
        public string CUSTOMERNO { get; set; }
        public string OPERATOR { get; set; }
        public double AMOUNT { get; set; }
        public int STATUS { get; set; }
        public string MESSAGE { get; set; }
        public string CIRCLE { get; set; }
        public string ERRORCODE { get; set; }
        public long TXNNO { get; set; }
        public string OPTXNID { get; set; }
        public string REQUESTTXNID { get; set; }
        public int HTTPCODE { get; set; }
    }


    public class RechargeRequestResponse
    {
        public int STATUS { get; set; }
        public string MESSAGE { get; set; }
        public string ERRORCODE { get; set; }
        public string OPTXNID { get; set; }
        public string TXNNO { get; set; }
        public string REQUESTTXNID { get; set; }
        public int HTTPCODE { get; set; }
    }


    #endregion


}