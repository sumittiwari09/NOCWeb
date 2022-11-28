using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class Department { }
    public class DepartmentTypeDocumentBO
    {
        public string Name { get; set; }
        public string ID { get; set; }
        public int MstCategoryID { get; set; }
        public string Category { get; set; }
    }
    public class ListDepartmentMasterBOData
    {
        public List<DepartmentTypeDocumentBO> ListRequest { get; set; }
    }
    public class DocumentBO
    {
        public int MstCategoryID { get; set; }
        public string Category { get; set; }
    }
    public class ListDocumentMasterBOData
    {
        public List<DocumentBO> ListRequest { get; set; }
    }
    public class DocumentMasterData : ResponseData
    {
        public ListDocumentMasterBOData Data { get; set; }
    }
    public class DocumentMasterResData : ResponseData
    {
        public List<DocumentBO> ResData { get; set; }
    }
}