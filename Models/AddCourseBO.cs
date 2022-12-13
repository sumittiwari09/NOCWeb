using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class AddCourseBO
    {
        public int? iPk_AddCourseId { get; set; }
        public string TrustName { get; set; }
        public string TrustInfoId { get; set; }
        public string CollegeName { get; set; }
        public string iCollegeId { get; set; }
        public string TagDegrees { get; set; }
        public string TagCourse { get; set; }
    }
    public class ListAddCourseBOMaster : ResponseData
    {
        public List<AddCourseBO> ListRequest { get; set; }
    }
}