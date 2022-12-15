using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class Partial
    {
        public class Testing
        {
            public int CategoryId { get; set; }
            public string ClassName { get; set; }
            public string ChargeType { get; set; }
            public string PaymentName { get; set; }
            public string Unitname { get; set; }
            public string Amount { get; set; }
            public string CategoryName { get; set; }
        }
        public class ListTesting : ResponseData
        {
            public List<Testing> ListRequest { get; set; }
        }
        public enum RecordStatus
        {
            Active = 1,
            Deactive = 0,
            Deleted = 2,
            TypeDocument=3,
            Clone = 12,
            Draft = 13
        }

        public enum TypeDocument
        {
            ChargeType = 1,
            HardwareType = 2,
            DocumentType = 3,
            UnitType = 4,
            MonthType = 5,
            VerificationType = 6,
            UserType = 7,
            TaxType = 10,
            CommissionType = 19,
            OperaterName = 20,
            ServiceProvider = 21,
            UOM = 34
                
        }

        public class ResponseDataBO
        {
            public string ResponseCode { get; set; }
            public int statusCode { get; set; }
            public string Message { get; set; }
            public string ID { get; set; }
            // public JObject Data { get; set; }
            public string Body { get; set; }
        }
        public class Activeclass
        {
            public string Tablename { get; set; }
            public int Id { get; set; }
            public int status { get; set; }
        }
        public class Permissionclass
        {
            public string Type { get; set; }
            public int MappingId { get; set; }
            public int MstGroupId { get; set; }
            public int PermissionId { get; set; }
            public int status { get; set; }
            public string PartyId { get; set; }
        }
        public partial class Documentlist
        {
            public string UploadDocumentUrl { get; set; }
            public int DocumentId { get; set; }
            public string DocumentName { get; set; }
            public int DocumentStatus { get; set; }

        }
        public class RegistratedUser
        {
            public string UserType { get; set; }
            public string PartyId { get; set; }
            public string RegistrationNo { get; set; }
            public string UserName { get; set; }
            public string FirstName { get; set; }
            public string MobileNumber { get; set; }
            public string Servicecollection { get; set; }
            public string Hardwarecollection { get; set; }
            public int IsActive { get; set; }
            public string PaymentStatus { get; set; }
            public string EncryPartyId { get; set; }
        }
        public class ACtivedServicesHardwarelist
        {
            public string OrderId { get; set; }
            public string PartyId { get; set; }
            public string Servicecollection { get; set; }
            public string Hardwarecollection { get; set; }
            public string OrderStatus { get; set; }
        }
        public class setting
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public Nullable<int> IsActive { get; set; }
        }
        public class ListSetting : ResponseData
        {
            public List<setting> ListRequest { get; set; }
        }
        public class CustomEnum
        {
            public int CustomEnumId { get; set; }
            public int? EnumNo { get; set; }
            public string Name { get; set; }
            public string CreatedBy { get; set; }
            public DateTime? CreatedOn { get; set; }
            public string UpdatedBy { get; set; }
            public DateTime? UpdatedOn { get; set; }
            public string PartyId { get; set; }
            public int? Id { get; set; }
            public int? IsActive { get; set; }
        }
        public class ListCustomEnum : ResponseData
        {
            public List<CustomEnum> ListRequest { get; set; }
        }
        public partial class Commisssiondata
        {
            public string TableName { get; set; }
            public int CommissionMasterId { get; set; }
            public int UserType { get; set; }
            public int ServiceTypeId { get; set; }
            public int Isdefault { get; set; }
            public string PartyId { get; set; }
            public string IsdefaultPartyId { get; set; }
        }
        public static List<GlobalClass> TypeName()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();
            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "White Label"
            });
            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Stockist"
            });
            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Distributer"
            });
            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Retailer"
            });

            return Lst;
        }

        public partial class ApesSetCommission
        {
            public string TransactionAmountFrom { get; set; }
            public string TransactionAmountTo { get; set; }
            public string Retailer { get; set; }
            public string Distributer { get; set; }
            public string Stockist { get; set; }
            public string Whitelabel { get; set; }
            public string Retailer_Commission { get; set; }
            public string Distributer_Commission { get; set; }
            public string Stockist_Commission { get; set; }
            public string Whitelabel_Commission { get; set; }
        }


    }
}