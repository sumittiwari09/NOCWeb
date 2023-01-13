using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class Master
    {
    }
    public class FinYear
    {
        public int iPk_Id { get; set; }
        public string sName { get; set; }
        public string dtInsertStrDt { get; set; }
        public string dtInsertEndDt { get; set; }
        public int iStts { get; set; }
    }
    public class FinYearView : FinYear
    {
        public DateTime? dtStrdate { get; set; }
        public DateTime? dtEnddate { get; set; }

        public string StartDate
        {
            get
            {
                if (!string.IsNullOrEmpty(dtStrdate.ToString()))
                {
                    return dtStrdate.Value.ToString("ddd dd MMM yyyy");
                }
                else
                {
                    return null;
                }
            }
        }
        public string EndDate
        {
            get
            {
                if (!string.IsNullOrEmpty(dtEnddate.ToString()))
                {
                    return dtEnddate.Value.ToString("ddd dd MMM yyyy");
                }
                else
                {
                    return null;
                }
            }
        }

    }
    public class NOCDEPMAP
    {
        public int iPk_DeptMapId { get; set; }
        public int? iFk_DeptId { get; set; }
        public string iFk_NOCDeptId { get; set; }
        public string iFk_NOCTyp { get; set; }
        public int? iStts { get; set; }
        public int? iMode { get; set; }
        public string NewGuid { get; set; }
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

    public class PARMTVALUCONFMST
    {
           public int iPK_ParValId {get;set;}
           public int iFk_Deptid {get;set;}
           public int iCourseId {get;set;}
           public int iParCatId {get;set;}
           public int iParCatSubId {get;set;}
           public int iParUomid {get;set;}
           public int iMin {get;set;}
           public int iMax {get;set;}
           public int iField {get;set;}
           public int iValue {get;set;}
           public int iminlength {get;set;}
           public int? iFix { get; set; }
           public int iminwidth {get;set;}
           public long iminval {get;set;}
           public int imaxlength  {get;set;}
           public int imaxwidth {get;set;}
           public long imaxval {get;set;}
           public int iStts { get; set; }
    }
    public class PARMTVALUCONFMSTView : PARMTVALUCONFMST
    {
        public string sDeptName { get; set; }
        public string sCateName { get; set; }
        public string sCateSubName { get; set; }
        public string UomName { get; set; }
        public string CourseName { get; set; }
        public string InsertValue { get; set; }
        public string UploadUrl { get; set; }
    }
    public class ArchiMstDetail
    {
        public int ipk_ArchiMstDetId { get; set; }
        public int iParamId { get; set; }
        public int iSubCatId { get; set; }
        public int iUomId { get; set; }
        public int iWid { get; set; }
        public int iLen { get; set; }
        public int iQty { get; set; }
        public string sAppId { get; set; }
        public string bAttachFile { get; set; }
        public string sProfileExtension { get; set; }
        public string ProfileContentType { get; set; }


    }
    public class ArchiMstData
    {
        public int iParamId { get; set; }
        public int iSubCatId { get; set; }
        public int iUomId { get; set; }
    }

    public class ArchitectureMst
    {
         public int iPk_MasterId{ get; set; }
         public string iFK_AppId   { get; set; }
         public int iTrustId    { get; set; }
         public int iCollId     { get; set; }
         public int iParamId    { get; set; }
         public int iSubCatId   { get; set; }
         public string Value       { get; set; }
         public int iUom        { get; set; }


}
    public class ArchUpload
    {
        public int iParamId { get; set; }
        public int iSubCatId { get; set; }
        public int iUomId { get; set; }
        public string sFK_AppId { get; set; }
        public string Type { get; set; }
        public string UploadUrl { get; set; }
    }
    public class EVNTMST
    {
        public int iPk_EventId { get; set; }
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
        public string dtFormdate { get; set; }
        public string dtTodate { get; set; }
        public string sNewGuid { get; set; }
    }
    public class EventMstSave
    {
        public string sNewGuid { get; set; }
        public int Id { get; set; }
        public string dtFormdate { get; set; }
        public string dtTodate { get; set; }
    }
    public class CommiteeMaster
    {
        public int iPk_CommiteeId { get; set; }
        public int iComtTypid { get; set; }
        public string sComtMemLst { get; set; }
        public string sComtNam { get; set; }    
        public int iStts { get; set; }
        public int? iDeptId { get; set; }
        public string sCtrby { get; set; }
        public string CommiteeMember { get; set; }
        public string CommiteeType
        {
            get
            {
                switch (iComtTypid)
                {
                    case 1:
                        return "Inspection Commitee";
                    case 2:
                        return "Noc Approval Commitee";
                   

                    default:
                        return "";
                }
            }
        }
    }
}