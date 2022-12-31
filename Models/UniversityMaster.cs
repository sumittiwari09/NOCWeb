using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class UniversityMaster
    {
        public string Type_id { get; set; }
        public string UniName { get; set; }
        public string Website { get; set; }
        public string Document { get; set; }
        public string Financier_id { get; set; }
        public string Created_id { get; set; }
        public List<ContactDetails> ContactDetails { get; set; }
        public string IsActive { get; set; }
        public string Documentextension { get; set; }
        public string DocumentContentType { get; set; }
    }
}