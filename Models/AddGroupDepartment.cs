using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{

    public class AddGroup
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int MenuID { get; set; }
        public string Menu { get; set; }
        public int SubmenuId { get; set; }
        public string Submenu { get; set; }
        public string Status { get; set; }
        public string PartyId { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Type { get; set; }
    }
    public class AddDepartment
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string PartyId { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Type { get; set; }
    }

}