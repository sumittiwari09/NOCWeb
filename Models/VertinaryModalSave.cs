using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class VertinaryModalSave
    {
        public int iPK_VetnryId { get; set; }
        public string sNameOfVtnry { get; set; }
        public decimal dDistance { get; set; }
        public string sPersnNm { get; set; }
        public string sMobileNO { get; set; }
        public string sLocation { get; set; }
        public string sEmailId { get; set; }
        public int IsOnBoard { get; set; }
        public string sNameOfAdmin { get; set; }
        public int iFk_DesgntnId { get; set; }
        public string sRelWithInst { get; set; }
        public string sRemark { get; set; }

    }

}