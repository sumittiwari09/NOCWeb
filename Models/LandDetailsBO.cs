using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace NewZapures_V2.Models
{
    public class LandDetailsBO
    {
        public int iFk_AplcnId { get;set; }
        public int iFK_ClgId { get;set; }
        public string iFK_TypeConvert { get;set; }
        public BinaryConverter bLndConvert { get;set; }
        public string sOwnrName { get;set; }
        public string LandArea { get;set; }
        public string sOwnerbuildNo { get;set; }
        public string dtOrderDt { get;set; }
        public string sLandDetails { get;set; }
        public string sLndNo { get;set; }
        public BinaryConverter IsAnyOtherInfo { get;set; }

    }
}