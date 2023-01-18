using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Common;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    //[NoDirectAccess]
    public class AdminController : Controller
    {
        // GET: Admin
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();

        public ActionResult WalletLeft()
        {
            //MobileRechargeController mobile = new MobileRechargeController();
            //var balanceLeft = mobile.CheckBalance();
            //var jsonBalance = JsonConvert.SerializeObject(balanceLeft.Data);
            //var objResponse = JsonConvert.DeserializeObject<CheckWalletResponse>(jsonBalance);
            //ViewBag.balance = Convert.ToDecimal(objResponse.BALANCE);
            return View();
        }
        #region Committee
        public ActionResult AdminApplicationPreview()
        {
            var recentApplicationList = ZapurseCommonlist.GetAdminApplication("");
            ViewBag.applicationDetails = recentApplicationList;

            return View();
        }
        
        public ActionResult InspactionApplicationList()
        {
            var recentApplicationList = ZapurseCommonlist.GetAdminApplication("");
            ViewBag.applicationDetails = recentApplicationList;

            return View();
        }

        public ActionResult ApplicationPreviewLayout(string applGUID)
        {
            var EditdraftedApplications = ZapurseCommonlist.GetAdminApplication(applGUID);
            ViewBag.applicationDetails = EditdraftedApplications[0];
            //Data To Preview

            var trusteeMember = ZapurseCommonlist.GetTrusteeMember(EditdraftedApplications[0].iFKTst_ID);
            var LandData = ZapurseCommonlist.GetLandBuildingInfo(applGUID);
            var AcadmicData = ZapurseCommonlist.GetAcdmcData();
            var subjectData = ZapurseCommonlist.GetSubjectList(applGUID);

            ViewBag.trusteeMember = trusteeMember;
            ViewBag.landDataList = LandData;
            ViewBag.AcadmicDataList = AcadmicData;
            ViewBag.subjectDataList = subjectData;
            return View();
        }

        public JsonResult CommitteeList(int deptID)
        {

            var committeeList = ZapurseCommonlist.getCommitteeList(deptID);

            return new JsonResult
            {
                Data = new { Data = committeeList, failure = true, msg = "success" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult CommitteeMembersList(int committeeID)
        {
            var committeeList = ZapurseCommonlist.getCommitteeMembersList(committeeID);

            return new JsonResult
            {
                Data = new { Data = committeeList, failure = true, msg = "success" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AssignCommitteeToApplication(Committee committeeData)
        {
            var json = JsonConvert.SerializeObject(committeeData);
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Committee/AssignCommitteeToApplication");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData responseData = new ResponseData();

            if (response.StatusCode.ToString() == "OK")
            {
                responseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }
            return new JsonResult
            {
                Data = new { Data = responseData, failure = true, msg = responseData.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetExistingCommitteeAsignment(string applicationNumber)
        {
            var committeeList = ZapurseCommonlist.GetExistingCommitteeAsignment(applicationNumber);

            return new JsonResult
            {
                Data = new { Data = committeeList, failure = true, msg = "success" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
        public JsonResult SendMail(SendMailToCommittee mailToCommittee)
        {
            var json = JsonConvert.SerializeObject(mailToCommittee);
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Committee/SendMailToCommitteeMembers");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "success" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion


        public ActionResult Detail()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {

                List<CategoryMaster> objCaterMastermaster = new List<CategoryMaster>();
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetServicesDetail");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    //var data = _JsonSerializer.Deserialize<CategoryMaster>(response.Contentd);
                    //CategoryMasterData objResponseData = _JsonSerializer.Deserialize<CategoryMasterData>(response.Content);
                    //objUsermaster = objResponseData.ListRequest;

                    if (d.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (d.ResponseCode == "000" && d.Data != null)
                    {
                        var objResponseData = JsonConvert.DeserializeObject<ListCategoryMaster>(d.Data.ToString());
                        objCaterMastermaster = objResponseData.ListRequest;
                    }
                }

                var permissions = (List<UserPermissions>)Session["UserPermissions"];
                if (permissions != null)
                {
                    var data = new List<UserPermissions>();//permissions.Where(wh => wh.SubMenu.Contains("Commission Master")).ToList();
                    ViewBag.PagePermission = data;
                }

                return View(objCaterMastermaster);
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        public ActionResult Report()
        {
            return View();
        }
        public ActionResult Create(CategoryMaster Master = null)
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var notificationMasterdata = new List<NotificationMaster>();
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {

                int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
                if (Request.HttpMethod == "POST")
                {
                    string HardwareList = Request["HardwareList"];
                    string DocumentList = Request["DocumentList"];
                    string varificationList = Request["varificationList"];
                    string Classname = Request["ClassName"];
                    Master.DocumentList = DocumentList;
                    Master.HardwareList = HardwareList;
                    Master.ClassName = Classname;
                    string PI = objcf.getIPAdd();
                    Master.varificationList = varificationList;
                    if (Id == 0)
                    {
                        Master.IsActive = 1;

                    }
                    Master.CategoryId = Id;
                    try
                    {
                        var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/InsertServices");
                        var request = new RestRequest(Method.POST);
                        request.AddHeader("cache-control", "no-cache");

                        request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
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
                                return RedirectToAction("Detail");
                            }
                            else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                            {
                                TempData["SwalStatusMsg"] = "warning";
                                TempData["SwalMessage"] = objResponseData.Message;
                                TempData["SwalTitleMsg"] = "warning!";
                                return RedirectToAction("Detail");
                            }
                            else
                            {
                                TempData["SwalStatusMsg"] = "error";
                                TempData["SwalMessage"] = "Something wrong";
                                TempData["SwalTitleMsg"] = "error!";
                                return RedirectToAction("Detail");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("Detail");
                    }
                    return RedirectToAction("Details");
                }
                else
                {
                    CategoryMaster objCaterMastermaster = new CategoryMaster();
                    if (Id != 0)
                    {

                        var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetServicesDetail?Id=" + Id);
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");
                        //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                        request.AddParameter("application/json", "", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        if (response.StatusCode.ToString() == "OK")
                        {
                            var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                            var objResponseData = JsonConvert.DeserializeObject<ListCategoryMaster>(d.Data.ToString());
                            //var data = _JsonSerializer.Deserialize<CategoryMaster>(response.Content);
                            //CategoryMasterData objResponseData = _JsonSerializer.Deserialize<CategoryMasterData>(response.Content);
                            //objUsermaster = objResponseData.ListRequest;

                            if (d.ResponseCode == "001")
                            {
                                return RedirectToAction("SignOut", "Home");
                            }
                            else if (d.ResponseCode == "000")
                            {
                                objCaterMastermaster = objResponseData.ListRequest.FirstOrDefault();
                            }
                        }

                    }
                    List<CustomMaster> HardwareLst = new List<CustomMaster>();
                    HardwareLst = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.HardwareType));
                    List<CustomMaster> DocumentLst = new List<CustomMaster>();
                    DocumentLst = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.DocumentType));
                    List<CustomMaster> VarificationLst = new List<CustomMaster>();
                    VarificationLst = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.VerificationType));
                    ViewBag.HardwareLst = HardwareLst;
                    ViewBag.DocumentLst = DocumentLst;
                    ViewBag.VarificationLst = VarificationLst;
                    //ViewBag.NotificationAllowed = isnotificationAllowed;
                    return View(objCaterMastermaster);
                }

            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }

        }
        public ActionResult RateMaster()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                List<ShowRateMaster> objCaterMastermaster = new List<ShowRateMaster>();
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetRateMasterDetail");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    //var data = _JsonSerializer.Deserialize<CategoryMaster>(response.Contentd);
                    //CategoryMasterData objResponseData = _JsonSerializer.Deserialize<CategoryMasterData>(response.Content);
                    //objUsermaster = objResponseData.ListRequest;

                    if (d.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (d.ResponseCode == "000" && d.Data != null)
                    {
                        objCaterMastermaster = JsonConvert.DeserializeObject<List<ShowRateMaster>>(d.Data.ToString());
                        //  objCaterMastermaster =  JsonConvert.DeserializeObject<List<ShowRateMaster>>(objResponseData.ToString());
                    }
                }
                List<CustomMaster> Chargetype = new List<CustomMaster>();
                Chargetype = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.ChargeType));

                List<CustomMaster> UnitType = new List<CustomMaster>();
                UnitType = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.UnitType));

                List<CustomMaster> PaymentRange = new List<CustomMaster>();
                PaymentRange = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.MonthType));
                ViewBag.Chargetype = Chargetype;
                ViewBag.UnitType = UnitType;
                ViewBag.PaymentRange = PaymentRange;
                ViewBag.Servicelist = objCaterMastermaster;
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        public ActionResult RateCreate(RateMaster master)
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                master.IsActive = 0;
                try
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/InsertRateMaster");
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
                            return RedirectToAction("RateMaster");
                        }
                        else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                        {
                            TempData["SwalStatusMsg"] = "warning";
                            TempData["SwalMessage"] = objResponseData.Message;
                            TempData["SwalTitleMsg"] = "warning!";
                            return RedirectToAction("RateMaster");
                        }
                        else
                        {
                            TempData["SwalStatusMsg"] = "error";
                            TempData["SwalMessage"] = "Something wrong";
                            TempData["SwalTitleMsg"] = "error!";
                            return RedirectToAction("RateMaster");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    return RedirectToAction("RateMaster");
                }
                return RedirectToAction("RateMaster");
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        public JsonResult FillListServices(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();


            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetCustomList?EnumNo=" + Enum);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var objResponseData = JsonConvert.DeserializeObject<ListCustom>(d.Data.ToString());

                objUsermaster = objResponseData.ListRequest;
                if (d.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = objUsermaster, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (d.ResponseCode == "000" && d.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objUsermaster, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult ChangeStatus(string TableId, int type, int Id)
        {
            Activeclass objRatemaster = new Activeclass();
            objRatemaster.Id = Id;
            objRatemaster.Tablename = TableId;
            objRatemaster.status = type;


            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ChangeStatus");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            //request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(objRatemaster), ParameterType.RequestBody);
            IRestResponse response = client2.Execute(request2);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }

            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public ActionResult GstApplicable()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                List<GSTApplicable> objCaterMastermaster = new List<GSTApplicable>();
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetApplicableGstDetails");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);


                    if (d.ResponseCode == "000" && d.Data != null)
                    {

                        objCaterMastermaster = JsonConvert.DeserializeObject<List<GSTApplicable>>(d.Data.ToString());


                    }
                }
                List<CustomMaster> UnitType = new List<CustomMaster>();
                UnitType = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.UnitType));
                List<CustomMaster> TaxType = new List<CustomMaster>();
                TaxType = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.TaxType));
                ViewBag.UnitType = UnitType;
                ViewBag.TaxType = TaxType;
                return View(objCaterMastermaster);
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }

        }
        public ActionResult GSTEditApplicable(GSTApplicable applicable)
        {
            var ApplicableChargesType = Request["ApplicableChargesType"];
            applicable.ApplicableChargesType = ApplicableChargesType;
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/InserGstApplicable");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(applicable), ParameterType.RequestBody);
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
                        return RedirectToAction("GstApplicable");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("GstApplicable");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("GstApplicable");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                return RedirectToAction("GstApplicable");
            }
            return RedirectToAction("GstApplicable");
        }

        //public List<GSTApplicable> ShowGstApplicable()
        //{
        //    List<GSTApplicable> objCaterMastermaster = new List<GSTApplicable>();
        //    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetApplicableGstDetails");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("cache-control", "no-cache");
        //    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    request.AddParameter("application/json", "", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    if (response.StatusCode.ToString() == "OK")
        //    {
        //        var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);


        //      if (d.ResponseCode == "000" && d.Data != null)
        //        {

        //                objCaterMastermaster = JsonConvert.DeserializeObject<List<GSTApplicable>>(d.Data.ToString());


        //        }
        //    }
        //    return objCaterMastermaster;
        //}
        public JsonResult FillChargesType(long ServiceId)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();


            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetChargesType?ServiceId=" + ServiceId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var objResponseData = JsonConvert.DeserializeObject<ListCustom>(d.Data.ToString());

                objUsermaster = objResponseData.ListRequest;
                if (d.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = objUsermaster, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (d.ResponseCode == "000" && d.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objUsermaster, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #region Add Client Change Request
        public ActionResult ClientChangeRequest()
        {
            //string PartyId = "D000002";
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string PartyId = userdetailsSession.PartyId;
            List<Models.ClientSwitchRequestData> _result = new List<Models.ClientSwitchRequestData>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetChangeRequestListNew?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<Models.ClientSwitchRequestData>>(response.Content);
                ViewData["PendingList"] = _result;
            }
            return View(_result);
        }

        public ActionResult RejectChangeRequest(string PartyId)
        {
            //string LoginId = "S000001";
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string LoginId = userdetailsSession.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/RejectChangeRequest?PartyId=" + PartyId + "&LoginId=" + LoginId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = objResponseData.Message;
                    TempData["SwalTitleMsg"] = "Success!";
                    return RedirectToAction("ClientChangeRequest");
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    return RedirectToAction("ClientChangeRequest");
                }
            }
            return RedirectToAction("ClientChangeRequest");
        }

        public ActionResult ApprovedChangeRequest(string PartyId)
        {
            //string LoginId = "A000001";
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string LoginId = userdetailsSession.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ApprovedChangeRequest?PartyId=" + PartyId + "&LoginId=" + LoginId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = objResponseData.Message;
                    TempData["SwalTitleMsg"] = "Success!";
                    return RedirectToAction("ClientChangeRequest");
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    return RedirectToAction("ClientChangeRequest");
                }
            }
            return RedirectToAction("ClientChangeRequest");
        }

        public JsonResult RetailerByDistributorID(string ID)
        {
            var UserDetails = (UserModelSession)Session["UserDetails"];
            SwitchVendorRequest obj = new SwitchVendorRequest();
            obj.PartyId = UserDetails.PartyId;
            obj.ParentID = ID;
            #region RetailersBydistributorid

            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/RetailerByDistributorID");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", obj.PartyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            //request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                if (LoginObj.ResponseCode == "1")
                {
                    return new JsonResult
                    {
                        Data = new { Lst = LoginObj.vendetList },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
            {
                TempData["Type"] = "warning";
                TempData["Message"] = "Session Expired! Please login again.";
                TempData["Flag"] = "-1";
            }
            #endregion
            return new JsonResult
            {
                Data = new { Lst = "" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UpdateParent(SwitchVendorRequest model)
        {
            var UserDetails = (UserModelSession)Session["UserDetails"];
            //UserDetails.PartyId;
            #region update Parent Or ASM
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/SwitchParent");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", UserDetails.PartyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
            //request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                if (LoginObj.ResponseCode == "1")
                {
                    return new JsonResult
                    {
                        Data = new { Details = LoginObj },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
            {
                TempData["Type"] = "warning";
                TempData["Message"] = "Session Expired! Please login again.";
                TempData["Flag"] = "-1";
            }
            #endregion
            return new JsonResult
            {
                Data = new { Details = "" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpGet]
        public ActionResult CollectRequest(ClientSwitchRequestData req)
        {
            var UserDetails = (UserModelSession)Session["UserDetails"]; ;
            Models.ClientSwitchRequestData viewModal = new Models.ClientSwitchRequestData();
            Models.ClientSwitchRequestData modal = new Models.ClientSwitchRequestData();
            modal.PartyId = req.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetChangeRequestData?PartyId=" + req.PartyId);
            var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json;");
            request.AddHeader("LoginID", UserDetails.PartyId);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                viewModal = _JsonSerializer.Deserialize<Models.ClientSwitchRequestData>(response.Content);
                //viewModal.collectMoney.PayTrackingId = PayTrackingId;
            }
            return PartialView("_ChangeRequestDetail", viewModal);
        }

        public JsonResult GetActivePartyList(string newparenttype, string oldparentid)
        {
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            ListRequest obj = new ListRequest();
            obj.ParentID = oldparentid;
            obj.CangeRequestType = newparenttype;
            #region GetParentList
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetActivePartyList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", servicesCollection.PartyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                SelectedList LoginObj = _JsonSerializer.Deserialize<SelectedList>(response.Content);
                return new JsonResult
                {
                    Data = new { Lst = LoginObj.Data },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
            {
                TempData["Type"] = "warning";
                TempData["Message"] = "Session Expired! Please login again.";
                TempData["Flag"] = "-1";
            }
            return new JsonResult
            {
                Data = new { Lst = "" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            #endregion            
        }
        #endregion

        #region setting  created by Abhishek
        public ActionResult Setting()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                List<setting> settings = new List<setting>();
                settings = Common.GetSettings();
                List<CustomEnum> CustomEnumlist = new List<CustomEnum>();
                CustomEnumlist = Common.GetCustomEnum();
                ViewBag.CustomEnumlist = CustomEnumlist;
                return View(settings);
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        public JsonResult InsertSubjectLines(int Type)
        {
            int Id = new int();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/InsertNewCustomEnumRow?Id=" + Type);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                //var objResponseData = JsonConvert.DeserializeObject<int>(d.Data.ToString());

                Id = d.ID;
                if (d.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = Id, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult DeleteCustomSentence(int deteleId)
        {
            int Id = new int();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/DeleteCustomSentence?Id=" + deteleId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                //var objResponseData = JsonConvert.DeserializeObject<int>(d.Data.ToString());


                if (d.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = 1, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

            }
            return new JsonResult
            {
                Data = new { Data = 0, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult UpdateSubjectLines(int Id, string LineText)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/UpdateSubjectLines?Id=" + Id + "&Text=" + LineText);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                //var objResponseData = JsonConvert.DeserializeObject<int>(d.Data.ToString());


                if (d.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = 1, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }

            }
            return new JsonResult
            {
                Data = new { Data = 0, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };


        }
        #endregion

        #region Add Department and Group by Vivek

        [HttpGet]
        public ActionResult AddDepartment()
        {

            var UserDetails = Session["UserDetails"];
            if (UserDetails != null)
            {
                var departmentList = GetDepartments();

                ViewBag.departmentList = departmentList;

                return View();
            }
            else
            {
                return RedirectToAction("login-alt", "authentication");
            }
        }
        public JsonResult AddDepartment(AddDepartment department)
        {

            var UserDetails = (UserModelSession)Session["UserDetails"];
            if (UserDetails != null)
            {

                department.PartyId = UserDetails.PartyId;
                department.CreatedBy = UserDetails.PartyId;

                var json = JsonConvert.SerializeObject(department);
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/AddDepartment");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    return new JsonResult
                    {
                        Data = new { StatusCode = d.statusCode, Data = "", Failure = false, Message = d.Message },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult EditDepartment(int DepartmentID, string Type = "DepartmentSingle")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + DepartmentID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var Department = JsonConvert.DeserializeObject<List<AddDepartment>>(d.Data.ToString());

                return new JsonResult
                {
                    Data = new { StatusCode = d.statusCode, Data = Department, Failure = false, Message = d.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public List<AddDepartment> GetDepartments(string Type = "DepartmentList")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=0");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AddDepartment> departments = new List<AddDepartment>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                departments = JsonConvert.DeserializeObject<List<AddDepartment>>(objResponse.Data.ToString());
            }
            return departments;
        }

        public List<Dropdown> GetRoles(string Type = "RoleList")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=0");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> roles = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                roles = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return roles;
        }
        public static List<Dropdown> GetAllTemplatesType(string Type = "Templates")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=0");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> roles = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                roles = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return roles;
        }

        public List<AadhaarDetails> GetAadhaarDetails(string partyID)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetAadhaarData?partyID=" + partyID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AadhaarDetails> roles = new List<AadhaarDetails>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                roles = JsonConvert.DeserializeObject<List<AadhaarDetails>>(objResponse.Data.ToString());
            }
            return roles;
        }


        [HttpGet]
        public ActionResult AddGroup()
        {

            var UserDetails = (UserModelSession)Session["UserDetails"];
            if (UserDetails != null)
            {
                var departmentList = GetDepartments();
                var groups = GetGroups(UserDetails.PartyId);
                var Menus = ZapurseCommonlist.GetMenusList();

                ViewBag.departmentList = departmentList;
                ViewBag.departmentList = departmentList;
                ViewBag.groups = groups;
                ViewBag.Menus = Menus;

                return View();
            }
            else
            {
                return RedirectToAction("login-alt", "authentication");
            }
        }
        public JsonResult AddGroup(AddGroup group)
        {

            var UserDetails = (UserModelSession)Session["UserDetails"];
            if (UserDetails != null)
            {

                group.PartyId = UserDetails.PartyId;
                group.CreatedBy = UserDetails.PartyId;

                var json = JsonConvert.SerializeObject(group);
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/AddGroup");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    return new JsonResult
                    {
                        Data = new { StatusCode = d.statusCode, Data = "", Failure = false, Message = d.Message },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult EditGroup(int GroupId, string Type = "GroupSingle")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + GroupId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var Groups = JsonConvert.DeserializeObject<List<AddGroup>>(d.Data.ToString());

                return new JsonResult
                {
                    Data = new { StatusCode = d.statusCode, Data = Groups, Failure = false, Message = d.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public List<AddGroup> GetGroups(string partyID, string Type = "GroupList")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=0&PartyId=" + partyID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AddGroup> groups = new List<AddGroup>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                groups = JsonConvert.DeserializeObject<List<AddGroup>>(objResponse.Data.ToString());
            }
            return groups;
        }


        public ActionResult ManageUserRights()
        {
            var UserDetails = (UserModelSession)Session["UserDetails"];
            if (UserDetails != null)
            {
                var departmentList = GetDepartments();
                var groupList = GetGroups(UserDetails.PartyId);

                ViewBag.departmentList = departmentList;
                ViewBag.groupList = groupList;

                return View();
            }
            else
            {
                return RedirectToAction("login-alt", "authentication");
            }
        }
        #endregion

        #region ServiceProviderWalletconsumption AddedBy : Shipra Garg 
        [HttpGet]
        public ActionResult ServiceProviderWalletconsumption()
        {
            var servicesCollection = (Data)Session["UserAllDetails"];
            if (servicesCollection != null)
            {
                #region Get ServiceType
                List<CustomMaster> ServiceTypeLst = new List<CustomMaster>();
                ServiceTypeLst = Common.GetCustomMastersList(15);
                ViewBag.ServiceTypeDDL = ServiceTypeLst.Select(m => new SelectListItem { Text = m.text, Value = m.Id.ToString() }).ToList();
                #endregion

                #region Get Service Provider List
                List<CustomMaster> ServiceProvider = new List<CustomMaster>();
                ServiceProvider = Common.GetCustomMastersList(14);
                ViewBag.ServiceProviderDDL = ServiceProvider.Select(m => new SelectListItem { Text = m.text, Value = m.Id.ToString() }).ToList();
                #endregion

                #region Get Consuption List for ServiceProvider Wise         
                CompanyConsumption model = new CompanyConsumption();
                model.Type = "ConsumptionList";
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ConsumptionList");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                //request.AddHeader("Content-Type", "application/json;");
                //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                //request.AddHeader("LoginID", CurrentSessions.AtishayVendorID.ToString());
                request.AddHeader("LoginID", servicesCollection.userDetails.partyId);
                request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    CompanyConsumptionList LoginObj = _JsonSerializer.Deserialize<CompanyConsumptionList>(response.Content);
                    if (LoginObj.ResponseCode == "1")
                    {
                        ViewData["ConsumptionList"] = LoginObj.CompanyConsumptionLst;
                    }
                }
                #endregion
            }
            return View();
        }

        [HttpPost]
        public ActionResult ServiceProviderWalletconsumption(CompanyConsumption model)
        {
            try
            {

                #region Get ServiceType
                List<CustomMaster> ServiceTypeLst = new List<CustomMaster>();
                ServiceTypeLst = Common.GetCustomMastersList(15);
                ViewBag.ServiceTypeDDL = ServiceTypeLst.Select(m => new SelectListItem { Text = m.text, Value = m.Id.ToString() }).ToList();
                #endregion

                #region Get Service Provider List
                List<CustomMaster> ServiceProvider = new List<CustomMaster>();
                ServiceProvider = Common.GetCustomMastersList(14);
                ViewBag.ServiceProviderDDL = ServiceProvider.Select(m => new SelectListItem { Text = m.text, Value = m.Id.ToString() }).ToList();
                #endregion

                var servicesCollection = (Data)Session["UserAllDetails"];
                if (servicesCollection != null)
                {
                    model.PartyId = servicesCollection.userDetails.partyId;
                    #region WalletRecharge
                    //model.Platform = "Web";
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ConsumptionRequest");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddHeader("LoginID", model.PartyId);
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    //request.AddParameter("application/json", "", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        CompanyConsumptionList ReqObj = _JsonSerializer.Deserialize<CompanyConsumptionList>(response.Content);
                        if (ReqObj.ResponseCode == "1")
                        {
                            TempData["SwalStatusMsg"] = "success";
                            TempData["SwalMessage"] = "Request Sent Successfully!";
                            TempData["SwalFlag"] = "1";
                            TempData["SwalTitleMsg"] = "Success";
                        }
                        else
                        {
                            TempData["SwalStatusMsg"] = "error";
                            TempData["SwalMessage"] = "Some technical issue!Please Contact Support.";
                            TempData["SwalFlag"] = "-1";
                            TempData["SwalTitleMsg"] = "error";
                        }
                    }
                    else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                    {
                        servicesCollection.userDetails.partyId = "";
                        //servicesCollection.userDetails.Token = "";
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = "Session Expired! Please login again.";
                        TempData["Flag"] = "-1";
                        TempData["SwalTitleMsg"] = "warning";
                        return RedirectToAction("logout", "Home");
                    }
                    else
                    {
                        TempData["SwalMessage"] = "Some technical issue!Please Contact Support.";
                        TempData["SwalFlag"] = "-1";
                    }
                    #endregion
                    #region Get Consuption List for ServiceProvider Wise         
                    CompanyConsumption modelList = new CompanyConsumption();
                    model.Type = "ConsumptionList";
                    client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ConsumptionList");
                    request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    request.AddHeader("LoginID", servicesCollection.userDetails.partyId);
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        CompanyConsumptionList LoginObj = _JsonSerializer.Deserialize<CompanyConsumptionList>(response.Content);
                        if (LoginObj.ResponseCode == "1")
                        {
                            ViewData["ConsumptionList"] = LoginObj.CompanyConsumptionLst;
                        }
                    }
                    #endregion
                    return RedirectToAction("ServiceProviderWalletconsumption", "Client");
                }
            }
            catch (Exception ex)
            {
                TempData["Type"] = "error";
                TempData["Message"] = "Details Not Found-Ex1! Please try after sometime.";
            }
            return View();
        }
        #endregion

    }
}