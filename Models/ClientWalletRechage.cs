using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class ClientWalletRechage
    {

        public string Platform { get; set; }
        public string ToDate { get; set; }
        public string FromDate { get; set; }
        public string State { get; set; }
        public string FileType { get; set; }
        public string Receiptbase64 { get; set; }
        public string ApproveRejectDate { get; set; }
        public string EntryDate { get; set; }
        public string Status { get; set; }
        public string RechargeId { get; set; }
        public string Message { get; set; }
        public string Amount { get; set; }
        public string UTRNo { get; set; }
        public string BankName { get; set; }
        public string UPI { get; set; }
        public string AccountNo { get; set; }
        public string ParentId { get; set; }
        public string TransactionType { get; set; }
        public string PartyName { get; set; }
        public string PartyId { get; set; }
        public string Type { get; set; }
        public string GSTNumber { get; set; }
        public string PANNumber { get; set; }
        public string MobileNo { get; set; }
        public string SrNo { get; set; }
        public string AmountWord { get; set; }
        public string SystemTransactionId { get; set; }
        public string DepositeDate { get; set; }
        public string Note { get; set; }
        public string WalletRechargeId { get; set; }
        public string PayFrom { get; set; }
        public string Mode { get; set; }
        public string PayMode { get; set; }
        public string ToBankAccount { get; set; }
        public string DocumentURL { get; set; }
        public string OrderId { get; set; }
    }

    public class WalletRequestResponse
    {
        public string Messsage { get; set; }
        public string ResponseCode { get; set; }
        public List<ClientWalletRechage> WalletRechargeLst { get; set; }
    }
    public class WalletLeft
    {
        public string PartyID { get; set; }
        public string MainWalletBalance { get; set; }
    }
    public class WalletRechargeHistory : ClientWalletRechage
    {
        public string BtnValue { get; set; }
        public string FilterType { get; set; }
        public string FilterValue { get; set; }

    }
    public class BankDetailsBO : ErrorBO
    {
        public List<AdminBankDetailsBO> Banks { get; set; }
    }

    public class AdminBankDetailsBO
    {
        public string AtishayVendorID { get; set; }
        public string BankAccountNo { get; set; }
        public string BankName { get; set; }
        public string BankIFSC { get; set; }
    }
}