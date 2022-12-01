using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class AEPSTransaction
    {
        public class AEPSRequest
        {
            public string tranType { get; set; }
            public string AadhaarNo { get; set; }
            public string BankIINno { get; set; }
            public decimal Amount { get; set; }

            public List<DropDownBO> TranTypeList = new List<DropDownBO>();

            public List<AEPSTransaction.BankiINNo> BankDetailsList = new List<AEPSTransaction.BankiINNo>();
            public string merchantTranId { get; set; }
            public CaptureResponse captureResponse { get; set; }
            public CardnumberORUID cardnumberORUID { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string mobileNumber { get; set; }
            public string transactionAmount { get; set; }
            public string transactionType { get; set; }
            public string hdndata { get; set; }
            public string EncryAadhaarNo { get; set; }
            public string Platform { get; set; }
            public string DeviceIMEI { get; set; }

            public string ScannerDevice { get; set; }

        }

        public class AEPSRequestAPI
        {
            public string AadhaarNo { get; set; }
            public CaptureResponse captureResponse { get; set; }
            public CardnumberORUID cardnumberORUID { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
            public string mobileNumber { get; set; }
            public string paymentType { get; set; }
            public decimal transactionAmount { get; set; }
            public string transactionType { get; set; }
            public string Platform { get; set; }
            public string DeviceIMEI { get; set; }
            public string ReferenceTransactionID { get; set; }
        }


        public class CaptureResponse
        {
            public string PidDatatype { get; set; }
            public string Piddata { get; set; }
            public string ci { get; set; }
            public string dc { get; set; }
            public string dpID { get; set; }
            public string errCode { get; set; }
            public string errInfo { get; set; }
            public string fCount { get; set; }
            public string fType { get; set; }
            public string hmac { get; set; }
            public string iCount { get; set; }
            public string mc { get; set; }
            public string mi { get; set; }
            public string nmPoints { get; set; }
            public string pCount { get; set; }
            public string pType { get; set; }
            public string qScore { get; set; }
            public string rdsID { get; set; }
            public string rdsVer { get; set; }
            public string sessionKey { get; set; }
        }
        public class CardnumberORUID
        {
            public string adhaarNumber { get; set; }
            public int indicatorforUID { get; set; }
            public string nationalBankIdentificationNumber { get; set; }
        }
        public class AEPSResponse
        {
            public bool status { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
            public string statusCode { get; set; }
        }
        public class Data
        {
            public string terminalId { get; set; }
            public string requestTransactionTime { get; set; }
            public decimal transactionAmount { get; set; }
            public string transactionStatus { get; set; }
            public decimal balanceAmount { get; set; }
            public string bankRRN { get; set; }
            public string transactionType { get; set; }
            public string fingpayTransactionId { get; set; }
            public string merchantTxnId { get; set; }
        }
        public class Datum
        {
            public int id { get; set; }
            public int activeFlag { get; set; }
            public string bankName { get; set; }
            public string details { get; set; }
            public string iINNo { get; set; }
            public string remarks { get; set; }
            public string timestamp { get; set; }
        }
        public class BankiINNoAEPS : ErrorBO
        {
            public bool status { get; set; }
            public string message { get; set; }
            public List<Datum> data { get; set; }
            public int statusCode { get; set; }
            public List<BankiINNo> bankiINNo { get; set; }
        }
        public class BankiINNo
        {
            public string bankName { get; set; }
            public string iINNo { get; set; }
        }
        public class DropDownBO
        {
            public string ListKey { get; set; }
            public string ListValue { get; set; }
            public string ListKeyStr { get; set; }

        }
        public class AEPSAPIResponse
        {
            public string APIResponseCode { get; set; }
            public string APIResponseMessage { get; set; }
            public string Message { get; set; }
            public string ResponseCode { get; set; }
            public details details { get; set; }
        }
        public class details
        {
            public string terminalId { get; set; }
            public string requestTransactionTime { get; set; }
            public decimal transactionAmount { get; set; }
            public string transactionStatus { get; set; }
            public decimal balanceAmount { get; set; }
            public string bankRRN { get; set; }
            public string transactionType { get; set; }
            public string merchantTxnId { get; set; }
        }


        public class AEPSBOWEBREQ : RequestData
        {
            public string APIResponseCode { get; set; }
            public string APIResponseMessage { get; set; }
            public string FromDate { get; set; }
            public string ToDate { get; set; }
            public string TransactionId { get; set; }
            public string TransactionStatus { get; set; }
            public string BtnValue { get; set; }
            public string Mobileno { get; set; }
            public string Status { get; set; }

            public List<AEPSBOWEBRES> AEPSRecords = new List<AEPSBOWEBRES>();

        }

        public class AEPSBOWEBRES
        {
            public string PartyId { get; set; }
            public string CompanyName { get; set; }
            public string CompanyAddress { get; set; }
            public string TransactionId { get; set; }
            public string transType { get; set; }
            public decimal Amount { get; set; }
            public string RRN { get; set; }
            public string EntryDate { get; set; }
            public string Mobileno { get; set; }
            public string IMEI { get; set; }
            public string Status { get; set; }
            public string statusdesc { get; set; }
            public string SettlementMessage { get; set; }
            public string APIPlatform { get; set; }
            public string CardNo { get; set; }
        }
    }
}