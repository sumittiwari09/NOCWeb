using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public partial class DocumentLibraryMaster
    {
        public int? Id { get; set; }
        public string DocumentName { get; set; }
        public int? isMandatory { get; set; }
        public int? isActive { get; set; }
        public string CompanyID { get; set; }
        public int DocumentID { get; set; }
        public int isSelected { get; set; }

    }
}