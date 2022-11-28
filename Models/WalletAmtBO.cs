using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class WalletAmtBO
    {
        public decimal CIW { get; set; }
        public decimal COW { get; set; }
        public decimal PWA { get; set; }
        public decimal UWA { get; set; }

        //GetDashboardPendingTransactions
        public string PartyName { get; set; }
        public string MobileNo { get; set; }
        public string TxnDate { get; set; }
        public string TxnId { get; set; }
        public string Amount { get; set; }
        public string PendingSinceInHr { get; set; }
        public string SumAmount { get; set; }

        //USP_Dashboard_Top_BusinessMakers
        // public string PartyName { get; set; }--This is use in model 
        public decimal TxnAmnt { get; set; }

        //StateWaise Transaction
        public string SName { get; set; }

        // public string TxnAmnt { get; set; }
        public string StateName { get; set; }

        //Target VS Actuaal Model
      
        public decimal AcheiveAmnt { get; set; }
        public decimal TargetAmount { get; set; }
        public decimal TargetAmount1 { get; set; }

        //public decimal TxnAmnt { get; set; }
        public string ServiceName { get; set; }

        //User Wise Count
        public int? WhiteLabel { get; set; }
        public int? Stockist { get; set; }
        public int? Distributer { get; set; }
        public int? Retailer { get; set; }
        public int? TotalCount { get; set; }

        //Wallet Request 
        public string FullName { get; set; }
        public string Mode { get; set; }
        public decimal dAmt { get; set; }
        public string Status { get; set; }
        public string dtCrtdDt { get; set; }

        //Service Income List
        public int? iPk_CatId { get; set; }
        public string CatDesc { get; set; }
        //public decimal TxnAmnt { get; set; }
        public decimal Profit { get; set; }
    }
    public class ListWalletAmtMaster : ResponseData
    {
        public List<WalletAmtBO> ListRequest { get; set; }
    }
}