using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class CustomMaster
    {
        public int? Id { get; set; }
        public string text { get; set; }
    }
    public class ListCustom : ResponseData
    {
        public List<CustomMaster> ListRequest { get; set; }
    }
}