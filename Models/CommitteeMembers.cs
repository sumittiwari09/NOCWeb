using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class CommitteeMembers
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public string SSOID { get; set; }
        public string EmailAddress { get; set; }
    }
    public class Committee
    {
        public int iPk_AsgnID { get; set; }
        public int iFk_CmtyID { get; set; }
        public int iFk_CmtytypeID { get; set; }
        public string sApplNo { get; set; }
        public string sCrtdBy { get; set; }
        public string dtCrtdOn { get; set; }
        public string isNotfy { get; set; }
        public string dtStrtDate { get; set; }
        public string tStrttime { get; set; }
        public string dtEndDate { get; set; }
        public string tEndtime { get; set; }

    }
}