using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class NotificationMaster
    {
        public int ConfId { get; set; }
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public int SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public string Message { get; set; }
        public int SendtoFlow { get; set; }
        public string FlowName { get; set; }
        public int Event { get; set; }
        public string EventName { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
        public string PartyName { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string UpdateBy { get; set; }
    }

    public class NotificationTransectionUserListRequest
    {
        public string PartyID { get; set; }
        public int FlowDirection { get; set; }
        public int configId { get; set; }
        public string specificUser { get; set; } = null;
    }

    public class NotificationOperationRequest
    {
        public string PartyId { get; set; }
        public int RowID { get; set; }
        public string Type { get; set; }

    }
    
    public class NotificationOperationData
    {
        public int NotificationUserListID { get; set; }
        public string Message { get; set; }
        public int IsRead { get; set; }
        public int IsDelete { get; set; }
        public DateTime? ReadedOn { get; set; }
        public DateTime? DeletedOn { get; set; }

    }




}