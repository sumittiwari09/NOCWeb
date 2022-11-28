using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class SwitchVendorResponse
    {
        public string Messsage { get; set; }
        public string ResponseCode { get; set; }
        public List<VendetList> vendetList { get; set; }
        public List<Datum> Data { get; set; }
        public List<SwitchClientReport> switchpartyList { get; set; }
        public List<SwitchClientLedger> switchpartyledger { get; set; }
    }
    public class SwitchVendorRequest
    {
        public string PartyId { get; set; }
        public string FromASMID { get; set; }
        public string ToASMID { get; set; }
        public string FromParentID { get; set; }
        public string ToParentID { get; set; }
        public string SwitchVendorId { get; set; }
        public string SwitchType { get; set; }
        public string Type { get; set; }
        public string PartyType { get; set; }
        public string ParentID { get; set; }


    }
    public class VendetList
    {
        public string FullName { get; set; }
        public string ParentName { get; set; }
        public string State { get; set; }
        public string VendorId { get; set; }
        public string Wallet { get; set; }
        public string ASMID { get; set; }
        public string ParentId { get; set; }
        public string ASMName { get; set; }
        public string MobileNo { get; set; }
    }
    public class Datum
    {
        public string ListID { get; set; }
        public string ListName { get; set; }
    }

    public class SwitchClientReport
    {
        public string SrNo { get; set; }
        public string MobileNo { get; set; }
        public string VendorId { get; set; }
        public string VendorName { get; set; }
        public string OldParentName { get; set; }
        public string NewParentName { get; set; }
        public string WalletAmount { get; set; }
        public string CreatedDate { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string CreatedByName { get; set; }
        public string SwitchId { get; set; }
        public string BtnValue { get; set; }
        public string OldASMName { get; set; }
        public string NewASMName { get; set; }
    }
    public class SwitchClientLedger
    {
        public string ClientName { get; set; }
        public string OpeningBalance { get; set; }
        public string ClosingBalance { get; set; }
        public string Credit { get; set; }
        public string Debit { get; set; }
        public string Narration { get; set; }
    }
}