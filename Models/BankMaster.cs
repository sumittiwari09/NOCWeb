using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class BankMaster
    {
        public int? ID {get;set;}    
        public int? lPK_Id { get;set;}    
        public int? Status { get; set; }
        public string BankName {get;set;}
        public string IFSC { get;set;}
        public string AccountNumber {get;set;}
        public string AccountHolderName { get;set;}
        public string MobileNumber { get;set;}
        

        public class ListBankMasterMaster : ResponseData
        {
            public List<BankMaster> ListRequest { get; set; }
        }
    }
}