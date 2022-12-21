using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class FinancialDocumentBO : ErrorBO
    {

        public string TrusteeInfoId { get; set; }
        public string ApplicantId { get; set; }
        public string Name { get; set; }

        public string Affidavit { get; set; }
        public string AffidavitExtension { get; set; }
        public string AffidavitContentType { get; set; }

        public string Salary { get; set; }
        public string SalaryExtension { get; set; }
        public string SalaryContentType { get; set; }

        public string Bank { get; set; }
        public string BankExtension { get; set; }
        public string BankContentType { get; set; }

        public string Annexure { get; set; }
        public string AnnexureExtension { get; set; }
        public string AnnexureContentType { get; set; }

        public string ESI { get; set; }
        public string ESIExtension { get; set; }
        public string ESIContentType { get; set; }

        public string IsConcerned { get; set; }

        public string VCI { get; set; }
        public string VCIExtension { get; set; }
        public string VCIContentType { get; set; }


        public string IsFrame { get; set; }

        public string Frame { get; set; }
        public string FrameExtension { get; set; }
        public string FrameContentType { get; set; }

        public string IsAdmission { get; set; }

        public string Admission { get; set; }
        public string AdmissionExtension { get; set; }
        public string AdmissionContentType { get; set; }

        public string IsSufficient { get; set; }

        public string Land { get; set; }
        public string LandExtension { get; set; }
        public string LandContentType { get; set; }

        public string IsAffidavit { get; set; }

        public string SubmitAffidavit { get; set; }
        public string SubmitAffidavitExtension { get; set; }
        public string SubmitAffidavitContentType { get; set; }

        public string IsOther { get; set; }

        public string Other { get; set; }
        public string OtherExtension { get; set; }
        public string OtherContentType { get; set; }








    }
}