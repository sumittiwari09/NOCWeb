using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public partial class FillDropDown
    {
        public int Id { get; set; }
        public string Text { get; set; }
    }
    //public partial class CategoryMaster 
    //{
    //    public int CategoryId { get; set; }
    //    public int? SubCategoryId { get; set; }
    //    public int? IsActive { get; set; }
    //    public string Name { get; set; }
    //    public int? Type { get; set; }
    //    public string RateList { get; set; }
    //    public int? CreateBy { get; set; }
    //    public DateTime? CreateOn { get; set; }
    //    public int? UpdateBy { get; set; }
    //    public DateTime? UpdateOn { get; set; }
    //}

    public class PartialReponse
    {
        public string Total { get; set; }
        public object Data { get; set; }
    }

    //public class Reponse
    //{
    //    public string StatusCode { get; set; }
    //    public string Message { get; set; }
    //    public object Data { get; set; }
    //    public int Total { get; set; }
    //}

    public partial class GlobalClass
    {
    
        public int Id { get; set; }
        public string strId { get; set; }
        public string value { get; set; }
        public string label { get; set; }
        public string graphbackcolor { get; set; }
        public string Text { get; set; }
        public int RecordCount { get; set; }
        public string Pincode { get; set; }
        public string Color { get; set; }
    }
    public class Dropdown
    {
        public  int? fk_SrvTyp { get; set; }
        public string Id { get; set; }
        public string Text { get; set; }
        public string ID1 { get; set; }
        public string PartyId { get; set; }
    }
    //Emitra Pages
    public class CustomList
    {
        public int Id { get; set; }
        public string text { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
    }
    public class CustomListRequest
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string StrId { get; set; }
    }
}