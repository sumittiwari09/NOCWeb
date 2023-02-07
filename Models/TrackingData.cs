using System;
using System.Collections.Generic;
using System.Text;

namespace NewZapures_V2.Models
{
    public class TrackingData
    {
        public int ipk_TrkID { get; set; }
        public string sApplNo { get; set; }
        public string sSndrSSO { get; set; }
        public int sSndrRolID { get; set; }
        public int sSndrLvlID { get; set; }
        public string sRcvrSSO { get; set; }
        public int sRcvrRolID { get; set; }
        public int sRcvrLvlID { get; set; }
        public string dtPrcDate { get; set; }
        public string sPrcName { get; set; }
        public string sNrtn { get; set; }
        public string sAction { get; set; }
    }
}
