using System;
using System.Collections.Generic;
using System.Text;

namespace NewZapures_V2.Models
{
    public class InspectionComments
    {
        public int iComtId { get; set; }
        public string sUsrId { get; set; }
        public string userName { get; set; }
        public string sDiscription { get; set; }
        public string sUpdimg { get; set; }
        public string dtCtrdate { get; set; }
        public int? iCnt { get; set; }
        public string sCntlst { get; set; }
        public string sAppid { get; set; }
        public string sPageName { get; set; }
        public int? iFK_RemarkId { get; set; }
        public int? iFK_FieldId { get; set; }
        public int? iVal { get; set; }
        public string Valueis { get; set; }
        public string type { get; set; }


      
       
        
    }
    public class CommitedMstview : InspectionComments
    {
        public string sName { get; set; }
        public string CtDate { get; set; }
        public string Remarklist { get; set; }
        public string ValueName
        {
            get
            {
                switch (iVal)
                {
                    case 1:
                        return "Correct";
                    case 2:
                        return "Wrong";


                    default:
                        return "";
                }
            }
        }
    }
}
