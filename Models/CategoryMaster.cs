using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
   
    public class CategoryMaster
    {
        public int? CategoryId { get; set; }
        public int? IsActive { get; set; }
        public string Name { get; set; }
        public string varificationList { get; set; }
        public string DocumentList { get; set; }
        public string HardwareList { get; set; }
        public string ClassName { get; set; }
    }
    public class ListCategoryMaster: ResponseData
    {
        public List<CategoryMaster> ListRequest { get; set; }
    }
   
}