using BO.Models;
using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    public class MobileRechargeController : Controller
    {
        // GET: MobileRecharge
        public ActionResult Index()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var userAlldetailsSession = (Data)Session["UserAllDetails"];
            var Token = Session["Token"];

            if (userdetailsSession != null)
            {
                var Userwallet = ZapurseCommonlist.GetWalletAmount(userdetailsSession.PartyId);

                ViewBag.wallet = Userwallet;
                ViewBag.partyId = userdetailsSession.PartyId;
                ViewBag.partyType = userdetailsSession.Type;
                if (userdetailsSession.PartyId == "A000001")
                {
                    ViewBag.serviceAvailable = 1;
                }
                else
                {
                    foreach (var item in userAlldetailsSession.serviceName)
                    {
                        if (item != null)
                        {
                            if (item.serviceName.Contains("Mobile Recharge"))
                            {
                                ViewBag.serviceAvailable = 1;
                                break;
                            }
                            else
                            {
                                ViewBag.serviceAvailable = 0;
                            }

                        }

                        else
                        {
                            ViewBag.serviceAvailable = 0;

                        }

                    }
                }

                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        public ActionResult RechargeNew()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var userAlldetailsSession = (Data)Session["UserAllDetails"];
            var Token = Session["Token"];

            if (userdetailsSession != null)
            {
                var Userwallet = ZapurseCommonlist.GetWalletAmount(userdetailsSession.PartyId);

                ViewBag.wallet = Userwallet;
                ViewBag.partyId = userdetailsSession.PartyId;
                ViewBag.partyType = userdetailsSession.Type;
                if (userdetailsSession.PartyId == "A000001")
                {
                    ViewBag.serviceAvailable = 1;
                }
                else
                {
                    foreach (var item in userAlldetailsSession.serviceName)
                    {
                        if (item != null)
                        {
                            if (item.serviceName.Contains("Mobile Recharge"))
                            {
                                ViewBag.serviceAvailable = 1;
                                break;
                            }
                            else
                            {
                                ViewBag.serviceAvailable = 0;
                            }

                        }

                        else
                        {
                            ViewBag.serviceAvailable = 0;

                        }

                    }
                }

                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        public JsonResult GetPlans(string OperatorName, string CircleName)
        {
            if (OperatorName.Contains("BSNL"))
                OperatorName = "BSNL";


            //var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "MobileRecharge/GetMobilePlans?Operator=" + OperatorName+ "&Circle="+CircleName);
            var client = new RestClient("http://101.53.144.83:801/ZapurseMPlan/MobileRecharge/GetMobilePlans?Circle=" + CircleName + "&Operator=" + OperatorName + "");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            SimpalPlansResponse objResponse = new SimpalPlansResponse();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<SimpalPlansResponse>(response.Content);
            }

            objResponse.records.rate_cutter = objResponse.records.RATECUTTER;
            objResponse.records.three_four_g = objResponse.records._3G4G;

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.status, Data = objResponse.records, Failure = false, Message = "Records Fetched...!" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult GetPlansDTH(string OperatorName)
        {

            //var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "MobileRecharge/GetMobilePlans?Operator=" + OperatorName+ "&Circle="+CircleName);
            var client = new RestClient("http://101.53.144.83:801/ZapurseMPlan/MobileRecharge/GetDTHPlan?Operator=" + OperatorName);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            DTHPlanWOCResponse objResponse = new DTHPlanWOCResponse();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<DTHPlanWOCResponse>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.status, Data = objResponse.records, Failure = false, Message = "Records Fetched...!" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult CheckBalance()
        {
            // var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "MobileRecharge/GetWalletbalance");
            var client = new RestClient("http://101.53.144.83:801/ZapurseMPlan/MobileRecharge/GetWalletBalance");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            CheckWalletResponse objResponse = new CheckWalletResponse();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<CheckWalletResponse>(response.Content);
            }

            return new JsonResult
            {
                Data = objResponse,
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult Recharge(string mobileNo, int rechargeAmount, int operatorCode, string latitude, string longitude)
        {
            var json = JsonConvert.SerializeObject(new { MobileNumber = mobileNo, Amount = rechargeAmount, OperatorName = operatorCode });
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "MobileRecharge/RechargeRequest");
            //var client = new RestClient("http://101.53.144.83:801/ZapurseMPlan/MobileRecharge/RechargeRequest?mobileNo=" + mobileNo + "&rechargeAmount=" + rechargeAmount + "&operatorCode=" + operatorCode);
            //var request = new RestRequest(Method.GET);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            RechargeRequestResponse objResponse = new RechargeRequestResponse();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<RechargeRequestResponse>(response.Content);
            }


            if (objResponse.STATUS == 1)
            {
                //var IpAddressCollection = ZapurseCommonlist.GetIPAddress();
                //var userdetailsSession = (UserModelSession)Session["UserDetails"];

                //var MobileRacharge =ZapurseCommonlist.GetServicesAllDetails().Where(wh => wh.CategoryName.Contains("Mobile Recharge")).FirstOrDefault();

                //var serviceProvider = ZapurseCommonlist.GetServideProviderList().Where(wh => wh.Text.Contains("SHAKTIFINTECH")).FirstOrDefault();
                //var transectionID = "TXN" + DateTime.Now.ToString("ddMMyyyyhhmmss") + userdetailsSession.PartyId;

                //APIReqResModal aPIReqRes = new APIReqResModal();

                //aPIReqRes.API_Request = json;
                //aPIReqRes.API_Response = response.Content.ToString();
                //aPIReqRes.TransactionId = transectionID;

                //SaveApiReqRes(aPIReqRes);

                //TransectionMasterRequest transection = new TransectionMasterRequest();

                //transection.TransectionId = transectionID;
                //transection.TransectionAmount = Convert.ToDecimal(rechargeAmount);
                //transection.RefrenceId = objResponse.REQUESTTXNID;
                //transection.PartyId = userdetailsSession.PartyId;
                //transection.UserType = Convert.ToInt32(userdetailsSession.Type);
                //transection.OperatorId = operatorCode;
                //transection.ServicetypeId = Convert.ToInt32(MobileRacharge.CategoryId);
                //transection.ServiceProviderId = Convert.ToInt32(serviceProvider.Id);
                //transection.MobileNo = mobileNo;
                //transection.Longitude = longitude;
                //transection.Latitude = latitude;
                //transection.IPAddress = IpAddressCollection["IPv4_Address"].ToString();

                //SaveTransection(transection);
                //UpdateSoldPriceForTransection(transectionID, Convert.ToDecimal(rechargeAmount));



            }




            //new RechargeRequestResponse
            //{
            //    STATUS = 1,
            //    MESSAGE = "Request is successfull!",
            //    ERRORCODE = "0",
            //    OPTXNID = "32csdnewr8wedns4309",
            //    TXNNO = "sajdsiu435834ew934",
            //    REQUESTTXNID = "43932jxmeods0,dsreo",
            //    HTTPCODE = 200

            //}


            return new JsonResult
            {
                Data = objResponse,
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public string SaveTransection(TransectionMasterRequest transection)
        {
            var json = JsonConvert.SerializeObject(transection);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SaveTransection");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }
            return objResponse.Message;
        }
        public string UpdateSoldPriceForTransection(string transectionId, decimal rechargeAMT)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UpdateSoldPriceForTransection?transectionId=" + transectionId + "&rechargePrice=" + rechargeAMT);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }
            return objResponse.Message;
        }
        public JsonResult GetCommissionRatesForUser(string partyID, int PartyType, int operatorId, int serviceId, int serviceProviderId)
        {

            var data = ZapurseCommonlist.GetCommissionRatesList(partyID, PartyType, operatorId, serviceId, serviceProviderId);

            return new JsonResult
            {
                Data = new { STATUS = 1, ERRORCODE = 0, Data = data, Failure = false, MESSAGE = "Commission Rates" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public string SaveApiReqRes(APIReqResModal aPIReqRes)
        {
            var json = JsonConvert.SerializeObject(aPIReqRes);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SaveAPIReqRes");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }
            return objResponse.Message;
        }



    }
}