using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class SlotMaster
    {
        public string SlotMasterId { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public int AreaId { get; set; }
        public string Fromdate { get; set; }
        public string Todate { get; set; }
        public string ActivateSlot { get; set; }
        public string CreatedByID { get; set; }
        public string CreatedByName { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string AreaName { get; set; }
        public string Freedata { get; set; }
        public int IsActive { get; set; }
    }
    public class ListSlotMaster
    {
        public List<SlotMaster> ListRequest { get; set; }
    }
    public class ListSlotMasterData : ResponseData
    {
        public ListSlotMaster Data { get; set; }
    }
    public partial class SlotDropdown
    {
        public static List<GlobalClass> GetSlotList()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "6 AM to 9 AM",
                Color = "#2ebaee"
            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "9 AM to 12 PM",
                Color = "#f05123"
            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "12 PM to 3 PM",
                Color = "#1b5732"
            });

            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "3 PM to 6 PM",
                Color = "#d50000"
            });
            Lst.Add(new GlobalClass
            {
                Id = 5,
                Text = "6 PM to 9 PM",
                Color = "#d50000"
            });


            return Lst;

        }
    }

    public class SlotEnquiryMasterTemp
    {
        public string SlotMasterId { get; set; }
        public long ColourId { get; set; }
        public int NoofEnquiry { get; set; }
        public int Id { get; set; }
        public string ColorName { get; set; }
    }
    public class ListSlotEnquiryMasterTemp
    {
        public List<SlotEnquiryMasterTemp> ListRequest { get; set; }
    }
    public class ListSlotEnquiryMasterTempData : ResponseData
    {
        public ListSlotEnquiryMasterTemp Data { get; set; }
    }

    public class FreezeSlot
    {
        public long SlotFreezeDateId { get; set; }

        public string SlotMasterId { get; set; }
        public string FreezeDate { get; set; }
        public string Remark { get; set; }
        public string CreateById { get; set; }
        public string CreatedByName { get; set; }
    }
    public class ListFreezeSlot
    {
        public List<FreezeSlot> ListRequest { get; set; }
    }
    public class ListFreezeSlotData : ResponseData
    {
        public ListFreezeSlot Data { get; set; }
    }
}