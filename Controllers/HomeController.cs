using Newtonsoft.Json;
using NewZapures_V2.Helper;
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
using static NewZapures_V2.Models.Common;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
  //  [NoDirectAccess]
    public class HomeController : Controller
    {
        // GET: Home
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        public ResponseData ObjResponse = null;

        //public ActionResult Profile(string Id= "R000001")
        //{
        //   PartyMaster objCaterMastermaster = new PartyMaster();
        //    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/GetPartyDetail?PartyId=" + Id);
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("cache-control", "no-cache");
        //    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    request.AddParameter("application/json", "", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //   if (response.StatusCode.ToString() == "OK")
        //    {
        //        var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);

        //        //var data = _JsonSerializer.Deserialize<CategoryMaster>(response.Contentd);
        //        //CategoryMasterData objResponseData = _JsonSerializer.Deserialize<CategoryMasterData>(response.Content);
        //        //objUsermaster = objResponseData.ListRequest;

        //        if (d.ResponseCode == "001")
        //        {
        //            return RedirectToAction("SignOut", "Home");
        //        }
        //        else if (d.ResponseCode == "000" && d.Data != null)
        //        {
        //            objCaterMastermaster = JsonConvert.DeserializeObject<List<PartyMaster>>(d.Data.ToString()).FirstOrDefault();
        //            //  objCaterMastermaster =  JsonConvert.DeserializeObject<List<ShowRateMaster>>(objResponseData.ToString());
        //        }
        //    }
        //    return View(objCaterMastermaster);
        //}

        public ActionResult MyProfile(string Id = "")
        {
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            if (servicesCollection != null)
            {
                PartyMaster objCaterMastermaster = new PartyMaster();
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/GetPartyDetail?PartyId=" + servicesCollection.PartyId);
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
                        objCaterMastermaster = JsonConvert.DeserializeObject<List<PartyMaster>>(d.Data.ToString()).FirstOrDefault();
                        // objCaterMastermaster =  JsonConvert.DeserializeObject<List<ShowRateMaster>>(objResponseData.ToString());
                    }
                }

                var companyType = GetCompanyType();

                ViewBag.UserID = servicesCollection.PartyId;
                ViewBag.companyType = companyType;
                return View(objCaterMastermaster);
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }

        public ActionResult GetDocuments(string DocumentList = "DocumentListProfile", int companyId = 0)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetDocumentLibraryData?Type=" + DocumentList + "&companyId=" + companyId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<DocumentLibraryMaster> d = new List<DocumentLibraryMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
               var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    d = JsonConvert.DeserializeObject<List<DocumentLibraryMaster>>(objResponse.Data.ToString());
                }
                else
                {
                    d = null;
                }
            }
            return View(d);
        }



        public ActionResult AddUser(string Id="")
        {
            var servicesCollection = (Data)Session["UserAllDetails"];
            if (servicesCollection.userDetails != null)
            {
                ViewBag.UserID = servicesCollection.userDetails.partyId;
                PartyMaster partyMaster = new PartyMaster();
                return View(partyMaster);
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        public ActionResult EditProfile(PartyMaster master)
        {
            int TypeId = Convert.ToInt32(Request["Type1"]);
            master.Type = TypeId;

            var servicesList = (Data)Session["UserAllDetails"];
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/UpdatePartyMaster");
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
                        //TempData["SwalStatusMsg"] = "success";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("Finalpage", "Welcome");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("Finalpage", "Welcome");
                    }
                    else
                    {
                        //TempData["SwalStatusMsg"] = "error";
                        //TempData["SwalMessage"] = "Something wrong";
                        //TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("Finalpage", "Welcome");
                    }
                }
            }
            catch (Exception ex)
            {
                //TempData["SwalStatusMsg"] = "error";
                //TempData["SwalMessage"] = "Something wrong";
                //TempData["SwalTitleMsg"] = "error!";
                return RedirectToAction("Finalpage", "Welcome");
            }
            return RedirectToAction("Finalpage", "Welcome");
        }
        public ActionResult ProfileView(string Id,int status)
        {
            var base64string = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(Id));

            byte[] data = Convert.FromBase64String(base64string);
            string UserId = System.Text.Encoding.UTF8.GetString(data);
            PartyMaster objCaterMastermaster = new PartyMaster();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/GetPartyDetail?PartyId=" + UserId);
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
                    objCaterMastermaster = JsonConvert.DeserializeObject<List<PartyMaster>>(d.Data.ToString()).FirstOrDefault();
                    //  objCaterMastermaster =  JsonConvert.DeserializeObject<List<ShowRateMaster>>(objResponseData.ToString());
                }
            }
            List<Documentlist> documentlists = new List<Documentlist>();
            documentlists = Common.GetUploadDocumentlist(UserId);
            ViewBag.Documentlist = documentlists;
            ViewBag.status = status;
            return View(objCaterMastermaster);
        }

        public ActionResult RegistratedUser()
        {

            List<RegistratedUser> registratedUsers = new List<RegistratedUser>();
            registratedUsers = Common.GetRegistratedUsers();
            ViewBag.RegistratedUser = registratedUsers;
            return View();

        }
        public ActionResult PaymentDetail(string Id)
        {
         
            byte[] data = Convert.FromBase64String(Id);
            string userid = System.Text.Encoding.UTF8.GetString(data);
            List<ACtivedServicesHardwarelist> servicesHardwarelists = new List<ACtivedServicesHardwarelist>();
            servicesHardwarelists = Common.GetActivedServicesandHardwareList(userid);
            List<PayTracking> payslist = new List<PayTracking>();
            payslist = Common.GetPayTrackings(userid);
            ViewBag.payslist = payslist;
            return View(servicesHardwarelists);
        }
        public JsonResult upload(string image)
        {
            var image_array_1 = image.Split(';');
            var image_array_2 = image_array_1[1].Split(',');
            string img = image_array_2[1];
            string strm = img;

            var bytess = Convert.FromBase64String(strm);
            var servicesCollection  = (UserModelSession)Session["UserDetails"];

            UploadDoc doc = new UploadDoc();

            doc.Image = bytess;
            doc.PartyId = servicesCollection.PartyId;

            var json = JsonConvert.SerializeObject(doc);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UploadProfileImage");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<Settings> settings = new List<Settings>();

            if (response.StatusCode.ToString() == "OK")
            {
                ObjResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                servicesCollection.profileImage = ObjResponse.Body;
            }

            return new JsonResult
            {
                Data = new { StatusCode = ObjResponse.statusCode, Data = ObjResponse.Body, Failure = false, Message = ObjResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };




            


            ////var myfilename = string.Format(@"{0}", Guid.NewGuid());
            //string FilesName = "Zapurse_" + Guid.NewGuid().ToString() + ".jpeg";
            //string fname = HttpContext.Server.MapPath("~/images/Docs/" + FilesName);
            ////string filepath = "/Docs/Banners/abhishek" + myfilename + ".jpeg";
            //var bytess = Convert.FromBase64String(strm);
            ////File.WriteAllBytes(filepath, Convert.FromBase64String(strm));

            //using (var imageFile = new FileStream(fname, FileMode.Create))
            //{
            //    imageFile.Write(bytess, 0, bytess.Length);
            //    imageFile.Flush();
            //}
            //Documentlist obj = new Documentlist();
            //obj.DocumentName = FilesName;
            //ObjResponse = new ResponseData
            //{
            //    statusCode = 1,
            //    imagename = FilesName
            //};

            return Json(ObjResponse, JsonRequestBehavior.AllowGet);
            //return new JsonResult
            //{
            //    Data = new { Data = obj, failure = false, msg = "Success", isvalid = 1 },
            //    ContentType = FilesName,
            //    RecursionLimit=1,
            //    ContentEncoding = System.Text.Encoding.UTF8,
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //};


        }

        #region Payment Collection
        public ActionResult PaymentPendingCollection()
        {
            List<Models.PandingPaymentList> _result = new List<Models.PandingPaymentList>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/PaymentPendingList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<Models.PandingPaymentList>>(response.Content);
                ViewData["PendingList"] = _result;

            }
            return View();
        }

        [HttpGet]
        //public ActionResult AddPaymentCollect(string PartyId,string OrderId,int PayTrackingId)
        public ActionResult AddPaymentCollect(PandingPaymentList req)
        {
            Models.PartyPaymentCollect viewModal = new Models.PartyPaymentCollect();
            Models.PandingPaymentList modal = new Models.PandingPaymentList();
            modal.PartyId = req.PartyId;
            modal.OrderId = req.OrderId;
            modal.PayTrackingId = req.PayTrackingId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetPartyPaymentCollection");
            var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json;");
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                viewModal = _JsonSerializer.Deserialize<Models.PartyPaymentCollect>(response.Content);
                viewModal.partsummary.OrderId = req.OrderId;
                //viewModal.collectMoney.PayTrackingId = PayTrackingId;
            }
            return PartialView("_PaymentCollection", viewModal);
        }
        [HttpPost]
        public ActionResult AddCollectMoney(PaymentCollectionModel modal)
        {
            ResponseData _result = new ResponseData();
            //PaymentCollectionModel _request = new PaymentCollectionModel();
            //_request.payTrackingId = modal.partsummary.payTrackingId;
            //_request.PartyId = modal.partsummary.PartyId;
            //_request.PaymentMode = modal.collectMoney.PaymentMode;
            //_request.ChequeNumber = modal.collectMoney.ChequeNumber;
            //_request.Bank = modal.collectMoney.Bank;
            //_request.AccountNumber = modal.collectMoney.AccountNumber;
            //_request.CollectedAmt = modal.collectMoney.CollectedAmt;
            //_request.BalanceAmt = modal.collectMoney.BalanceAmt;
            //_request.OrderId = modal.partsummary.OrderId;
            //_request.RecivedBy = modal.collectMoney.RecivedBy;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/SavePaymentCollection");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json;");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<Models.ResponseData>(response.Content);
                if (_result.ResponseCode == "1")
                {
                    bool IsSuccess = true;
                    //string redirectUrl = string.Format("Home/PaymentPendingCollection");
                    //return NewtonSoftJsonResult(new RequestOutcome<string> { IsSuccess = true, Data = "", RedirectUrl = redirectUrl });
                    return Json(IsSuccess, JsonRequestBehavior.AllowGet);
                    //return RedirectToAction("PaymentPendingCollection");
                    //TempData["Message"] = "Success";
                    //ViewBag.Message = "Success";
                    //return RedirectToAction("PaymentPendingCollection");
                    //return null;
                }
            }
            return NewtonSoftJsonResult(new { IsSuccess = false, Data = "Collect Money were not saved" });
            //return null;
            //return null;
        }

        public ActionResult ChequeClearenceList()
        {
            List<Models.PandingPaymentList> _result = new List<Models.PandingPaymentList>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ChequePendingList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<Models.PandingPaymentList>>(response.Content);
                ViewData["PendingList"] = _result;

            }
            return View();
        }
        public ActionResult PendingCheque()
        {
            List<Models.PandingPaymentList> _result = new List<Models.PandingPaymentList>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "api/Registartion/PaymentPendingList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<Models.PandingPaymentList>>(response.Content);
                //CustomMasterData ls = new CustomMasterData();
                ViewData["PendingList"] = _result;

            }
            return View();
            //return PartialView("_PaymentCollection", assessmentModel);
        }

        public List<Dropdown> GetCompanyType(string CompanyType = "CompanyType")
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetDocumentLibraryData?Type=" + CompanyType);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> d = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                d = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return d;
        }
        public JsonResult ClearCheque(string PartyId, string OrderId, int PayTrackingId)
        {
            Models.PartyPaymentCollect viewModal = new Models.PartyPaymentCollect();
            Models.PandingPaymentList modal = new Models.PandingPaymentList();
            modal.PartyId = PartyId;
            modal.OrderId = OrderId;
            modal.PayTrackingId = PayTrackingId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ChequeClear");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json;");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                viewModal = _JsonSerializer.Deserialize<Models.PartyPaymentCollect>(response.Content);
                //viewModal.partsummary.OrderId = OrderId;
                return new JsonResult
                {
                    Data = new { Data = "", failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "error" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return PartialView("_PaymentCollection", viewModal);

        }

        public ActionResult ChequeClearedList()
        {
            List<Models.PandingPaymentList> _result = new List<Models.PandingPaymentList>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ChequeClearedList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<Models.PandingPaymentList>>(response.Content);
                ViewData["PendingList"] = _result;

            }
            return View();
        }

        public JsonResult ClearUnCheque(string PartyId, string OrderId, int PayTrackingId)
        {
            Models.PartyPaymentCollect viewModal = new Models.PartyPaymentCollect();
            Models.PandingPaymentList modal = new Models.PandingPaymentList();
            modal.PartyId = PartyId;
            modal.OrderId = OrderId;
            modal.PayTrackingId = PayTrackingId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ChequeUnClear");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json;");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                viewModal = _JsonSerializer.Deserialize<Models.PartyPaymentCollect>(response.Content);
                //viewModal.partsummary.OrderId = OrderId;
                return new JsonResult
                {
                    Data = new { Data = "", failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "error" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return PartialView("_PaymentCollection", viewModal);

        }

        public JsonResult CancelCheque(string PartyId, string OrderId, int PayTrackingId, string Reason)
        {
            Models.PartyPaymentCollect viewModal = new Models.PartyPaymentCollect();
            Models.PandingPaymentList modal = new Models.PandingPaymentList();
            modal.PartyId = PartyId;
            modal.OrderId = OrderId;
            modal.PayTrackingId = PayTrackingId;

            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ChequeCancel");
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json;");
            //request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                viewModal = _JsonSerializer.Deserialize<Models.PartyPaymentCollect>(response.Content);
                //viewModal.partsummary.OrderId = OrderId;
                return new JsonResult
                {
                    Data = new { Data = "", failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "error" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
            //return PartialView("_PaymentCollection", viewModal);

        }


        public JsonResult GetAllSettings(string Id = null)
        {
          //  var json = JsonConvert.SerializeObject(reset);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetAllSettings?Id="+ Id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<Settings> settings = new List<Settings>();

            if (response.StatusCode.ToString() == "OK")
            {
                ObjResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                settings = JsonConvert.DeserializeObject<List<Settings>>(ObjResponse.Data.ToString());
            }

            return new JsonResult
            {
                Data = new { StatusCode = ObjResponse.statusCode, Data = settings, Failure = false, Message = ObjResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult UpdateSetting(Settings settings)
        {
            var json = JsonConvert.SerializeObject(settings);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UpdateSetting");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Settings> searchedData = new List<Settings>();

            if (response.StatusCode.ToString() == "OK")
            {
                
                ObjResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if(settings.Type=="Search")
                {
                    searchedData = JsonConvert.DeserializeObject<List<Settings>>(ObjResponse.Data.ToString());
                }
            }

            return new JsonResult
            {
                Data = new { StatusCode = ObjResponse.statusCode, Data = searchedData, Failure = false, Message = ObjResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        #endregion Payment Collect Process

        #region "Serialization"
        public ActionResult NewtonSoftJsonResult(object data)
        {
            return new JsonResult
            {
                Data = data,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion


        #region SignOut
        public ActionResult SignOut()
        {
            Session["UserDetails"] = null;
            Session["Token"] = null;
            Session["UserAllDetails"] = null;

            return View();
        }
        public JsonResult SignOut1()
        {
            Session["UserDetails"] = null;
            Session["Token"] = null;
            Session["UserAllDetails"] = null;

            return new JsonResult
            {
                Data = new { StatusCode=1, Data = "Logged out successfully", failure = false, msg = "Success" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion

        #region Add Client Change Request
        public ActionResult ClientChangeRequest()
        {
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            ViewBag.Type = servicesCollection.Type;
            var Token = (string)Session["Token"];
            #region GetState List
            List<Dropdown> data = new List<Dropdown>();
            data = Common.GeAllData("State", 1, 0, 0, 0, Token);
            ViewData["State"] = data.ToList();
            ViewData["State"] = data.Select(m => new SelectListItem { Text = m.Text, Value = m.Id.ToString() }).ToList();
            #endregion
            #region change Request
            List<SelectListItem> _SequestFor = new List<SelectListItem>();
            _SequestFor.Add(new SelectListItem { Text = "My Parent", Value = "1" });
            _SequestFor.Add(new SelectListItem { Text = "My Level", Value = "2" });
            ViewData["SequestFor"] = new SelectList(_SequestFor, "Value", "Text");

            List<SelectListItem> _DoYouNowFor = new List<SelectListItem>();
            _DoYouNowFor.Add(new SelectListItem { Text = "Yes", Value = "1" });
            _DoYouNowFor.Add(new SelectListItem { Text = "No", Value = "2" });
            ViewData["DoYouNowFor"] = new SelectList(_DoYouNowFor, "Value", "Text");
            #endregion

            #region VendorType
            ListRequest obj = new ListRequest();
            //obj.PartyID = servicesCollection.userDetails.partyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/VendorType");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", servicesCollection.PartyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                if (LoginObj.ResponseCode == "1")
                {
                    ViewData["PartyType"] = new SelectList(LoginObj.Data, "ListID", "ListName");
                }
                else if (LoginObj.ResponseCode == "0")
                {
                    return RedirectToAction("login-alt", "authentication");
                }
                else
                {
                    ViewData["PartyType"] = null;
                }

            }
            else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
            {
                servicesCollection.PartyId = "";
                TempData["Type"] = "warning";
                TempData["Message"] = "Session Expired! Please login again.";
                TempData["Flag"] = "-1";
                return RedirectToAction("login-alt", "authentication");
            }
            else
            {
                TempData["Type"] = "error";
                TempData["Message"] = "Details Not Found-Ex! Please try after sometime.";
            }
            #endregion           
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string PartyId = userdetailsSession.PartyId;
            ChangeRquestDataList _result = new ChangeRquestDataList();
            client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ChangeRequestList?PartyId=" + PartyId);
            request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<ChangeRquestDataList>(response.Content);
            }
            return View(_result);
        }

        [HttpPost]
        public ActionResult ClientChangeRequest(ChangeRquestDataList _request)
        {
            var servicesCollection = (Data)Session["UserDetails"];
            var Token = (string)Session["Token"];
            #region GetState List
            List<Dropdown> data = new List<Dropdown>();
            data = Common.GeAllData("State", 1, 0, 0, 0, Token);
            ViewData["State"] = data.ToList();
            ViewData["State"] = data.Select(m => new SelectListItem { Text = m.Text, Value = m.Id.ToString() }).ToList();
            #endregion
            #region change Request
            List<SelectListItem> _SequestFor = new List<SelectListItem>();
            _SequestFor.Add(new SelectListItem { Text = "My Parent", Value = "1" });
            _SequestFor.Add(new SelectListItem { Text = "My Level", Value = "2" });
            ViewData["SequestFor"] = new SelectList(_SequestFor, "Value", "Text");

            List<SelectListItem> _DoYouNowFor = new List<SelectListItem>();
            _DoYouNowFor.Add(new SelectListItem { Text = "Yes", Value = "1" });
            _DoYouNowFor.Add(new SelectListItem { Text = "No", Value = "2" });
            ViewData["DoYouNowFor"] = new SelectList(_DoYouNowFor, "Value", "Text");
            #endregion

            #region VendorType
            ListRequest obj = new ListRequest();
            obj.PartyID = servicesCollection.userDetails.partyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/VendorType");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", servicesCollection.userDetails.partyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                if (LoginObj.ResponseCode == "1")
                {
                    ViewData["PartyType"] = new SelectList(LoginObj.Data, "ListID", "ListName");
                }
                else if (LoginObj.ResponseCode == "0")
                {
                    return RedirectToAction("login-alt", "authentication");
                }
                else
                {
                    ViewData["PartyType"] = null;
                }
                //TempData["Type"] = CommonFunctions.getResponseType(LoginObj.ResponseCode);

            }
            else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
            {
                servicesCollection.userDetails.partyId = "";
                TempData["Type"] = "warning";
                TempData["Message"] = "Session Expired! Please login again.";
                TempData["Flag"] = "-1";
                return RedirectToAction("login-alt", "authentication");
            }
            else
            {
                TempData["Type"] = "error";
                TempData["Message"] = "Details Not Found-Ex! Please try after sometime.";
            }
            #endregion           
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string PartyId = userdetailsSession.PartyId;
            ChangeRquestDataList _result = new ChangeRquestDataList();
            client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ChangeRequestList?PartyId=" + PartyId);
            request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<ChangeRquestDataList>(response.Content);
            }
            return View(_result);
        }

        public JsonResult FillGeographicalData(string Type, int CountryId = 0, int StateId = 0, int DistrictId = 0, int City = 0)
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = (string)Session["Token"];
            List<Dropdown> data = new List<Dropdown>();
            data = Common.GeAllData(Type, CountryId, StateId, DistrictId, City, Token);

            return new JsonResult
            {
                Data = new { Data = data, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public ActionResult SendCliectChangeRequest(string Id, string RequestFor, string doyouknowresource)
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string PartyId = userdetailsSession.PartyId;

            string ParentId = Id;
            ChangeRquestDataList _result = new ChangeRquestDataList();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/SaveChangeRequest?PartyId=" + PartyId + "&ParentId=" + ParentId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<ChangeRquestDataList>(response.Content);

            }
            return RedirectToAction("ClientChangeRequest");
        }

        public ActionResult SaveChangeRequest(string Id = "", string RequestFor = "", string doyouknowresource = "")
        {
            //string PartyId = "R000001";
            //string PartyId = "R000003"; // get session value

            //if (Id == "" && RequestFor == "" && doyouknowresource == "")
            //{
            //    RequestFor = "My Level";
            //}

            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string PartyId = userdetailsSession.PartyId;

            string ParentId = Id;
            ChangeRquestDataList _result = new ChangeRquestDataList();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/SaveChangeRequest?PartyId=" + PartyId + "&ParentId=" + ParentId + "&RequestFor=" + RequestFor + "&doyouknowresource=" + doyouknowresource);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<ChangeRquestDataList>(response.Content);
                // ViewData["PendingList"] = _result;
            }
            return RedirectToAction("ClientChangeRequest");
        }

        public ActionResult GetVenderDetail(ChangeRquestDataList res)
        {
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            var Token = (string)Session["Token"];
            //#region GetState List
            //List<Dropdown> data = new List<Dropdown>();
            //data = Common.GeAllData("State", 1, 0, 0, 0, Token);
            //ViewData["State"] = data.ToList();
            //ViewData["State"] = data.Select(m => new SelectListItem { Text = m.Text, Value = m.Id.ToString() }).ToList();
            //#endregion
            //#region change Request
            //List<SelectListItem> _SequestFor = new List<SelectListItem>();
            //_SequestFor.Add(new SelectListItem { Text = "My Parent", Value = "1" });
            //_SequestFor.Add(new SelectListItem { Text = "My Level", Value = "2" });
            //ViewData["SequestFor"] = new SelectList(_SequestFor, "Value", "Text");

            //List<SelectListItem> _DoYouNowFor = new List<SelectListItem>();
            //_DoYouNowFor.Add(new SelectListItem { Text = "Yes", Value = "1" });
            //_DoYouNowFor.Add(new SelectListItem { Text = "No", Value = "2" });
            //ViewData["DoYouNowFor"] = new SelectList(_DoYouNowFor, "Value", "Text");
            //#endregion

            #region VendorType
            ListRequest obj = new ListRequest();
            obj.PartyID = servicesCollection.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/VendorType");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", servicesCollection.PartyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                if (LoginObj.ResponseCode == "1")
                {
                    ViewData["PartyType"] = new SelectList(LoginObj.Data, "ListID", "ListName");
                }
                else if (LoginObj.ResponseCode == "0")
                {
                    return RedirectToAction("login-alt", "authentication");
                }
                else
                {
                    ViewData["PartyType"] = null;
                }

            }
            else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
            {
                servicesCollection.PartyId = "";
                TempData["Type"] = "warning";
                TempData["Message"] = "Session Expired! Please login again.";
                TempData["Flag"] = "-1";
                return RedirectToAction("login-alt", "authentication");
            }
            else
            {
                TempData["Type"] = "error";
                TempData["Message"] = "Details Not Found-Ex! Please try after sometime.";
            }
            #endregion

            #region getPartyDetailsList  
            string PartyId = servicesCollection.PartyId;
            ChangeRquestDataList _result = new ChangeRquestDataList();
            client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ChangeRequestList?PartyId=" + PartyId + "&PartyType=" + res.PartyType);
            request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", servicesCollection.PartyId);
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<ChangeRquestDataList>(response.Content);

            }
            #endregion
            return View("ClientChangeRequest", _result);
        }
        public JsonResult GetPartyList(string PartyType)
        {
            try
            {
                var servicesCollection = (UserModelSession)Session["UserDetails"];
                #region getPartyDetailsList  
                string PartyId = servicesCollection.PartyId;
                ChangeRquestDataList _result = new ChangeRquestDataList();
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/PartyList?PartyId=" + PartyId + "&PartyType=" + PartyType);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    _result = _JsonSerializer.Deserialize<ChangeRquestDataList>(response.Content);
                }
                #endregion
                return Json("", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
            }
            return Json("", JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetActiveParentList(string ParentName, string ParentDataName)
        {
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            SwitchVendorRequest obj = new SwitchVendorRequest();
            obj.PartyId = servicesCollection.PartyId;
            obj.Type = ParentName;
            obj.ParentID = ParentDataName;
            #region GetParentList
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/GetParentList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", servicesCollection.PartyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
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

        public JsonResult GetParentDetail(string ParentPartyId)
        {
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            SwitchVendorRequest obj = new SwitchVendorRequest();
            obj.PartyId = servicesCollection.PartyId;
            obj.ParentID = ParentPartyId;
            #region GetParentList
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/GetVendorDetail");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", servicesCollection.PartyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ClientRequestData LoginObj = _JsonSerializer.Deserialize<ClientRequestData>(response.Content);
                return new JsonResult
                {
                    Data = LoginObj,
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

        public JsonResult GetPartyTypeList(ListRequest obj)
        {
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            //ListRequest obj = new ListRequest();
            //obj.CangeRequestType = ChangeType;
            obj.PartyID = servicesCollection.PartyId;
            #region GetParentList
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/GetPartyTypeList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", servicesCollection.PartyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
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

        #region Add Client ChangeRequestList
        public ActionResult ClientChangeRequestList()
        {

            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string PartyId = userdetailsSession.PartyId;
            List<Models.ClientSwitchRequestData> _result = new List<Models.ClientSwitchRequestData>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/GetPendingchangeRequestList?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("LoginID", userdetailsSession.PartyId);
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
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string LoginId = userdetailsSession.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/RejectClientRequest?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("LoginID", userdetailsSession.PartyId);
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
                    return RedirectToAction("ClientChangeRequestList");
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    return RedirectToAction("ClientChangeRequestList");
                }
            }
            return RedirectToAction("ClientChangeRequestList");
        }

        public ActionResult ApprovedChangeRequest(string PartyId)
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            string LoginId = userdetailsSession.PartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/ApprovedChangeRequest?PartyId=" + PartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("LoginID", userdetailsSession.PartyId);
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
                    return RedirectToAction("ClientChangeRequestList");
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    return RedirectToAction("ClientChangeRequestList");
                }
            }
            return RedirectToAction("ClientChangeRequestList");
        }
        
        #endregion
    }
}