using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;
//using static NewZapures_V2.Controllers.ClientController;
using static NewZapures_V2.Models.Common;

namespace NewZapures_V2.Controllers
{
    public class DashboardReportController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        //Admin Dashboard 
        #region Admin Dashboard 
        public ActionResult AdminDashboard()
        {
            //MobileRechargeController mobile = new MobileRechargeController();

            //var balanceLeft = mobile.CheckBalance();
            //var jsonBalance = JsonConvert.SerializeObject(balanceLeft.Data);
            //var objResponse = JsonConvert.DeserializeObject<CheckWalletResponse>(jsonBalance);

            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            //List<NEWANNMST> _lstNewAnnoucobj = new List<NEWANNMST>();
            //_lstNewAnnoucobj = Common.GetNewAndAnnouncement(0, 1);
            //ViewBag.balance = Convert.ToDecimal(objResponse.BALANCE);
            if (userdetailsSession != null)
            {
                var walletAmountList = GetWalletAmount();
                ViewBag.walletAmount = walletAmountList;
                var transactionPendingList = GetDashboardPendingTransactions();
                ViewBag.transactionPendingList = transactionPendingList;
                var bunessmakerList = GetDashboardTopBusinessMakers();
                ViewBag.bunessmakerList = bunessmakerList;

                //List<NotificationOperationRequest> op = new List<NotificationOperationRequest>();
                NotificationOperationRequest op = new NotificationOperationRequest();
                op.Type = "NotificationHistory";
                var notificationMaster = ZapurseCommonlist.NotificationOperation(op);
                ViewBag.notificationMaster = notificationMaster;

                var statewaiseList = GetDashboardStateWiseCollection();

                ViewBag.statewaiseList = statewaiseList;

                var TargetvsActualList = GetDashboardTargetvsActual();
                ViewBag.targetvsActualList = TargetvsActualList;

                var userWiseList = GetDashboardUserwisePartnerCount();
                ViewBag.userWiseList = userWiseList;
                //ViewBag.NewAnnouncement = _lstNewAnnoucobj;

                //var walletRequestList = GetWalletRequestDetails();
                //ViewBag.walletRequestList = walletRequestList;

                if (userdetailsSession != null)
                {

                    #region Self WRHistory
                    WalletRechargeHistory model = new WalletRechargeHistory();
                    model.Type = "MyPendingWalletRechargeHistory";
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/WalletRechargeHistory");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                    //request.AddHeader("Content-Type", "application/json;");
                    //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                    //request.AddHeader("LoginID", CurrentSessions.AtishayVendorID.ToString());
                    request.AddHeader("LoginID", userdetailsSession.PartyId);
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        WalletRequestResponse LoginObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response.Content);
                        if (LoginObj.ResponseCode == "1")
                        {
                            ViewData["WalletRechargeHistory"] = LoginObj.WalletRechargeLst;
                        }
                    }
                    #endregion

                    //ViewData["BankName"] = new SelectList(null, "ListID", "ListName");
                    //ViewData["PayMode"] = new SelectList(null, "ListID", "ListName");
                }
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }

            return View();
        }
        #endregion
        // GET: DashboardReport
        public ActionResult Dashboard()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            List<NEWANNMST> _lstNewAnnoucobj = new List<NEWANNMST>();
            _lstNewAnnoucobj = Common.GetNewAndAnnouncement(0, 1);
            if (userdetailsSession != null)
            {
                var walletAmountList = GetWalletAmount();
                ViewBag.walletAmount = walletAmountList;
                var transactionPendingList = GetDashboardPendingTransactions();
                ViewBag.transactionPendingList = transactionPendingList;
                var bunessmakerList = GetDashboardTopBusinessMakers();
                ViewBag.bunessmakerList = bunessmakerList;

                //List<NotificationOperationRequest> op = new List<NotificationOperationRequest>();
                NotificationOperationRequest op = new NotificationOperationRequest();
                op.Type = "NotificationHistory";
                var notificationMaster = ZapurseCommonlist.NotificationOperation(op);
                ViewBag.notificationMaster = notificationMaster;

                var statewaiseList = GetDashboardStateWiseCollection();

                ViewBag.statewaiseList = statewaiseList;

                var TargetvsActualList = GetDashboardTargetvsActual();
                ViewBag.targetvsActualList = TargetvsActualList;
                
                var userWiseList = GetDashboardUserwisePartnerCount();
                ViewBag.userWiseList = userWiseList;
                ViewBag.NewAnnouncement = _lstNewAnnoucobj;

                var serviceIncomeList = GetDashBoardServiceWiseIncome();
                ViewBag.serviceIncomeList = serviceIncomeList;

                if (userdetailsSession != null)
                {

                    #region Self WRHistory
                    WalletRechargeHistory model = new WalletRechargeHistory();
                    model.Type = "MyPendingWalletRechargeHistory";
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/WalletRechargeHistory");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                    //request.AddHeader("Content-Type", "application/json;");
                    //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                    //request.AddHeader("LoginID", CurrentSessions.AtishayVendorID.ToString());
                    request.AddHeader("LoginID", userdetailsSession.PartyId);
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        WalletRequestResponse LoginObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response.Content);
                        if (LoginObj.ResponseCode == "1")
                        {
                            ViewData["WalletRechargeHistory"] = LoginObj.WalletRechargeLst;
                        }
                    }
                    #endregion

                    //ViewData["BankName"] = new SelectList(null, "ListID", "ListName");
                    //ViewData["PayMode"] = new SelectList(null, "ListID", "ListName");
                }
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
            return View();
        }

        //Get Wallet Amount Details
        public List<WalletAmtBO> GetWalletAmount()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetWalletBlanceShow?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<WalletAmtBO> amtWltList = new List<WalletAmtBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                amtWltList = JsonConvert.DeserializeObject<List<WalletAmtBO>>(response.Content);

            }
            return amtWltList;
        }

        // Dashboard_Pending_Transactions
        public List<WalletAmtBO> GetDashboardPendingTransactions()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetDashboardPendingTransactions?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<WalletAmtBO> transactionPendingList = new List<WalletAmtBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                transactionPendingList = JsonConvert.DeserializeObject<List<WalletAmtBO>>(response.Content);
            }
            return transactionPendingList;
        }

        // Dashboard Top Business Makers
        public List<WalletAmtBO> GetDashboardTopBusinessMakers()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetDashboardTopBusinessMakers?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<WalletAmtBO> bunessmakerList = new List<WalletAmtBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                bunessmakerList = JsonConvert.DeserializeObject<List<WalletAmtBO>>(response.Content);
            }
            return bunessmakerList;
        }
        // GetDashboardStateWiseCollection
        public List<WalletAmtBO> GetDashboardStateWiseCollection()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetDashboardStateWiseCollection?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<WalletAmtBO> stateWiseList = new List<WalletAmtBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                stateWiseList = JsonConvert.DeserializeObject<List<WalletAmtBO>>(response.Content);

            }
            WalletAmtBO walt = new WalletAmtBO();
            walt.SName = "Sumit";
            walt.TxnAmnt = 200;
            walt.StateName = "Jaipur";
            WalletAmtBO walt1 = new WalletAmtBO();
            walt1.SName = "Abishek";
            walt1.TxnAmnt = 400;
            walt1.StateName = "Bhopal";
            stateWiseList.Add(walt);
            stateWiseList.Add(walt1);
            return stateWiseList;
        }
        //Get Dashboard Targetvs Actual
        public ActionResult GetTargetvsActual()
        {
            var TargetvsActualList = GetDashboardTargetvsActual();
            return View(TargetvsActualList);
        }
        public List<WalletAmtBO> GetDashboardTargetvsActual()
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
        //Usere wise Count
        public List<WalletAmtBO> GetDashboardUserwisePartnerCount()
        
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetDashboardUserwisePartnerCount?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<WalletAmtBO> userwiseList = new List<WalletAmtBO>();            
            if (response.StatusCode.ToString() == "OK")
            {
                userwiseList = JsonConvert.DeserializeObject<List<WalletAmtBO>>(response.Content);
            }
            return userwiseList;
        }
        //DashBoard_ServiceWise_Income
        public List<WalletAmtBO> GetDashBoardServiceWiseIncome()        
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ALLReport/GetDashBoardServiceWiseIncome");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<WalletAmtBO> serviceWiseIncomeList = new List<WalletAmtBO>();            
            if (response.StatusCode.ToString() == "OK")
            {
                serviceWiseIncomeList = JsonConvert.DeserializeObject<List<WalletAmtBO>>(response.Content);
            }
            return serviceWiseIncomeList;
        }
       

    }
}
