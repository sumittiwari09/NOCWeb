using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Common;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    public class ServiceTypeDocumentController : Controller
    {
        // GET: ServiceTypeDocument
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        public ActionResult Detail()
        {
            List<CategoryMaster> objCaterMastermaster = new List<CategoryMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetServicesDetail");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                //var data = JsonConvert.DeserializeObject<CategoryMaster>(response.Contentd);
                //CategoryMasterData objResponseData = JsonConvert.DeserializeObject<CategoryMasterData>(response.Content);
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
            return View(objCaterMastermaster);
        }
        public ActionResult Create(CategoryMaster Master = null)
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
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/InsertServices");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");

                    request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
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

                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetServicesDetail?Id=" + Id);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddParameter("application/json", "", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                        var objResponseData = JsonConvert.DeserializeObject<ListCategoryMaster>(d.Data.ToString());
                        // List<CategoryMaster> ListRequest = JsonConvert.DeserializeObject<List<CategoryMaster>>(objResponseData.Data.ToString());
                        //var data = JsonConvert.DeserializeObject<CategoryMaster>(response.Content);
                        //CategoryMasterData objResponseData = JsonConvert.DeserializeObject<CategoryMasterData>(response.Content);
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
                return View(objCaterMastermaster);
            }


        }
        public ActionResult RateMaster()
        {
            List<ShowRateMaster> objCaterMastermaster = new List<ShowRateMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetRateMasterDetail");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
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
        public ActionResult RateCreate(RateMaster master)
        {
            master.IsActive = 0;
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/InsertRateMaster");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
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
        public JsonResult FillListServices(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetCustomList?EnumNo=" + Enum);
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


            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/ChangeStatus");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            //request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(objRatemaster), ParameterType.RequestBody);
            IRestResponse response = client2.Execute(request2);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
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
            List<GSTApplicable> objCaterMastermaster = new List<GSTApplicable>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetApplicableGstDetails");
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
        public ActionResult GSTEditApplicable(GSTApplicable applicable)
        {
            var ApplicableChargesType = Request["ApplicableChargesType"];
            applicable.ApplicableChargesType = ApplicableChargesType;
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/InserGstApplicable");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(applicable), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
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
        //    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetApplicableGstDetails");
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


            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetChargesType?ServiceId=" + ServiceId);
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
            List<Models.ClientRequestData> _result = new List<Models.ClientRequestData>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetChangeRequestList?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = JsonConvert.DeserializeObject<List<Models.ClientRequestData>>(response.Content);
                ViewData["PendingList"] = _result;
            }
            return View(_result);
        }

        public ActionResult RejectChangeRequest(string PartyId)
        {
            //string LoginId = "S000001";
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string LoginId = userdetailsSession.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/RejectChangeRequest?PartyId=" + PartyId + "&LoginId=" + LoginId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
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
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/ApprovedChangeRequest?PartyId=" + PartyId + "&LoginId=" + LoginId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
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
        #endregion

        #region setting  created by Abhishek
        public ActionResult Setting()
        {
            List<setting> settings = new List<setting>();
            settings = Common.GetSettings();
            List<CustomEnum> CustomEnumlist = new List<CustomEnum>();
            CustomEnumlist = Common.GetCustomEnum();
            ViewBag.CustomEnumlist = CustomEnumlist;
            return View(settings);
        }
        public JsonResult InsertSubjectLines(int Type)
        {
            int Id = new int();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/InsertNewCustomEnumRow?Id=" + Type);
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
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/DeleteCustomSentence?Id=" + deteleId);
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

            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/UpdateSubjectLines?Id=" + Id + "&Text=" + LineText);
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


        [HttpGet]
        public ActionResult AddGroup()
        {

            var UserDetails = Session["UserDetails"];
            if (UserDetails != null)
            {
                var groups = GetGroups("GroupList");

                ViewBag.groups = groups;

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


        public List<AddGroup> GetGroups(string Type = "DepartmentList")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=0");
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


        #endregion

        #region AddNewServiceType

        //public ActionResult ServiceType()
        //{  
        //    List<Mastersddl> ser = new List<Mastersddl>();
        //    var client3 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Mastersddl/GeServiceList");
        //    var request3 = new RestRequest(Method.GET);
        //    request3.AddHeader("cache-control", "no-cache");
        //   // request3.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    //  request3.AddParameter("application/json", "", ParameterType.RequestBody);
        //    IRestResponse response3 = client3.Execute(request3);
        //    if (response3.StatusCode.ToString() == "OK")
        //    {
        //        MastersddlData objResponseData = JsonConvert.DeserializeObject<MastersddlData>(response3.Content);
        //        //List<Mastersddl> ListRequest = JsonConvert.DeserializeObject<List<Mastersddl>>(objResponseData.Data.ToString());
        //        if (objResponseData.ResponseCode == "001")
        //        {
        //            return RedirectToAction("SignOut", "Home");
        //        }
        //        else if (objResponseData.ResponseCode == "000")
        //        {

        //            ViewData["Service"] = new SelectList(objResponseData.Data.ListMasters, "ID", "Name");
        //        }
        //        else
        //        {

        //            ser.Add(new Mastersddl() { Name = "-- Select Service --", ID = "" });
        //            ViewData["Service"] = new SelectList(ser, "ID", "Name", "");
        //        }
        //    }
        //    else
        //    {

        //        ser.Add(new Mastersddl() { Name = "-- Select Service --", ID = "" });
        //        ViewData["Service"] = new SelectList(ser, "ID", "Name", "");
        //    }
        //    var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetServiceList");
        //    var request2 = new RestRequest(Method.GET);
        //    request2.AddHeader("cache-control", "no-cache");
        //   // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    // request2.AddParameter("application/json", "", ParameterType.RequestBody);
        //    IRestResponse response2 = client2.Execute(request2);
        //    if (response2.StatusCode.ToString() == "OK")
        //    {
        //        ServiceTypeDocumentData objResponseData = JsonConvert.DeserializeObject<ServiceTypeDocumentData>(response2.Content);
        //        if (objResponseData.ResponseCode == "001")
        //        {
        //            return RedirectToAction("SignOut", "Home");

        //        }
        //        else if (objResponseData.ResponseCode == "000")
        //        {
        //            ViewData["ServiceTypeList"] = objResponseData.Data.ListRequest;

        //        }
        //    }

        //    //var clientdoc = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetServiceTypeDocument?id=" + Id + "");
        //    //var requestdoc = new RestRequest(Method.GET);
        //    //requestdoc.AddHeader("cache-control", "no-cache");
        //    //requestdoc.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    //// request2.AddParameter("application/json", "", ParameterType.RequestBody);
        //    //IRestResponse responsedoc = clientdoc.Execute(requestdoc);
        //    //if (responsedoc.StatusCode.ToString() == "OK")
        //    //{
        //    //    ServiceTypeDocumentData objResponseData = JsonConvert.DeserializeObject<ServiceTypeDocumentData>(responsedoc.Content);
        //    //    if (objResponseData.ResponseCode == "001")
        //    //    {
        //    //        return RedirectToAction("SignOut", "Home");
        //    //    }
        //    //    else if (objResponseData.ResponseCode == "000")
        //    //    {
        //    //        ViewData["ServiceTypedocList"] = objResponseData.Data.ListRequest;

        //    //    }
        //    //}

        //    return View();
        //}

        #region For Datartment Show Distrct
        [HttpGet]
        public ActionResult GetDistrictType()
        {
            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Mastersddl/GetDistrictList");
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                //      MastersddlData objResponseData = JsonConvert.DeserializeObject<MastersddlData>(response2.Content);
                var objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
                List<Mastersddl> ListMasters = JsonConvert.DeserializeObject<List<Mastersddl>>(objResponseData.Data.ToString());
                if (objResponseData.ResponseCode == "001")
                {
                    return RedirectToAction("SignOut", "Home");
                }
                else if (objResponseData.ResponseCode == "000")
                {
                    //ViewData["District"] = new SelectList(objResponseData.Data.ListRequest, "ID", "Name"); 
                    ViewBag.Districtlist = ListMasters;
                }
            }
            return View();
        }
        #endregion

        #region Department List
        public ActionResult GetDepartmentServiceList()
        {
            CustomListRequest lst = new CustomListRequest();
            lst.Type = "ServiceTypeDepartment";
            List<CustomList> depLst = Common.GetCommonListData(lst);
            if (depLst == null)
            {
                return RedirectToAction("SignOut", "Home");
            }
            else
            {
                ViewBag.Categorylist = depLst;
            }

            //List<DepartmentTypeDocumentBO> ser = new List<DepartmentTypeDocumentBO>();
            //// List<Mastersddl> allServices = OneDigitalIdentity.Models.CommonUtils.GetAllServivcesList();//get data from application cache
            //// if (allServices == null)
            //// {
            //var client3 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetDepartmentServiceList");
            //var request3 = new RestRequest(Method.GET);
            //request3.AddHeader("cache-control", "no-cache");
            ////request3.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //request3.AddParameter("application/json", "", ParameterType.RequestBody);
            //IRestResponse response3 = client3.Execute(request3);
            //if (response3.StatusCode.ToString() == "OK")
            //{
            //    var objResponseData = JsonConvert.DeserializeObject<ResponseData>(response3.Content);
            //    List<DepartmentTypeDocumentBO> lstDocument = JsonConvert.DeserializeObject<List<DepartmentTypeDocumentBO>>(objResponseData.Data.ToString());
            //    if (objResponseData.ResponseCode == "001")
            //    {
            //        return RedirectToAction("SignOut", "Home");
            //    }
            //    else if (objResponseData.ResponseCode == "000")
            //    {
            //        // OneDigitalIdentity.Models.CommonUtils.SetServivcesList(objResponseData.Data.ListRequest);//save data into application cache
            //        //ViewData["Category"] = new SelectList(objResponseData.Data.ListRequest, "MstCategoryID", "Category");
            //       // var listitem = new SelectList(lstDocument, "MstCategoryID", "Category");

            //        ViewBag.Categorylist = lstDocument;
            //    }

            //}
            //else
            //{
            //}
            return View();
        }
        #endregion

        #region Services List
        public ActionResult GetService()
        {
            List<DepartmentTypeDocumentBO> ser = new List<DepartmentTypeDocumentBO>();
            var client3 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetServiceMappingList");
            var request3 = new RestRequest(Method.GET);
            request3.AddHeader("cache-control", "no-cache");
            // request3.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request3.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response3 = client3.Execute(request3);
            if (response3.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response3.Content);
                List<DepartmentTypeDocumentBO> objListService = JsonConvert.DeserializeObject<List<DepartmentTypeDocumentBO>>(objResponseData.Data.ToString());
                if (objResponseData.ResponseCode == "001")
                {
                    return RedirectToAction("SignOut", "Home");
                }
                else if (objResponseData.ResponseCode == "000")
                {
                    // OneDigitalIdentity.Models.CommonUtils.SetServivcesList(objResponseData.Data.ListRequest);//save data into application cache
                    if (objResponseData.Data != null)
                    {
                        //var listitem = new SelectList(objListService, "MstCategoryID", "Category");

                        ViewBag.Serviceslist = objListService;
                    }
                    else
                    {
                        ViewBag.Serviceslist = null;
                    }
                }

            }
            else
            {
            }
            return View();
        }
        #endregion
        public ActionResult AddServiceType()
        {
            GetDistrictType();
            GetDepartmentServiceList();
            GetService();

            CustomListRequest lst = new CustomListRequest();
            lst.Type = "ServiceType";  // Main Services
            List<CustomList> service = Common.GetCommonListData(lst);
            if (service == null)
            {
                ViewData["Service"] = service;
            }
            else
            {
                ViewData["Service"] = service;
            }


            //List<Mastersddl> ser = new List<Mastersddl>();
            //var client3 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Mastersddl/GeServiceList");
            //var request3 = new RestRequest(Method.GET);
            //request3.AddHeader("cache-control", "no-cache");
            ////request3.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            ////request3.AddParameter("application/json", "", ParameterType.RequestBody);
            //IRestResponse response3 = client3.Execute(request3);
            //if (response3.StatusCode.ToString() == "OK")
            //{
            //    MastersddlData objResponseData = JsonConvert.DeserializeObject<MastersddlData>(response3.Content);
            //    if (objResponseData.ResponseCode == "001")
            //    {
            //        return RedirectToAction("SignOut", "Home");
            //    }
            //    else if (objResponseData.ResponseCode == "000")
            //    {

            //        ViewData["Service"] = new SelectList(objResponseData.Data.ListMasters, "ID", "Name");
            //    }
            //    else
            //    {

            //        ser.Add(new Mastersddl() { Name = "-- Select Service --", ID = "" });
            //        ViewData["Service"] = new SelectList(ser, "ID", "Name", "");
            //    }
            //}
            //else
            //{

            //    ser.Add(new Mastersddl() { Name = "-- Select Service --", ID = "" });
            //    ViewData["Service"] = new SelectList(ser, "ID", "Name", "");
            //}
            return View();
        }
        [HttpPost]
        public ActionResult SaveDocumentType(ServiceTypeDocument objservice)
        {
            try
            {
                var CurrentSessions = (UserModelSession)Session["UserDetails"];
                objservice.CreatedByName = CurrentSessions.Username;
                objservice.CreatedByID = CurrentSessions.PartyId;
                objservice.CreatedIP = objcf.getIPAdd();
                objservice.Type = 2;
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/InsertServiceType");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", _JsonSerializer.Serialize(objservice), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("AddServiceType");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("AddServiceType");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("AddServiceType");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                return RedirectToAction("AddServiceType");
            }
            return RedirectToAction("AddServiceType");
        }
        [HttpPost]
        public ActionResult SaveServiceType(ServiceTypeDocument objservice)
        {
            try
            {
                var CurrentSessions = (UserModelSession)Session["UserDetails"];
                objservice.CreatedByName = CurrentSessions.Username;
                objservice.CreatedByID = CurrentSessions.PartyId;
                objservice.CreatedIP = objcf.getIPAdd();
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/InsertServiceType");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", _JsonSerializer.Serialize(objservice), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("AddServiceType");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("AddServiceType");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("AddServiceType");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                return RedirectToAction("AddServiceType");
            }
            return View();
        }
        [HttpPost]
        public ActionResult SaveServiceDocument(List<ServiceTypeDocument> stulist)
        {
            try
            {
                ServiceTypeDocument model = new ServiceTypeDocument();
                var CurrentSessions = (UserModelSession)Session["UserDetails"];
                model.CreatedByName = CurrentSessions.Username;
                model.CreatedByID = CurrentSessions.PartyId;

                model.CreatedIP = objcf.getIPAdd();
                model.ServiceList = stulist;
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/InsertServiceDocument");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //  request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return new JsonResult
                        {
                            Data = new { msg = "success", Errormsg = objResponseData.Message },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return new JsonResult
                        {
                            Data = new { msg = "warning", Errormsg = objResponseData.Message },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return new JsonResult
                        {
                            Data = new { msg = "error", Errormsg = objResponseData.Message },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                return new JsonResult
                {
                    Data = new { msg = "error", Errormsg = "Something wrong" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return View();
        }
        [HttpPost]
        public ActionResult SaveMappingServicesandChild(ServiceTypeDocument objservice)
        {

            try
            {
                if (objservice.Type == 0)
                {
                    objservice.ChildSubCategory = 0;
                }
                objservice.InputString = Request["MultiChildServiceName"];

                //objservice.CreatedByName = CurrentSessions.FullName;
                //objservice.CreatedByID = CurrentSessions.UserId;
                //objservice.CreatedIP = objcf.getIPAdd();
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/UpdateChildServices");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //  request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", _JsonSerializer.Serialize(objservice), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("AddServiceType");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("AddServiceType");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("AddServiceType");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                return RedirectToAction("AddServiceType");
            }
            return RedirectToAction("AddServiceType");
        }
        [HttpPost]
        public ActionResult SaveMappingDepartment(ServiceTypeDocument objservice)
        {
            try
            {
                objservice.ServiceName = Request["MultiServiceName"];
                if (objservice.ServiceName == null)
                {
                    return RedirectToAction("AddServiceType");
                }

                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/InsertServicedepartMapping");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", _JsonSerializer.Serialize(objservice), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("AddServiceType");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("AddServiceType");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("AddServiceType");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                return RedirectToAction("AddServiceType");
            }
            return RedirectToAction("AddServiceType");
        }
        #region RateList
        public JsonResult GetRateMasterList(int MstCategoryId, int SubMstCategoryId, int ChildCateId = 0)
        {
            RateMaster objCitizenReq = new RateMaster();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetRateMasterList?MstCategoryId=" + MstCategoryId + "&SubMstCategoryId=" + SubMstCategoryId + "&ChildCateId=" + ChildCateId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                RateMasterData objResponseData = JsonConvert.DeserializeObject<RateMasterData>(response.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
                ViewBag.MstCategoryId = MstCategoryId;
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion
        public JsonResult FillDocumentList(int Id, int ChildServiceId = 0)
        {
            CustomListRequest lst = new CustomListRequest();
            lst.Type = "AllDocument";  // Main Services
            lst.Id = Id;
            lst.StrId = ChildServiceId.ToString();
            List<CustomList> service = Common.GetCommonListData(lst);
            if (service == null)
            {
                return new JsonResult
                {
                    Data = new { Data = "", failure = true, msg = "Failed" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { Data = service, failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            //GetRateMasterList(int MstCategoryId,int SubMstCategoryId,int @ChildCateId);
            // var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetDocumentList?ID=" + Id + " &ChildId=" + ChildServiceId + "");
            // var request2 = new RestRequest(Method.GET);
            // request2.AddHeader("cache-control", "no-cache");
            //// request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            // request2.AddParameter("application/json", "", ParameterType.RequestBody);
            // IRestResponse response2 = client2.Execute(request2);
            // if (response2.StatusCode.ToString() == "OK")
            // {
            //     ServiceTypeDocumentData objResponseData = JsonConvert.DeserializeObject<ServiceTypeDocumentData>(response2.Content);
            //     if (objResponseData.ResponseCode == "001")
            //     {
            //         return new JsonResult
            //         {
            //             Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
            //             ContentEncoding = System.Text.Encoding.UTF8,
            //             JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //         };
            //     }
            //     else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
            //     {
            //         return new JsonResult
            //         {
            //             Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
            //             ContentEncoding = System.Text.Encoding.UTF8,
            //             JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //         };

            //     }
            // }
            // return new JsonResult
            // {
            //     Data = new { Data = "", failure = true, msg = "Failed" },
            //     ContentEncoding = System.Text.Encoding.UTF8,
            //     JsonRequestBehavior = JsonRequestBehavior.AllowGet
            // };
        }
        public JsonResult FillChildSubCategory(int Id)
        {
            CustomListRequest lst = new CustomListRequest();
            lst.Type = "AllSubServiceType";  // Main Services
            if (Id != 0 && Id != null)
                lst.Type = "SubServiceType";
            lst.Id = Id;
            List<CustomList> service = Common.GetCommonListData(lst);
            if (service == null)
            {
                return new JsonResult
                {
                    Data = new { Data = "", failure = true, msg = "Failed" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { Data = service, failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            // var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetChildSubCategory?ID=" + Id + "");
            // var request2 = new RestRequest(Method.GET);
            // request2.AddHeader("cache-control", "no-cache");
            //// request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            // request2.AddParameter("application/json", "", ParameterType.RequestBody);
            // IRestResponse response2 = client2.Execute(request2);
            // if (response2.StatusCode.ToString() == "OK")
            // {
            //     ServiceTypeDocumentData objResponseData = JsonConvert.DeserializeObject<ServiceTypeDocumentData>(response2.Content);
            //     if (objResponseData.ResponseCode == "001")
            //     {
            //         return new JsonResult
            //         {
            //             Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
            //             ContentEncoding = System.Text.Encoding.UTF8,
            //             JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //         };
            //     }
            //     else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
            //     {
            //         return new JsonResult
            //         {
            //             Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
            //             ContentEncoding = System.Text.Encoding.UTF8,
            //             JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //         };

            //     }
            // }
            // return new JsonResult
            // {
            //     Data = new { Data = "", failure = true, msg = "Failed" },
            //     ContentEncoding = System.Text.Encoding.UTF8,
            //     JsonRequestBehavior = JsonRequestBehavior.AllowGet
            // };
        }
        public JsonResult FillSubCategory(int Id)
        {
            CustomListRequest lst = new CustomListRequest();
            lst.Type = "ServiceType";  // Main Services
            lst.Id = Id;
            List<CustomList> service = Common.GetCommonListData(lst);
            if (service == null)
            {
                return new JsonResult
                {
                    Data = new { Data = "", failure = true, msg = "Failed" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { Data = service, failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            //   var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetServiceByDepartmentId?ID=" + Id + "");
            //   var request2 = new RestRequest(Method.GET);
            //   request2.AddHeader("cache-control", "no-cache");
            ////   request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //   request2.AddParameter("application/json", "", ParameterType.RequestBody);
            //   IRestResponse response2 = client2.Execute(request2);
            //   if (response2.StatusCode.ToString() == "OK")
            //   {
            //       ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
            //       ListServiceTypeDocumentData objData = JsonConvert.DeserializeObject<ListServiceTypeDocumentData>(objResponseData.Data.ToString());
            //       if (objResponseData.ResponseCode == "001")
            //       {
            //           return new JsonResult
            //           {
            //               Data = new { Data = objData, failure = false, msg = "Success" },
            //               ContentEncoding = System.Text.Encoding.UTF8,
            //               JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //           };
            //       }
            //       else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
            //       {
            //           return new JsonResult
            //           {
            //               Data = new { Data = objData.ListRequest.ToArray(), failure = false, msg = "Success" },
            //               ContentEncoding = System.Text.Encoding.UTF8,
            //               JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //           };

            //       }
            //   }
            //   return new JsonResult
            //   {
            //       Data = new { Data = "", failure = true, msg = "Failed" },
            //       ContentEncoding = System.Text.Encoding.UTF8,
            //       JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //   };
        }
        public JsonResult Change_Service(string Id)
        {
            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetServiceTypeDocument?id=" + Id + "");
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("cache-control", "no-cache");
            //   request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            // request2.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                ServiceTypeDocumentData objResponseData = JsonConvert.DeserializeObject<ServiceTypeDocumentData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
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
        public JsonResult GetServiceList()
        {
            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetServiceList");
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("cache-control", "no-cache");
            //   request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            // request2.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                ServiceTypeDocumentData objResponseData = JsonConvert.DeserializeObject<ServiceTypeDocumentData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
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
        public JsonResult FillService(string Type)
        {
            CustomListRequest lst = new CustomListRequest();
            lst.Type = Type;  // Main Services
            List<CustomList> service = Common.GetCommonListData(lst);
            if (service == null)
            {
                return new JsonResult
                {
                    Data = new { Data = "", failure = true, msg = "Failed" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { Data = service, failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }



            //var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetDataServiceList?ID=" + Type + "");
            //var request2 = new RestRequest(Method.GET);
            //request2.AddHeader("cache-control", "no-cache");
            ////   request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //// request2.AddParameter("application/json", "", ParameterType.RequestBody);
            //IRestResponse response2 = client2.Execute(request2);
            //if (response2.StatusCode.ToString() == "OK")
            //{
            //    ServiceTypeDocumentData objResponseData = JsonConvert.DeserializeObject<ServiceTypeDocumentData>(response2.Content);
            //    if (objResponseData.ResponseCode == "001")
            //    {
            //        return new JsonResult
            //        {
            //            Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
            //            ContentEncoding = System.Text.Encoding.UTF8,
            //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //        };
            //    }
            //    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
            //    {
            //        return new JsonResult
            //        {
            //            Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
            //            ContentEncoding = System.Text.Encoding.UTF8,
            //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //        };

            //    }
            //}
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult FillColorService(string Id)
        {
            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/GetDataServiceList?ID=" + Id + "");
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("cache-control", "no-cache");
            //   request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            // request2.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                ServiceTypeDocumentData objResponseData = JsonConvert.DeserializeObject<ServiceTypeDocumentData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
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
        #endregion
        [HttpPost]
        public ActionResult SaveMappingColor(ServiceTypeDocument objservice)
        {
            try
            {
                string Color = Request["Color"];
                string servicesName = Request["ColorServiceName"];
                int Buffer = Convert.ToInt32(Request["Buffer"]);
                objservice.ServiceName = Color;
                objservice.InputString = servicesName;
                objservice.Type = Buffer;
                var CurrentSessions = (UserModelSession)Session["UserDetails"];
                objservice.CreatedByName = CurrentSessions.Username;
                objservice.CreatedByID = CurrentSessions.PartyId;
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ServiceConfigure/UpdateColorServices");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", _JsonSerializer.Serialize(objservice), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = _JsonSerializer.Deserialize<ResponseData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("AddServiceType");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("AddServiceType");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("AddServiceType");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                return RedirectToAction("AddServiceType");
            }
            return RedirectToAction("AddServiceType");
        }
        #region UserRegistration
        // GET: LoginSignup
        string AdharPanToken = ConfigurationManager.AppSettings["AdharTokenForVerification"];

        public List<Dropdown> GetUserTypes()
        {
            List<Dropdown> dropdowns = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetUserType");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                dropdowns = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());

            }
            return dropdowns;
        }

        ResponseData objResponse;
        public ActionResult Index()
        {
            var userTypes = GetUserTypes();
            ViewBag.userTypesList = userTypes;

            var content = ViewContent();
            if (content != null && content.Count > 0)
            {
                ViewBag.TandC = content[0];
            }
            return View();
        }
        public JsonResult SaveDetails(PartyMaster party)
        {

            var json = JsonConvert.SerializeObject(party);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/Add");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult VerifyAdharPAN(string id_number, string url, string type)
        {
            AdharPANVerification t = new AdharPANVerification();
            t.id_number = id_number;
            var json = JsonConvert.SerializeObject(t);
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AdharPanToken);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            PANRoot data = new PANRoot();
            if (response.StatusCode.ToString() == "OK")
            {
                data = JsonConvert.DeserializeObject<PANRoot>(response.Content);
            }
            if (response.StatusCode.ToString() == "422")
            {
                data = JsonConvert.DeserializeObject<PANRoot>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = data.status_code, Data = data.data, ClientID = data.data.client_id, OperationType = type, Failure = false, Message = data.message_code },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult VerifyAdharWithOTP(string otp, string url, string client_id, string type)
        {
            AdharVerificationWithOTP t = new AdharVerificationWithOTP();
            t.otp = otp;
            t.client_id = client_id;

            var json = JsonConvert.SerializeObject(t);
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AdharPanToken);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            AadharFullDetails data = new AadharFullDetails();
            if (response.StatusCode.ToString() == "OK")
            {
                data = JsonConvert.DeserializeObject<AadharFullDetails>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = data.status_code, Data = data.data, Failure = false, Message = data.message_code },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult MobileVerify(string mobile, string messageContent)
        {
            MobileVerification verification = new MobileVerification();
            verification.Mobile = mobile;
            verification.Message = messageContent;

            var json = JsonConvert.SerializeObject(verification);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/VerifyMobile");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            MobileOTPResponse data = new MobileOTPResponse();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                data = JsonConvert.DeserializeObject<MobileOTPResponse>(objResponse.Data.ToString());
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = data, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public List<Services> GetServices()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetServices");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Services> service = new List<Services>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                service = JsonConvert.DeserializeObject<List<Services>>(objResponse.Data.ToString());
            }
            return service;
        }
        public JsonResult UpdateVerificationStatus(string partyId, string ColName, string colvalue, int status)
        {
            var Token = Session["Token"];
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UpdateVerification?PartyId=" + partyId + "&colName=" + ColName + "&colValue=" + colvalue + "&status=" + status);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult SaveAadhaarData(AadharWithPartyId aadhar)
        {
            if (aadhar.Aadhaar != null)
            {
                var json = JsonConvert.SerializeObject(aadhar);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SaveAadhaarData");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                }

                return new JsonResult
                {
                    Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, Message = objResponse.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        public JsonResult SavePurchase(AddPurchase purchase)
        {
            var json = JsonConvert.SerializeObject(purchase);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SavePurchaseData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, OrderId = objResponse.UserID, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult SavePurchaseAfterLogin(AddPurchase purchase)
        {
            var userdata = (UserModelSession)Session["UserDetails"];

            if (userdata != null)
            {
                purchase.PartyId = userdata.PartyId;
                purchase.OrderId = userdata.PartialOrderID;

                var json = JsonConvert.SerializeObject(purchase);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SavePurchaseData");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                }

                return new JsonResult
                {
                    Data = new { StatusCode = objResponse.statusCode, OrderId = objResponse.UserID, RegistrationNo = userdata.RegistrationNo, Data = objResponse, Failure = false, Message = objResponse.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { StatusCode = "-1", Data = "", Failure = false, Message = "Unable to fatch the data" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        public List<ContentPage> ViewContent(string Id = "5")
        {
            ContentPage content = new ContentPage();

            content.Id = Id;
            content.Type = "Data";

            var json = JsonConvert.SerializeObject(content);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/EditContentData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<ContentPage> mainData = new List<ContentPage>();

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                mainData = JsonConvert.DeserializeObject<List<ContentPage>>(objResponse.Data.ToString());
            }
            return mainData;
        }
        public JsonResult UploadPan(UploadDoc upload)
        {
            if (upload.Image != null)
            {
                var json = JsonConvert.SerializeObject(upload);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UploadPan");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                object status = 0;
                if (response.StatusCode.ToString() == "OK")
                {
                    status = JsonConvert.DeserializeObject(response.Content);
                }

                return new JsonResult
                {
                    Data = new { StatusCode = 000, Failure = false, Message = "Pan Uploaded Successfully" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { StatusCode = 0, Failure = false, Message = "Image Can`t Be Null" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        public JsonResult UploadAadhaar(UploadDoc upload)
        {
            if (upload.Image != null)
            {
                var json = JsonConvert.SerializeObject(upload);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UploadAadhar");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                object status = 0;
                if (response.StatusCode.ToString() == "OK")
                {
                    status = JsonConvert.DeserializeObject(response.Content);
                }

                return new JsonResult
                {
                    Data = new { StatusCode = 000, Failure = false, Message = "Aadhaar Uploaded Successfully" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { StatusCode = 0, Failure = false, Message = "Image Can`t Be Null" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        #endregion


    }
}