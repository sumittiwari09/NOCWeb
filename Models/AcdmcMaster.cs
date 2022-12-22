using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class AcdmcMaster
    {
        public string FromYear { get; set; }
        public string ToYear { get; set; }
        public int NoOfStudent { get; set; }
        public int Fk_Result { get; set; }
        public int NoOfStudentPass { get; set; }

        public decimal Percentage { get; set; }
    }
     
    public class AcdmcTableData
    {
        public string FromYear { get; set; }
        public string ToYear { get; set; }
        public int NoOfStudent { get; set; }
        public int Fk_Result { get; set; }
        public int NoOfStudentPass { get; set; }

        public string Result { get; set; }


        public decimal Percentage { get; set; }
    }
}