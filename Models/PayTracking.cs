using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class PayTracking
    {
        public int PayTrackingId { get; set; }
        public string OrderId { get; set; }
        public Nullable<decimal> PayableAmount { get; set; }
        public string TransactionDate { get; set; }
        public Nullable<decimal> TransactionAmt { get; set; }
        public Nullable<decimal> BalancaAmt { get; set; }
        public Nullable<int> Mode { get; set; }
        public string PaymentMode { get; set; }
        public string Bank { get; set; }
        public string Account { get; set; }
        public string UPI { get; set; }
        public string ChequeNo { get; set; }
        public string CollectedBy { get; set; }
        public string CurrentStatus { get; set; }
        public string Narration { get; set; }
        public Nullable<int> PaymentStatus { get; set; }

        public string PartyId { get; set; }
        public Nullable<int> ChequeStatus { get; set; }
        public string ModeType { get; set; }
        public string BankUTR { get; set; }

        public string PaymentStatusName { get; set; }
    }
}