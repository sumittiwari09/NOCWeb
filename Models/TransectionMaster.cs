using System;
using System.Collections.Generic;
using System.Text;

namespace BO.Models
{
    public class TransectionMaster
    {
        public int Id { get; set; }
        public string TransectionId { get; set; }
        public int UserType { get; set; }
        public string PartyId { get; set; }
        public string ParentsId { get; set; }
        public string RefrenceId { get; set; }
        public int ServicetypeId { get; set; }
        public int OperatorId { get; set; }
        public int ServiceProviderId { get; set; }
        public decimal TransectionAmount { get; set; }
        public int CustomerFeeType { get; set; }
        public decimal CustomerFee { get; set; }
        public decimal ValueOfCustomerFee { get; set; }
        public decimal Commission { get; set; }
        public decimal Calc_Commission { get; set; }
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
        public decimal Purchase_Price { get; set; }
        public string MobileNo { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string IPAddress { get; set; }
        public string Card { get; set; }
        public string Bank { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
        public string Branch { get; set; }
    }

    public class TransectionMasterRequest
    {
        public string TransectionId { get; set; }
        public decimal TransectionAmount { get; set; }
        public string RefrenceId { get; set; }
        public string PartyId { get; set; }
        public int UserType { get; set; }
        public int OperatorId { get; set; }
        public int ServicetypeId { get; set; }
        public int ServiceProviderId { get; set; }
        public int CGST { get; set; }
        public int SGST { get; set; }
        public int IGST { get; set; }
        public int TDS { get; set; }
        public int CustomerFeeType { get; set; }
        public decimal CustomerFee { get; set; }
        public decimal ValueOfCustomerFee { get; set; }
        public string MobileNo { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string IPAddress { get; set; }
        public string Card { get; set; }
        public string Bank { get; set; }
        public string AccountNo { get; set; }
        public string IFSC { get; set; }
        public string Branch { get; set; }
    }

    public class APIReqResModal
    {

        public int ReqResID { get; set; }
        public string TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public string API_Request { get; set; }
        public string API_Response { get; set; }
    }
}
