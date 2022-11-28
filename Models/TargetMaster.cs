using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
   
    public class TargetMaster
    {
        public int? CategoryId { get; set; }
        public int? ddlCategoryId { get; set; }
        public int? IsActive { get; set; }
        public string Name { get; set; }
        public string varificationList { get; set; }
        public string DocumentList { get; set; }
        public string HardwareList { get; set; }
        public string ClassName { get; set; }

        public string TargetAmount { get; set; }
        public string TargetPeriod { get; set; }
        public string TargetType { get; set; }
        public string StartingDate { get; set; }
        public string StartTargetDate { get; set; }

    }
    public class ListTargetMasterMaster : ResponseData
    {
        public List<TargetMaster> ListRequest { get; set; }
    }
}