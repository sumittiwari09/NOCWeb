using System;
using System.Collections.Generic;
using System.Text;

namespace BO.Models
{
    public class DMT_ModelCollection
    {



    }
    public class DMTBaseURL
    {
        public string Url { get; set; }
    }
    public class DMT_SendOTPRequest
    {
        public string customerMobile { get; set; }
        public string otpType { get; set; }
        public sendOTPBeneficiaryBankAccount properties { get; set; }
    }

    public class sendOTPBeneficiaryBankAccount
    {
        public string benAccNum { get; set; }
    }


    public class DMT_UserPreValidationRepsonse
    {
        public string status { get; set; }
        public int response_code { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string customerMobile { get; set; }
        public double limitLeft { get; set; }
    }
    public class DMT_SendOTPResponse
    {
        public string status { get; set; }
        public int response_code { get; set; }
        public string txn_id { get; set; }
        public string state { get; set; }
    }


    #region View Beneficiary List Model
    public class DMT_AccountDetail
    {
        public string accountNumber { get; set; }
        public string ifscCode { get; set; }
        public string bankName { get; set; }
        public string accountHolderName { get; set; }
    }

    public class DMT_Beneficiary
    {
        public string beneficiaryId { get; set; }
        public string uuid { get; set; }
        public DMT_AccountDetail accountDetail { get; set; }
    }

    public class DMT_BeneficiaryList
    {
        public string status { get; set; }
        public int response_code { get; set; }
        public string customerMobile { get; set; }
        public List<DMT_Beneficiary> beneficiaries { get; set; }
        public int totalCount { get; set; }
    }
    #endregion


    #region Add beneficiary request model


    public class BeneficiaryDetails
    {
        public string accountNumber { get; set; }
        public string bankName { get; set; }
        public string benIfsc { get; set; }
    }

    public class AddBeneficairyRequestModel
    {
        public BeneficiaryDetails beneficiaryDetails { get; set; }
        public string channel { get; set; } = "S2S";
        public string customerMobile { get; set; }
        public string transactionType { get; set; } = "CORPORATE_PENNY_DROP";
        public string txnReqId { get; set; }
    }

    public class BeneAcc_ExtraInfo
    {
        public object beneficiaryName { get; set; }
    }

    public class AddBeneficairyResponse
    {
        public string status { get; set; }
        public string message { get; set; }
        public double amount { get; set; }
        public string customerMobile { get; set; }
        public int response_code { get; set; }
        public string txn_id { get; set; }
        public string mw_txn_id { get; set; }
        public BeneAcc_ExtraInfo extra_info { get; set; }
        public string rrn { get; set; }
        public string transactionDate { get; set; }
    }
    #endregion


    #region DMT User Registration Models

    public class DMT_Address
    {
        public string name { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string pin { get; set; }
        public string mobile { get; set; }
    }

    public class DMT_Name
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class DMT_UserRegistrationRequest
    {
        public string customerMobile { get; set; }
        public string otp { get; set; }
        public string state { get; set; }
        public DMT_Name name { get; set; }
        public DMT_Address address { get; set; }
    }


    #endregion




}
