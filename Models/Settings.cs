using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class Settings
    {
        public int Id { get; set; }
        public string SettingName { get; set; }
        public int IsActive { get; set; }
        public string Type { get; set; }
    }
}