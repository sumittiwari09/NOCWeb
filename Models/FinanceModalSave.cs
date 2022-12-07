using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class FinanceModalSave
    {
        public string type { get; set; }
        public List<FinanceCapital> financeCapitals { get; set; }
        public List<FinanceIncome> financeIncomes { get; set; }
        public List<FinanceProject> financeProjects { get; set; }
        public decimal ProjectExpectedCost { get; set; }
    }
    public class FinanceCapital
    {
        public int PK_FinCapId { get; set; }
        public int iFk_AplcnId { get; set; }
        public string sFirstName { get; set; }
        public decimal dAproxWdth { get; set; }
        public string sRemark { get; set; }
    }
    public class FinanceIncome
    {
        public int PK_FinCapId { get; set; }
        public int iFk_AplcnId { get; set; }
        public string sName { get; set; }
        public decimal dAproxIncome { get; set; }
        public string sTimeFrame { get; set; }
        public int iTyp { get; set; }
    }
    public class FinanceProject
    {
        public int PK_FinProjId { get; set; }
        public int iFk_AplcnId { get; set; }
        public string sName { get; set; }
        public decimal dAproxFund { get; set; }
        public string sRemark { get; set; }
        public int iTyp { get; set; }
    }

}