using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class RateMaster
    {
        public long Id { get; set; }
        public int RateMasterId { get; set; }
        public int? HardWareServicesId { get; set; }
        public int? ChargeType { get; set; }
        public int? UnitType { get; set; }
        public int? TypeId { get; set; }
        public int? PaymentType { get; set; }
        public long? Amount { get; set; }
        public int? IsActive { get; set; }
        public string Createdby { get; set; }
        public DateTime? Createdon { get; set; }

        //emitra based Page Model
        public long MstCategoryId { get; set; }
        public long SubMstCategoryId { get; set; }
        public long ChildCateId { get; set; }
        public int DepartmentCharge { get; set; }
        public int EmitraCharge { get; set; }
        public int PrintingCharge { get; set; }
        public int HomeCharge { get; set; }
        public string DepartmentName { get; set; }
        public string ServiceName { get; set; }
        public string ChildServiceName { get; set; }
        public string CollectionTime { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public string Total { get; set; }
        public string CollectionTotal { get; set; }
        public string DeliveryTotal { get; set; }
    }
    public class ShowRateMaster
    {
        public int RateMasterId { get; set; }
        public string ServiceName { get; set; }
        public string Name { get; set; }
        public string ChargeType { get; set; }
        public string UnitType { get; set; }
        public string PaymentType { get; set; }
        public string Amount { get; set; }
        public int Isactive { get; set; }
    }
    public class ListShowRateMaster : ResponseData
    {
        public List<ShowRateMaster> ListRequest { get; set; }
    }

    //Emitra Based pages model
    public class CommonResp
    {
        public string CreatedIP { get; set; }
        public string CreatedByID { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public string ModifyByName { get; set; }
        public string ModifyByID { get; set; }
        public string ModifyIP { get; set; }
        public string ModifyDate { get; set; }
    }
    public class ListRateMaster
    {
        public List<RateMaster> ListRequest { get; set; }
    }
    public class RateMasterData : ResponseData
    {
        public ListRateMaster Data { get; set; }
    }
    public class RateSpecialCategory
    {

        public int SpecialCategoryid { get; set; }
        public int RateMasterId { get; set; }
        public int SpecialMasterid { get; set; }
        public int DepartmentCharge { get; set; }
        public int EmitraCharge { get; set; }
        public int IsActive { get; set; }

    }
    public class ListRateSpecialCategory
    {
        public List<RateSpecialCategory> ListRequest { get; set; }
    }
    public class RateSpecialCategoryData : ResponseData
    {
        public ListRateSpecialCategory Data { get; set; }
    }
    public class CollectionDropdown
    {
        public static List<GlobalClass> GetStarterList()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Department Charge",
                Color = "#2ebaee"
            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Emitra Charge",
                Color = "#f05123"
            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Printing Charge",
                Color = "#1b5732"
            });

            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Home Charge",
                Color = "#d50000"
            });

            return Lst;

        }

        public static List<GlobalClass> GetSpecialCategoryList()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Male",
                Color = "#2ebaee"
            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Female",
                Color = "#f05123"
            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Disable",
                Color = "#1b5732"
            });

            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Widow",
                Color = "#d50000"
            });

            return Lst;
        }
    }

}