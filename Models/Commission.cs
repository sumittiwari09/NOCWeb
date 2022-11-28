using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class Serviceprovider
    {
        public int ServiceProviderMasterId { get; set; }
        public int ServiceType { get; set; }
        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public string ValidationUrl { get; set; }
        public string PaymentUrl { get; set; }
        public int Status { get; set; }
        public string CheckUrl { get; set; }
        public int priority { get; set; }
        public int IsWebActive { get; set; }
        public int IsMobileActive { get; set; }
        public string PartyId { get; set; }
        public string ServiceName { get; set; }
    }
    public class ServiceProviderRateconfiguration
    {
        public int ServiceProviderRateConfigurationId { get; set; }
        public int CommissionType { get; set; }
        public decimal CommissionRate { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }

        public int ServiceProviderId { get; set; }
        public string ServiceProviderName { get; set; }
        public int Status { get; set; }
        public int OperaterId { get; set; }
        public string PartyId { get; set; }
        public string OperaterName { get; set; }
        public string TypeName { get; set; }
        public int? CommissionApplicableId { get; set; }
    }
    public class tbl_CommissionMaster
    {
        public int CommissionMasterId { get; set; }
        public int ServiceTypeId { get; set; }
        public int ChargeType { get; set; }
        public int Commissionrang { get; set; }
        public int CustomerFeeType { get; set; }
        public int IsCustomerFeesApplicable { get; set; }
        public Nullable<decimal> CustomerFeeValue { get; set; }

        public Nullable<decimal> ChargeValue { get; set; }
        public int MinMaxApplicableOn { get; set; }
        public int MinMaxValue { get; set; }
        public Nullable<decimal> MinMaxApplicablevalue { get; set; }
        public string PartyId { get; set; }
        public int UserType { get; set; }
        public string UserName { get; set; }
        public string ServiceName { get; set; }
        public string IsdefaultPartyId { get; set; }
        public int Isdefault { get; set; }
        public string IsdefaultPartyName { get; set; }
        public int Status { get; set; }

    }

    public class UserCommission
    {
        public int UserCommissionId { get; set; }
        public int UserType { get; set; }
        public int ServiceId { get; set; }
        public int TransactionAmountFrom { get; set; }
        public int TransactionAmountTo { get; set; }
        public int CommissionPercentage { get; set; }
        public Nullable<decimal> CommissionAmount { get; set; }
        public string PartyId { get; set; }
        public int OperatorId { get; set; }
        public int CommissionMasterId { get; set; }
        public int Status { get; set; }
        public int ServiceProviderId { get; set; }
        public string OperatorName { get; set; }
        public string ServiceProviderName { get; set; }
        public int ChargeType { get; set; }
        public string DefaultUserPartyId { get; set; }
    }

    public class SLBMST
    {
        public int? iPk_SlbMst { get; set; }
        public Nullable<int> iTxnAmtFm { get; set; }
        public Nullable<int> iTxnAmtTo { get; set; }
        public int iSts { get; set; }
        public string dtCrtDt { get; set; }
        public bool bRngSet { get; set; }
        public int iSerId { get; set; }
        public string sPatyCode { get; set; }
        public int bRng { get; set; }
    }

    public class NEWANNMST
    {
        public int? iPK_NewId { get; set; }
        public int iSts { get; set; }
        public string dtCrtDt { get; set; }
        public string dtSrtDt { get; set; }
        public string dtEndDt { get; set; }
        public int iNwsAnnoType { get; set; }
        public string sPatyCode { get; set; }

        public string sMsg { get; set; }
        public string sSubject { get; set; }
        public string dFrmTime { get; set; }

        public string dToTime { get; set; }
    }
}