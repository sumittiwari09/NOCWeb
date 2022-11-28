using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class UserPermissions
    {
        public int MappedSubmenuId { get; set; }
        public int MenuId { get; set; }
        public string Menu { get; set; }
        public int SubMenuId { get; set; }
        public string SubMenu { get; set; }
        public string Controller { get; set; }
        public string ActionMethod { get; set; }
        public string Allow_Insert { get; set; }
        public string Allow_Edit { get; set; }
        public string Allow_Delete { get; set; }
        public string Allow_View { get; set; }
    }
}