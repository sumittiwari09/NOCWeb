using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewZapures_V2.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        ResponseData objResponse;
        public ActionResult ServiceWiseTrans(int fk_SrvTyp=5)
        {
            var serviceDetailsList = GetServicesAllDetails();
            ViewBag.serviceDetailsList = serviceDetailsList;
            List<ServiceTransationBO> transactionList = new List<ServiceTransationBO>();
            transactionList = GetServiceTransactionDetails(new ServiceTransationBO { fk_SrvTyp = fk_SrvTyp });
            ViewBag.fk_SrvTyp = fk_SrvTyp.ToString();
            return View(transactionList);        
        }
        public static List<Dropdown> GetServicesAllDetails()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCategoryAllInformation?Type=Service");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<GetservicesetailsAndroidNew> serviceDetails = new List<GetservicesetailsAndroidNew>();
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    serviceDetails = JsonConvert.DeserializeObject<List<GetservicesetailsAndroidNew>>(objResponse.Data.ToString());
            }

            var serviceList = serviceDetails.Select(s => new Dropdown
            {
                Id = s.CategoryId,
                Text = s.CategoryName
            }).ToList();

            return serviceList;
        }
        public List<ServiceTransationBO> GetServiceTransactionDetails(ServiceTransationBO service)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetServicesTransation?PartyId=" + PartyId + "&fk_SrvTyp=" + service.fk_SrvTyp);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<ServiceTransationBO> contents = new List<ServiceTransationBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                contents = JsonConvert.DeserializeObject<List<ServiceTransationBO>>(response.Content);
               
            }
            return contents;
        }

        //Get Details USP_USerwise_Partner_Count
        public ActionResult SerwisePartnerCount()
        {
            var serwisePartnerList = GetSerwisePartnerCountDetails();
            return View(serwisePartnerList);
        }

        public List<ServiceWisePartCountBO> GetSerwisePartnerCountDetails()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetSerwisePartnerCount?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<ServiceWisePartCountBO> serviceCount = new List<ServiceWisePartCountBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                serviceCount = JsonConvert.DeserializeObject<List<ServiceWisePartCountBO>>(response.Content);

            }
            return serviceCount;
        }

        /// <summary>
        /// USP_Partwise_View_Statement
        /// </summary>
        /// <returns></returns>

        public ActionResult PartWiseStaDetails()
        {
            var serwiseStatementPartnerList = PartWiseStaDetailsSave();
            return View(serwiseStatementPartnerList);
        }
        public List<PartwiseStatementBO> PartWiseStaDetailsSave()
        {                  
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetPartwiseViewStatement?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<PartwiseStatementBO> satementCount = new List<PartwiseStatementBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                satementCount = JsonConvert.DeserializeObject<List<PartwiseStatementBO>>(response.Content);

            }
            return satementCount;
        }

        /// <summary>
        /// USP_User_Profile_Report
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult UserProfileReport()
        {
            var UserProfileReportList = UserProfileReportSave();
            return View(UserProfileReportList);
        }
        public List<UserProfileDetailsBO> UserProfileReportSave()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetUserProfileDetails?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<UserProfileDetailsBO> profileCount = new List<UserProfileDetailsBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                profileCount = JsonConvert.DeserializeObject<List<UserProfileDetailsBO>>(response.Content);

            }
            return profileCount;
        }

       
        /// <summary>
        ///User Party Wise Profile Report
        /// </summary>
        /// <returns></returns>
        /// 

        public ActionResult UserPartyWiseProfileReport()
        {
            var UserProfileReportList = UserPartyWiseProfileReportSave();
            return View(UserProfileReportList);
        }
        public List<UserPartywiseProfileBO> UserPartyWiseProfileReportSave()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetUserPartywiseProfile?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<UserPartywiseProfileBO> partyProfileCount = new List<UserPartywiseProfileBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                partyProfileCount = JsonConvert.DeserializeObject<List<UserPartywiseProfileBO>>(response.Content);
            }
            return partyProfileCount;
        }

        ///<summary>
        ///Target vs actual
        //////</summary>

        //Get Details USP_USerwise_Partner_Count
        public ActionResult GetTargetvsActual()
        {
            var TargetvsActualList = GetDetailsTargetvsActual();
            return View(TargetvsActualList);
        }

        public List<WalletAmtBO> GetDetailsTargetvsActual()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetDashboardTargetvsActual");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<WalletAmtBO> TargetvsActualList = new List<WalletAmtBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                TargetvsActualList = JsonConvert.DeserializeObject<List<WalletAmtBO>>(response.Content);
            }
            return TargetvsActualList;
        }

    }

}