using System;
using System.Collections.Generic;
using System.Text;

namespace BO.Models
{
    public class CommissionDistributionMaster
    {
        public int Id { get; set; }
        public string TransectionId { get; set; }
        public string PartyId { get; set; }
        public decimal PartyCommission { get; set; }
        public decimal Calc_PartyCommission { get; set; }
        public int State { get; set; }
        public int IGST { get; set; }
        public decimal Calc_IGST { get; set; }
        public int CGST { get; set; }
        public decimal Calc_CGST { get; set; }
        public int SGST { get; set; }
        public decimal Calc_SGST { get; set; }
        public int TDS { get; set; }
        public decimal Calc_TDS { get; set; }
        public decimal Profit { get; set; }
        public decimal SoldPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public decimal TransectionAmount { get; set; }
    }

    public class LadgerTableData
    {
        public string TransactionId { get; set; }
        public int Type { get; set; }
        public string PartyId { get; set; }
        public decimal OpeningBalance { get; set; }
        public decimal ClosingBalance { get; set; }
        public decimal CommissionPaid { get; set; }
        public decimal CommissionRecieved { get; set; }
        public decimal TransectionAMT { get; set; }
        public decimal TDS { get; set; }
        public decimal GST { get; set; }
    }
}
