using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class UploadReceipt
    {
        public string AppGUID { get; set; }
        public string UploadReceiptDocument { get; set; }
        public string UploadReceiptDocumentExtension { get; set; }
        public string UploadReceiptDocumentContent { get; set; }
    }
}