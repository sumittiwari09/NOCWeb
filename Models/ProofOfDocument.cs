using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewZapures_V2.Models
{
    public class ProofOfDocument
    {
        public string iFk_TrstId { get; set; }
        public string iFk_ClgId { get; set; }
        public string sSSOID { get; set; }
        public string sCrtdBy { get; set; }
        public string dtCrtdDt { get; set; }
        public string sUpdtBy { get; set; }
        public string dtUpdDt { get; set; }
        //certifiedcopy
        public string certifiedcopy { get; set; }
        public string certifiedcopyExtension { get; set; }
        public string certifiedcopyContenttype { get; set; }
        //AimsandObjective
        public string AimsandObjective { get; set; }
        public string AimsandObjectiveExtension { get; set; }
        public string AimsandObjectiveContenttype { get; set; }
        //ProjectmapandRoad
        public string ProjectmapandRoad { get; set; }
        public string ProjectmapandRoadExtension { get; set; }
        public string ProjectmapandRoadContenttype { get; set; }
        //ProofofOwnership
        public string ProofofOwnership { get; set; }
        public string ProofofOwnershipExtension { get; set; }
        public string ProofofOwnershipContenttype { get; set; }
        //Authorizationletters
        public string Authorizationletters { get; set; }
        public string AuthorizationlettersExtension { get; set; }
        public string AuthorizationlettersContenttype { get; set; }
        //ProjectReport
        public string ProjectReport { get; set; }
        public string ProjectReportExtension { get; set; }
        public string ProjectReportContenttype { get; set; }
        //BalanceSheet
        public string BalanceSheet { get; set; }
        public string BalanceSheetExtension { get; set; }
        public string BalanceSheetContenttype { get; set; }
        //ESI
        public string ESI { get; set; }
        public string ESIExtension { get; set; }
        public string ESIContenttype { get; set; }
    }
}
