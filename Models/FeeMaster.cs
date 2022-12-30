using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class FeeMaster
    {
        public int iPK_ID { get; set; }
        public string Type { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Deptname { get; set; }
        public string nocTypeName { get; set; }
        public string Feetype { get; set; }
        public int DeptID { get; set; }
        public int NOCApplID { get; set; }
        public string    NocApplication { get; set; }
        public int NOCTypID { get; set; }
        public int FeeTypID { get; set; }
        public decimal dBoys { get; set; }
        public decimal dGirls { get; set; }
        public decimal dCoEdu { get; set; }
        public decimal dCutOff { get; set; }

    }
}