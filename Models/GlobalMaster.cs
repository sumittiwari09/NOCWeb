using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class GlobalMaster
    {
        public int GlobalId { get; set; }
        public string Name { get; set; }
        public int? IsActive { get; set; }
        public int? EnumNo { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateOn { get; set; }
        public string UpdateBy { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string PartyId { get; set; }
    }

    public class tblGlobalMaster
    {
        public string Stype { get; set; }
        public string SName { get; set; }
        public string SDcrptn { get; set; }
        public string SCrtBy { get; set; }

        public DateTime? DtcrtOn { get; set; }

        public bool BisActv { get; set; }

    }
}