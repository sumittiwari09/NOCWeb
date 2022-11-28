using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    #region Ledger Report
    public class LedgerReport : RequestData
    {
        public long LedgerID { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal ClosingBalance { get; set; }
        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string BtnValue { get; set; }
        public string FilterType { get; set; }
        public string FilterValue { get; set; }
        public string MobileNo { get; set; }
        public string Type { get; set; }
    }
    public class LedgerReportDetails : ErrorBO
    {
        public List<ALLLedgerReport> Data { get; set; }

        public DataTable DownloadData { get; set; }
    }
    public class ALLLedgerReport
    {
        public string PartyID { get; set; }
        public string PartyName { get; set; }
        public string ParentName { get; set; }
        public long LedgerID { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public decimal ClosingBalance { get; set; }
        public string TransactionDate { get; set; }
        public string Narration { get; set; }
        public string TransactionID { get; set; }
        public string MobileNo { get; set; }
        public string PANNumber { get; set; }
        public string GSTNumber { get; set; }
        public string State { get; set; }
        public string SrNo { get; set; }
        public string Name { get; set; }
        public string Amount { get; set; }
        public string TotalAmount { get; set; }
        public string ServiceType { get; set; }
        public string CommonTransactionId { get; set; }
    }

    #endregion
}