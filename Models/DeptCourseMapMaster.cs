using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public partial class DeptCourseMapMaster
    {

        public int deptId { get; set; }
        public int courseId { get; set; }
    }
    public class DeptCourseMapTableData
    {
        public int pk_MapId { get; set; }
        public int deptId { get; set; }
        public int courseId { get; set; }
        public string Department { get; set; }
        public string Course { get; set; }
    }
}