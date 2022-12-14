﻿using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace NewZapures_V2.Models
{
    public class LandDetailsBO
    {
        //public int iFk_AplcnId { get;set; }
        //public int iFK_ClgId { get;set; }
        //public string iFK_TypeConvert { get;set; }
        //public BinaryConverter bLndConvert { get;set; }
        //public string sOwnrName { get;set; }
        //public string LandArea { get;set; }
        //public string sOwnerbuildNo { get;set; }
        //public string dtOrderDt { get;set; }
        //public string sLandDetails { get;set; }
        //public string sLndNo { get;set; }
        //public BinaryConverter IsAnyOtherInfo { get;set; }

    }

    public class LandInfoBO
    {
        public string CollageId { get; set; }
        public string IsLandConvert { get; set; }
        public string OwnerName { get; set; }
        public string OwnerBuildNo { get; set; }
        public string OrderDate { get; set; }
        public string LandDetail { get; set; }
        public string LandNo { get; set; }
        public string IsAnyOtherInfo { get; set; }
        public string LandAreaProof { get; set; }
        public string OwnBuildingProof { get; set; }
        public string LandConvertProof { get; set; }
        public string LandAreaProofExtension { get; set; }
        public string LandAreaProofDocumentContent { get; set; }
        public string OwnBuildingProofExtension { get; set; }
        public string OwnBuildingProofDocumentContent { get; set; }
        public string LandConvertProofExtension { get; set; }
        public string LandConvertProofDocumentContent { get; set; }
    }
}