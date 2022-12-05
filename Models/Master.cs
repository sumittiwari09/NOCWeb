using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class Master
    {
    }
    public class NOCDEPMAP
    {
        public int iPk_DeptMapId { get; set; }
        public int? iFk_DeptId { get; set; }
        public string iFk_NOCDeptId { get; set; }
        public string iFk_NOCTyp { get; set; }
        public int? iStts { get; set; }
        public int? iMode { get; set; }
        public string NocDepartmentName { get; set; }
        public string NocDepartmenttype { get; set; }
        public string DepartName { get; set; }
        public string Type
        {
            get
            {
                switch (iMode)
                {
                    case 1:
                        return "Temporary";
                    case 2:
                        return "Permanent";
                    case 3:
                        return "Both";
                  
                    default:
                        return "";
                }
            }
        }

    }

    public class PARAMCAT
    {
        public int? iPk_ParmCateId { get; set; }
        public string sParmetNam { get; set; }
        public string iParCatId { get; set; }
        public string iParCatSubId { get; set; }
        public string iParUomid { get; set; }
        public int? iStts { get; set; }
        public string sParCatName { get; set; }
        public string sParCatSubName { get; set; }
        public string sParUomName { get; set; }
        public int? iFk_Deptid { get; set; }
        public string sDeptName { get; set; }
    }

}