using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Helper
{
    public class Recharge_Helper
    {
    }

    #region Mobile Operator Check

    public class MobileOperator
    {
        public int status { get; set; }
        public string Operator { get; set; }
    }

    public class MobileOperatorResponse
    {
        public string tel { get; set; }
        public MobileOperator records { get; set; }
    }


    #endregion

    #region Mobile Recharge_ROffer
    public class ROffers
    {
        public string rs { get; set; }
        public string desc { get; set; }
    }
    public class ROfferr_Response
    {
        public string tel { get; set; }
        public string @operator { get; set; }
        public List<ROffers> records { get; set; }
        public int status { get; set; }
    }

    #endregion

    #region MobileRecharge Simple


    public class _3G4G
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class COMBO
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class FRC
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class FULLTT
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class RATECUTTER
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }

    public class SimpalPlans
    {
        public List<FULLTT> FULLTT { get; set; }
        public List<TOPUP> TOPUP { get; set; }

        [JsonProperty("3G/4G")]
        public List<_3G4G> _3G4G { get; set; }

        [JsonProperty("RATE CUTTER")]
        public List<RATECUTTER> RATECUTTER { get; set; }
        public List<SM> SMS { get; set; }
        public List<Romaing> Romaing { get; set; }
        public List<COMBO> COMBO { get; set; }
        public List<FRC> FRC { get; set; }
    }

    public class Romaing
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }
    public class SM
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }
    public class TOPUP
    {
        public string rs { get; set; }
        public string desc { get; set; }
        public string validity { get; set; }
        public string last_update { get; set; }
    }
    public class SimpalPlansResponse
    {
        public SimpalPlans records { get; set; }
        public int status { get; set; }
    }


    #endregion

    #region DTH Customer Info

    public class DTHCustomerInfo
    {
        public object MonthlyRecharge { get; set; }
        public string Balance { get; set; }
        public string customerName { get; set; }
        public object status { get; set; }
        public string NextRechargeDate { get; set; }
        public string planname { get; set; }
    }

    public class DTHCustomerInfoResponse
    {
        public string tel { get; set; }
        public string @operator { get; set; }
        public List<DTHCustomerInfo> records { get; set; }
        public int status { get; set; }
    }


    #endregion

    #region DTH Plans Without Channel
    public class AddOnPackWOC
    {
        public DTHWOCRs rs { get; set; }
        public string desc { get; set; }
        public string plan_name { get; set; }
        public string last_update { get; set; }
    }
    public class DTHPlanWOC
    {
        public DTHWOCRs rs { get; set; }
        public string desc { get; set; }
        public string plan_name { get; set; }
        public string last_update { get; set; }
    }
    public class DTHPlanWOCData
    {
        public List<DTHPlanWOC> Plan { get; set; }

        [JsonProperty("Add-On Pack")]
        public List<AddOnPackWOC> AddOnPack { get; set; }
    }
    public class DTHPlanWOCResponse
    {
        public DTHPlanWOCData records { get; set; }
        public int status { get; set; }
    }
    public class DTHWOCRs
    {
        [JsonProperty("1 MONTHS")]
        public string _1MONTHS { get; set; }

        [JsonProperty("3 MONTHS")]
        public string _3MONTHS { get; set; }

        [JsonProperty("6 MONTHS")]
        public string _6MONTHS { get; set; }

        [JsonProperty("1 YEAR")]
        public string _1YEAR { get; set; }
    }
    #endregion

    #region DTH Plans With Channel
    public class AddOnPackWC
    {
        public DTHWCRs rs { get; set; }
        public string desc { get; set; }
        public string plan_name { get; set; }
        public DTHWCChannel channel { get; set; }
        public string last_update { get; set; }
    }

    public class DTHWCChannel
    {
        [JsonProperty("HINDI ENTERTAINMENT")]
        public List<string> HINDIENTERTAINMENT { get; set; }
        public List<string> DOORDARSHAN { get; set; }
        public List<string> SHOPPING { get; set; }

        [JsonProperty("HINDI MOVIES")]
        public List<string> HINDIMOVIES { get; set; }

        [JsonProperty("HINDI NEWS")]
        public List<string> HINDINEWS { get; set; }

        [JsonProperty("ENGLISH NEWS")]
        public List<string> ENGLISHNEWS { get; set; }
        public List<string> SPORTS { get; set; }
        public List<string> KNOWLEDGE { get; set; }
        public List<string> LIFESTYLE { get; set; }
        public List<string> MUSIC { get; set; }
        public List<string> KIDS { get; set; }
        public List<string> GUJARATI { get; set; }
        public List<string> DEVOTIONAL { get; set; }
        public List<string> BENGALI { get; set; }
        public List<string> ORIYA { get; set; }
        public List<string> PUNJABI { get; set; }
        public List<string> MARATHI { get; set; }
        public List<string> TAMIL { get; set; }
        public List<string> TELUGU { get; set; }
        public List<string> KANNADA { get; set; }
        public List<string> MALAYALAM { get; set; }

        [JsonProperty("OTHER REGIONAL")]
        public List<string> OTHERREGIONAL { get; set; }

        [JsonProperty("ENGLISH ENTERTAINMENT")]
        public List<string> ENGLISHENTERTAINMENT { get; set; }

        [JsonProperty("ENGLISH MOVIES")]
        public List<string> ENGLISHMOVIES { get; set; }
    }

    public class DTHWCPlan
    {
        public DTHWCRs rs { get; set; }
        public string desc { get; set; }
        public string plan_name { get; set; }
        public DTHWCChannel channel { get; set; }
        public string last_update { get; set; }
    }

    public class DTHWCData
    {
        public List<DTHWCPlan> Plan { get; set; }

        [JsonProperty("Add-On Pack")]
        public List<AddOnPackWC> AddOnPack { get; set; }
    }

    public class DTHWCResponse
    {
        public DTHWCData records { get; set; }
        public int status { get; set; }
    }

    public class DTHWCRs
    {
        [JsonProperty("1 MONTHS")]
        public string _1MONTHS { get; set; }

        [JsonProperty("3 MONTHS")]
        public string _3MONTHS { get; set; }

        [JsonProperty("6 MONTHS")]
        public string _6MONTHS { get; set; }

        [JsonProperty("1 YEAR")]
        public string _1YEAR { get; set; }
    }



    #endregion

    #region Electricity Customer Info
    public class ElectricityCustomerInfo
    {
        public string CustomerName { get; set; }
        public string Billamount { get; set; }
        public string Billdate { get; set; }
        public string Duedate { get; set; }
    }
    public class ElectricityCustomerInfoResponse
    {
        public string tel { get; set; }
        public string @operator { get; set; }
        public List<ElectricityCustomerInfo> records { get; set; }
        public int status { get; set; }
    }



    #endregion
}