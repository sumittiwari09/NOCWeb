using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using static NewZapures_V2.Models.ZapurseCommonlist;

namespace NewZapures_V2.Models.Others
{
    public class Modal
    {
        public string ID { get; set; }
        public string AreaLabeledId { get; set; }
        public ModalSize Size { get; set; }
        public string Message { get; set; }
        public decimal DeletionKey { get; set; }
        public string DeletionKeys { get; set; }
        public string SelectAll { get; set; }
        [DisplayName("Remarks")]
        public string REMARKS { get; set; }
        [DisplayName("Notification ID")]
        public string Notification { get; set; }
        public bool IsPendency { get; set; }
        public bool IsShowRemarks { get; set; }


        public string ModalSizeClass
        {
            get
            {
                switch (this.Size)
                {
                    case ModalSize.Small:
                        return "modal-sm";
                    case ModalSize.Large:
                        return " modal-lg";
                    case ModalSize.Medium:
                    default:
                        return "";
                }
            }
        }

        public ModalHeader Header { get; set; }
        public ModalFooter Footer { get; set; }
        public Modal()
        {
            this.IsShowRemarks = true;
        }
    }

    public class ModalHeader
    {
        public string Heading { get; set; }
    }

    public class ModalFooter
    {
        public ModalFooter()
        {
            SubmitButtonText = "Save";//"Save";
            CancelButtonText = "Cancel";//"Cancel";
            //SubmitButtonText = "Save";
            //CancelButtonText = "Cancel";
            SubmitButtonID = "btn-submit";
            CancelButtonID = "btn-cancel";
            SubmitButtonCss = "btn btn-success grid-btn";
            OnlyCancelButton = false;

        }

        public string SubmitButtonText { get; set; }
        public string CancelButtonText { get; set; }
        public string SubmitButtonID { get; set; }
        public string CancelButtonID { get; set; }
        public bool OnlyCancelButton { get; set; }
        public string SubmitButtonCss { get; set; }
    }
}