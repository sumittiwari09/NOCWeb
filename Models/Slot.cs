using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewZapures_V2.Models
{
   
    public class Slot
    {
        public int id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public SelectListItem MerritalStatus { get; set; }
        public string From_Date { get; set; }
        public string To_Date { get; set; }
        public string Number { get; set; }
        public string Day { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public int AreaId { get; set; }
        public string Start_Time { get; set; }
        public string End_Time { get; set; }
        public string CreatedByID { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedDate { get; set; }
        public string ModifyByName { get; set; }
        public string ModifyByID { get; set; }
        public string ModifyIP { get; set; }
        public string ModifyDate { get; set; }
        public string CreatedIP { get; set; }
        public int MstSlotID { get; set; }

        public string Creater { get; set; }

        public string District { get; set; }
        public string Area { get; set; }
        public string block { get; set; }
        public string Caldate { get; set; }
        public string Days { get; set; }

        //public class ListslotMasterData
        //{
        //    public List<Slot> Listslot { get; set; }
        //}
        //public class SlotMasterData : ResponseDataBO
        //{
        //    public ListslotMasterData Data { get; set; }
        //}

        public class ListSlotMasterData
        {
            public List<Slot> ListUsers { get; set; }
            public List<Slot> ListSlots { get; set; }
            public List<Slot> ListDays { get; set; }
        }
        public class SlotMasterData : ResponseData
        {
            public ListSlotMasterData Data { get; set; }
        }

        public class SlotMasterResData : ResponseData
        {
            public List<Slot> ResData { get; set; }
        }
        public int daynumber { get; set; }
        public string Date { get; set; }
        public string Dateto { get; set; }

    }
    public class ServiceTypeDocument
    {
        public List<ServiceTypeDocument> ServiceList { get; set; }
        public int MstCategoryID { get; set; }
        public int MstServiceTypeID { get; set; }
        public int MstServiceTypeDocumentID { get; set; }
        public string ServiceName { get; set; }
        public string SubServiceType { get; set; }
        public string ServiceTypeCode { get; set; }
        public string DocumentName { get; set; }
        public string DocumentName_Hindi { get; set; }
        public int Fromdays { get; set; }
        public int Todays { get; set; }
        public string Required { get; set; }
        public int SrNo { get; set; }
        public string CreatedByID { get; set; }
        public string CreatedByName { get; set; }
        public string CreatedIP { get; set; }
        public string CreatedDate { get; set; }
        public string ModifyByName { get; set; }
        public string ModifyByID { get; set; }
        public string ModifyIP { get; set; }
        public string ModifyDate { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }
        public string InputString { get; set; }
        public int ChildSubCategory { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
        public int CollectionID { get; set; }

    }
    public class ListServiceTypeDocumentData
    {
        public List<ServiceTypeDocument> ListRequest { get; set; }
    }
    public class ServiceTypeDocumentData : ResponseData
    {
        public ListServiceTypeDocumentData Data { get; set; }
    }
    public class Mastersddl
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string EsahayakName { get; set; }

        public string RoleLevel { get; set; }

        public string Text { get; set; }
        public string Color { get; set; }
    }
    public class ListMastersddl
    {
        public List<Mastersddl> ListMasters { get; set; }
    }
    public class MastersddlData : ResponseData
    {
        public ListMastersddl Data { get; set; }
    }
}