using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    public class CommissionController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();

        public ResponseData ObjResponse = null;
        public ActionResult ServiceProviderIndex()
        {
            List<Serviceprovider> Provider = new List<Serviceprovider>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GetServiceProvider");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    Provider = JsonConvert.DeserializeObject<List<Serviceprovider>>(d.Data.ToString());
                }

            }
            return View(Provider);
        }
        public ActionResult ServiceProviderCreate(Serviceprovider master)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            master.PartyId = servicesCollectiondata.PartyId;
            Serviceprovider obj = new Serviceprovider();
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/InsertCommission");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("ServiceProviderIndex", "Commission");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("ServiceProviderIndex", "Commission");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("ServiceProviderIndex", "Commission");
                    }
                }

            }
            else
            {
                if (Id != 0)
                {


                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GetServiceProvider?Id=" + Id);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddParameter("application/json", "", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                        if (d.Data != null)
                        {

                            obj = JsonConvert.DeserializeObject<List<Serviceprovider>>(d.Data.ToString()).FirstOrDefault();
                        }

                    }
                }
                List<CustomMaster> objUsermaster = new List<CustomMaster>();
                objUsermaster = Common.GetCustomMastersList(0);
                ViewBag.ServiceType = objUsermaster;
                List<CustomMaster> ServiceProvider = new List<CustomMaster>();
                ServiceProvider = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.ServiceProvider));
                ViewBag.ServiceProvide = ServiceProvider;
            }


            return View(obj);
        }

        public ActionResult ServiceProviderRateMaster(ServiceProviderRateconfiguration master)
        {

            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            master.PartyId = servicesCollectiondata.PartyId;
            Serviceprovider obj = new Serviceprovider();
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/InsertServiceProviderrate");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                    }
                }

            }
            List<ServiceProviderRateconfiguration> ServiceRatelist = new List<ServiceProviderRateconfiguration>();
            ServiceRatelist = Common.GetServiceProviderRate(Id);
            List<CustomMaster> UnitType = new List<CustomMaster>();
            UnitType = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.UnitType));
            List<CustomMaster> OperaterName = new List<CustomMaster>();
            OperaterName = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.OperaterName));
            ViewBag.UnitType = UnitType;
            ViewBag.OperaterName = OperaterName;
            ViewBag.Id = Id;
            return View(ServiceRatelist);
        }
        public ActionResult CommissionMaster(tbl_CommissionMaster obj)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            obj.PartyId = servicesCollectiondata.PartyId;

            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/InsertCommissionInformation");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {

                        //var NotificationList = (List<NotificationMaster>)Session["notificationList"];
                        //var TowhomSendNotification = NotificationList.Where(wh => wh.SubMenuName.Contains("Commission Master") && wh.EventName == "Save").FirstOrDefault();

                        //if (TowhomSendNotification != null)
                        //{
                        //    var data = new NotificationTransectionUserListRequest { PartyID = servicesCollectiondata.PartyId, FlowDirection = TowhomSendNotification.SendtoFlow, configId = TowhomSendNotification.ConfId, specificUser = null };

                        //    ZapurseCommonlist.SaveNotificationTransactionData(data);
                        //}

                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("CommissionMasterIndex", "Commission");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                    }
                }

            }
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            objUsermaster = Common.GetCustomMastersList(0);
            ViewBag.ServiceType = objUsermaster;
            List<CustomMaster> UserType = new List<CustomMaster>();
            UserType = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.UserType));
            ViewBag.UserType = UserType;
            ViewBag.PartyId = obj.PartyId;
            return View();
        }

        public ActionResult CommissionMasterIndex()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            List<tbl_CommissionMaster> Provider = new List<tbl_CommissionMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GetCommissionMaster?PartyId=" + PartyId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    Provider = JsonConvert.DeserializeObject<List<tbl_CommissionMaster>>(d.Data.ToString());
                }

            }
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            objUsermaster = Common.GetCustomMastersList(0);
            ViewBag.ServiceType = objUsermaster;
            ViewBag.PartyId = PartyId;
            return View(Provider);
        }

        public ActionResult UserCommission(int Id, int UserType, int ServiceId, string IsdefaultPartyId = null, string commissionFor=null)
        {
            //int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            //int UserType = Convert.ToInt32(Request.RequestContext.RouteData.Values["UserType"]);
            //int ServiceId = Convert.ToInt32(Request.RequestContext.RouteData.Values["ServiceId"]);
            List<CustomMaster> OperaterName = new List<CustomMaster>();
            OperaterName = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.ServiceProvider));
            ViewBag.OperaterName = OperaterName;
            ViewBag.Id = Id;
            ViewBag.UserType = UserType;
            ViewBag.ServiceId = ServiceId;
            ViewBag.IsdefaultPartyId = IsdefaultPartyId;
            ViewBag.commissionFor = commissionFor;
            return View();
        }
        public ActionResult RechargeCommissionIndex()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            List<tbl_CommissionMaster> Provider = new List<tbl_CommissionMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GetCommissionMaster?PartyId=" + PartyId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    Provider = JsonConvert.DeserializeObject<List<tbl_CommissionMaster>>(d.Data.ToString());
                }

            }
            ViewBag.PartyId = PartyId;
            return View(Provider);
        }
        public ActionResult RechargeCommission()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;

            int Type = Convert.ToInt32(servicesCollectiondata.Type);

            int OperaterEnum = Convert.ToInt32(TypeDocument.OperaterName);
            int ServiceProvider = Convert.ToInt32(TypeDocument.ServiceProvider);
            List<UserCommission> Commissionlist = new List<UserCommission>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GetRechargeData?OperaterId=" + 0 + "&UserType=" + Type + "&ServiceId=" + 5 + "&OperaterNameEnum=" + OperaterEnum + "&ServiceProviderEnum=" + ServiceProvider + "&PartyId=" + PartyId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    Commissionlist = JsonConvert.DeserializeObject<List<UserCommission>>(d.Data.ToString());
                }

            }
            ViewBag.Type = Type;
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            objUsermaster = Common.GetCustomMastersList(0);
            ViewBag.ServiceType = objUsermaster;
            List<CustomMaster> UserType = new List<CustomMaster>();
            UserType = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.UserType));
            ViewBag.UserType = UserType;
            ViewBag.PartyId = PartyId;
            return View(Commissionlist);

        }
        [HttpPost]
        public ActionResult RechargeCommissionMaster(tbl_CommissionMaster obj)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            obj.PartyId = servicesCollectiondata.PartyId;


            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/InsertCommissionInformation");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return RedirectToAction("SignOut", "Home");
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    var NotificationList = (List<NotificationMaster>)Session["notificationList"];
                    var TowhomSendNotification = NotificationList.Where(wh => wh.SubMenuName.Contains("Commission Recharge") && wh.EventName == "Save").FirstOrDefault();

                    if (TowhomSendNotification != null)
                    {
                        var data = new NotificationTransectionUserListRequest { PartyID = servicesCollectiondata.PartyId, FlowDirection = TowhomSendNotification.SendtoFlow, configId = TowhomSendNotification.ConfId, specificUser = null };

                        ZapurseCommonlist.SaveNotificationTransactionData(data);
                    }











                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = objResponseData.Message;
                    TempData["SwalTitleMsg"] = "Success!";
                    //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                {
                    //TempData["SwalStatusMsg"] = "warning";
                    //TempData["SwalMessage"] = objResponseData.Message;
                    //TempData["SwalTitleMsg"] = "warning!";
                    //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                }
            }


            return RedirectToAction("RechargeCommissionIndex", "Commission");

        }

        public ActionResult RechargeUserCommission(int Id, int UserType, int ServiceId, string IsdefaultPartyId = null, string commissionFor = null)
        {
            List<CustomMaster> OperaterName = new List<CustomMaster>();
            OperaterName = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.ServiceProvider));
            ViewBag.OperaterName = OperaterName;
            ViewBag.Id = Id;
            ViewBag.UserType = UserType;
            ViewBag.ServiceId = ServiceId;
            ViewBag.IsdefaultPartyId = IsdefaultPartyId;
            ViewBag.commissionFor = commissionFor;
            return View();
        }
        public JsonResult ChangeStatus(List<Commisssiondata> TableName)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/ChangeStatus");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", _JsonSerializer.Serialize(TableName.FirstOrDefault()), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { isvalid = 1, Failure = false, Message = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

            }
            return new JsonResult
            {
                Data = new { isvalid = 0, Failure = false, Message = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //Changes 09-09-2022
        public JsonResult ServiceProviderChangeStatus(string TableId, int Id, int Type)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/ServiceProviderChangeStatus?TableId=" + TableId + "&Id=" + Id + "&Type=" + Type);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);



            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { isvalid = 1, Failure = false, Message = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

            }
            return new JsonResult
            {
                Data = new { isvalid = 0, Failure = false, Message = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult TestingCreate()
        {
            return View();
        }
        public ActionResult SlabMaster(SLBMST master)
        {
            if (Request.HttpMethod == "POST")
            {
                if (master.bRng == 1)
                {
                    master.bRngSet = true;
                }
                var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
                master.sPatyCode = servicesCollectiondata.PartyId;

                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/Admin_SlabData_Mst_Save");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("SlabMaster", "Commission");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("SlabMaster", "Commission");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                    }
                }
            }


            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            objUsermaster = Common.GetCustomMastersList(0);
            ViewBag.ServiceType = objUsermaster;
            return View();
        }
        public ActionResult AepsMaster(int CommissionMasterId, int UserType, int ServiceId, int MinMaxapplicable = 0, decimal MinMaxValue = 0, string IsdefaultPartyId = null)
        {

            int Aeps = Convert.ToInt32(ServiceId);
            List<SLBMST> lst_slbmst = new List<SLBMST>();
            lst_slbmst = Common.GetSlabRange(Aeps);
            ViewBag.lst_slbmst = lst_slbmst;
            ViewBag.CommissionMasterId = CommissionMasterId;
            ViewBag.UserType = UserType;
            ViewBag.ServiceId = ServiceId;
            ViewBag.IsdefaultPartyId = IsdefaultPartyId;
            ViewBag.MinMaxapplicable = MinMaxapplicable;
            ViewBag.MinMaxValue = MinMaxValue;
            ViewBag.Typename = Partial.TypeName().FirstOrDefault(p => p.Id == UserType).Text;
            return View();
        }

        public ActionResult Admin_setCommission_Show(int serviceId = 1, string DefaultParty = null, string Type = null)
        {

            List<ApesSetCommission> LstApesData = new List<ApesSetCommission>();
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];

            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/Admin_Set_APesCommission_All_Show?PartyId=" + servicesCollectiondata.PartyId + "&ServiceId=" + serviceId + "&DefaultParty=" + DefaultParty);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    LstApesData = JsonConvert.DeserializeObject<List<ApesSetCommission>>(d.Data.ToString());
                }

            }
            List<Dropdown> Client_list = new List<Dropdown>();
            Client_list = Common.GetListClientOfAdmin();
            ViewBag.clientlist = Client_list;
            ViewBag.DefaultParty = DefaultParty;
            return View(LstApesData);
        }

        public ActionResult DTHMaster(int CommissionMasterId, int UserType, int ServiceId, int MinMaxapplicable = 0, decimal MinMaxValue = 0, string IsdefaultPartyId = null)
        {

            int DTH = Convert.ToInt32(ServiceId);
            List<SLBMST> lst_slbmst = new List<SLBMST>();
            lst_slbmst = Common.GetSlabRange(DTH);
            ViewBag.lst_slbmst = lst_slbmst;
            ViewBag.CommissionMasterId = CommissionMasterId;
            ViewBag.UserType = UserType;
            ViewBag.ServiceId = ServiceId;
            ViewBag.IsdefaultPartyId = IsdefaultPartyId;
            ViewBag.MinMaxapplicable = MinMaxapplicable;
            ViewBag.MinMaxValue = MinMaxValue;
            ViewBag.Typename = Partial.TypeName().FirstOrDefault(p => p.Id == UserType).Text;
            return View();
        }

        public ActionResult Admin_DTHCommission_Show(int serviceId = 2)
        {
            List<ApesSetCommission> LstApesData = new List<ApesSetCommission>();
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];

            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/Admin_Set_APesCommission_All_Show?PartyId=" + servicesCollectiondata.PartyId + "&ServiceId=" + serviceId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    LstApesData = JsonConvert.DeserializeObject<List<ApesSetCommission>>(d.Data.ToString());
                }

            }
            return View(LstApesData);

        }
//newzAnnounce
        [ValidateInput(false)]
        public ActionResult NewsAndAnnoucement(NEWANNMST nobj)
        {
            if (Request.HttpMethod == "POST")
            {
                var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
                nobj.sPatyCode = servicesCollectiondata.PartyId;
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/Admin_News_Announcement_Save");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(nobj), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("NewsAndAnnoucementIndex", "Commission");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("NewsAndAnnoucementIndex", "Commission");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        //return RedirectToAction("ServiceProviderRateMaster", "Commission");
                    }
                }
            }
            return View();
        }

        public ActionResult NewsAndAnnoucementIndex()
        {
            List<NEWANNMST> _lstNewAnnoucobj = new List<NEWANNMST>();
            _lstNewAnnoucobj = Common.GetNewAndAnnouncement();
            return View(_lstNewAnnoucobj);
        }

    }
}