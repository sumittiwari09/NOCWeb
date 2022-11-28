using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class PartwiseStatementBO
    {
        public string PartyName { get; set; }
        public string TxnId { get; set; }
        public string TxnDate { get; set; }
        public string OpAmnt { get; set; }
        public string CreditAmnt { get; set; }
        public string DebitAmnt { get; set; }
        public string ClosingAmnt { get; set; }
        public string Narration { get; set; }
    }
}