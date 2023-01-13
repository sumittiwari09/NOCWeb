using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using static NewZapures_V2.Models.TrusteeBO;

namespace NewZapures_V2.Controllers
{
    public class TrusteeController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        // GET: Trustee
        public ActionResult Index()
        {
            List<CustomMaster> TrustList = new List<CustomMaster>();

            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;


            List<CustomMaster> RoleType = new List<CustomMaster>();
            RoleType = Common.GetCustomMastersList(29);
            ViewBag.RoleType = RoleType;

            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrusteeList?TrustId="+SessionModel.TrustId.ToString());
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.Trustee> _result = _JsonSerializer.Deserialize<List<TrusteeBO.Trustee>>(response.Content);
                if (_result != null)
                {
                    ViewBag.TrusteeList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }

        [HttpPost]
        public ActionResult Index(TrusteeBO.Trustee obj, HttpPostedFileBase aadhaarfile, HttpPostedFileBase panfile, HttpPostedFileBase profilefile,HttpPostedFile Authfile)
        {
            obj.TrustInfoId = SessionModel.TrustId;
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Aadhaar
            if (aadhaarfile != null)
            {
                byte[] AadharDocumentbyte;
                extension = Path.GetExtension(aadhaarfile.FileName);
                ContentType = aadhaarfile.ContentType;
                using (Stream inputStream = aadhaarfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Aadhaar = Convert.ToBase64String(Documentbyte);
                    obj.AadhaarExtension = extension;
                    obj.AadhaarContentType = ContentType;
                }
            }
            #endregion
            #region Pan
            if (panfile != null)
            {
                extension = Path.GetExtension(panfile.FileName);
                ContentType = panfile.ContentType;
                using (Stream inputStream = panfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Pan = Convert.ToBase64String(Documentbyte);
                    obj.PanExtension = extension;
                    obj.PanContentType = ContentType;
                }
            }
            #endregion
            #region Profile
            if (profilefile != null)
            {
                extension = Path.GetExtension(profilefile.FileName);
                ContentType = profilefile.ContentType;
                using (Stream inputStream = profilefile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.ProfileImg = Convert.ToBase64String(Documentbyte);
                    obj.ProfileExtension = extension;
                    obj.ProfileContentType = ContentType;
                }
            }
            #endregion
            #region Authfile
            if (Authfile != null)
            {
                byte[] AadharDocumentbyte;
                extension = Path.GetExtension(Authfile.FileName);
                ContentType = aadhaarfile.ContentType;
                using (Stream inputStream = Authfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Authorized = Convert.ToBase64String(Documentbyte);
                    obj.AuthorizedExtension = extension;
                    obj.AuthorizedContentType = ContentType;
                }
            }
            #endregion
            #region Add Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddTrustee");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = "Data saved sussessfully!";
                    TempData["SwalTitleMsg"] = "Success...!";
                    //return RedirectToAction("Index");
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            #region List Trustee
            client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrusteeList?TrustId="+obj.TrustInfoId);
            request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.Trustee> _result = _JsonSerializer.Deserialize<List<TrusteeBO.Trustee>>(response.Content);
                if (_result != null)
                {
                    ViewBag.TrusteeList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return RedirectToAction("Index");
        }

        public ActionResult DeleteTrustMemeber(int Id)
        {
            #region Delete Trust Memeber
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/DeleteTrustMemeber?Id=" + Id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
            if (objResponseData.ResponseCode == "1")
            {
                TempData["SwalStatusMsg"] = "success";
                TempData["SwalMessage"] = "Deleted Successfully!!";
                TempData["SwalTitleMsg"] = "Success...!";
                //return RedirectToAction("Index");
            }
            else
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                //return RedirectToAction("Index");
            }
            #endregion
            return RedirectToAction("Index");
        }
        public ActionResult DownloadDocuments(int id)
        {
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/DocumentDetail?Id=" + id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                TrusteeBO.Trustee _result = _JsonSerializer.Deserialize<TrusteeBO.Trustee>(response.Content);
                if (_result != null)
                {
                    System.Web.HttpResponse Response = System.Web.HttpContext.Current.Response;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    string[] ps = _result.AadhaarContentType.Split('/');
                    string ContentType = ps[1];
                    Response.ContentType = _result.AadhaarContentType;
                    Response.AppendHeader("content-disposition", "attachment; filename=MPR." + ContentType);
                    byte[] fileBytes = Convert.FromBase64String(_result.Aadhaar);
                    Response.BinaryWrite(fileBytes);
                    Response.Flush();
                    Response.End();
                    return View();
                    //return RedirectToAction("Index");
                }
            }
            return View();
            #endregion
            //m_RequestMPRInternDetail item = Context.m_RequestMPRInternDetail.Where(s => s.RequestMPRInternDetailId == id).FirstOrDefault();

        }
        public ActionResult DraftApplication()
        {
            var draftApplications = ZapurseCommonlist.GetDraftApplication();
            ViewBag.draftApplication = draftApplications;
            return View();
        }
        
        public ActionResult UploadFeeRecipt()
        {
            var draftApplications = ZapurseCommonlist.GetApplicationsToUploadReceipt();
            ViewBag.draftApplication = draftApplications;
            return View();
        }

        public JsonResult CancelDraftApplication(string applGUID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/CancleDarftApplications?applGUID=" + applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
        public JsonResult UploadReceipt(UploadReceipt receipt)
        {
            var json = JsonConvert.SerializeObject(receipt);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/UploadReceipt");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        //public ActionResult EditApplication(string applicationNo, string trustName, int trustID, string clgName, string dptname, string cours, int deptID, int courseID,int clgID)
        public ActionResult EditApplication(string applGUID)
        
        {           
            var EditdraftedApplications = ZapurseCommonlist.GetDraftApplication(applGUID);
            ViewBag.applicationDetails = EditdraftedApplications[0];
            var trusteeMember = ZapurseCommonlist.GetTrusteeMember(EditdraftedApplications[0].iFKTst_ID);
            //var LandData = ZapurseCommonlist.GetLandData(EditdraftedApplications[0].ApplGuid);
            //ViewBag.LandDetails = LandData;
            ViewBag.trusteeMember = trusteeMember;
            SessionModel.ApplicantGuid = applGUID;
            return View();
        }

        //public ActionResult ApplicationPreview(string applicationNo, string trustName, int trustID, string clgName, string dptname, string cours, int deptID, int courseID,int clgID)
        public ActionResult ApplicationPreview(string applGUID)
        {
            var EditdraftedApplications = ZapurseCommonlist.GetDraftApplication(applGUID);
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
        
        public ActionResult ApplyNOCApplication()
        {
            var departmentList =ZapurseCommonlist.GetDepartmentlist();
            ViewBag.departments = departmentList;
            return View();
        }

        //public ActionResult CollageAttachment()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult TrusteeGeneralInfo(string RegNo)
        {
            SessionModel.TrustRegNo = RegNo;
            TrustRoot _trustapi = new TrustRoot();
            //modal.RegistrationNo = "COOP/2019/ALWAR/100658";
            #region Trust API
            var client = new RestClient("https://rajsahakarapp.rajasthan.gov.in/api/EntireSocietyDetail/GetSocietyDetailsByRegistrationNO?Reg_no=" + RegNo);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _trustapi = _JsonSerializer.Deserialize<TrustRoot>(response.Content);
                if (_trustapi.Status == "200" && _trustapi.Message == "Success")
                {
                    ErrorBO _ress = Verificationdata(_trustapi);
                    if(_ress.ResponseCode == "1")
                    {
                        #region List Trustee
                        client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetTrustInfo?TrustId=" + _trustapi.Data.RegistrationNo);
                        request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");
                        //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                        _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                        request.AddParameter("application/json", "", ParameterType.RequestBody);
                        response = client.Execute(request);
                        if (response.StatusCode.ToString() == "OK")
                        {
                            TrusteeBO.TrusteeInfo _result = _JsonSerializer.Deserialize<TrusteeBO.TrusteeInfo>(response.Content);
                            if (_result != null)
                            {
                                ViewBag.TrustDetails = _result;
                                ViewData["RegDate"] = _result.RegistrationDate;
                                SessionModel.TrustId = _result.TrusteeInfoId;
                                //return RedirectToAction("Index");
                            }
                        }
                        #endregion
                    }
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { Success = false, Message = "Enter Correct Registration Number", res = _trustapi },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            //Console.WriteLine(response.Content);
            #endregion

            // TrusteeBO.TrusteeInfo modal = new TrusteeBO.TrusteeInfo();
            // modal.RegistrationNo = RegNo;
            // #region Save and Get details
            // //#region VerifyDetails
            //var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrustVerificationAPI");
            //var  request = new RestRequest(Method.POST);
            // request.AddHeader("cache-control", "no-cache");
            // //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            // _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            // request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            // if (response.StatusCode.ToString() == "OK")
            // {
            //     ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
            //     if (objResponseData.ResponseCode == "1")
            //     {
            //         //TempData["SwalStatusMsg"] = "success";
            //         //TempData["SwalMessage"] = "Data saved sussessfully!";
            //         //TempData["SwalTitleMsg"] = "Success...!";
            //         SessionModel.TrustId = objResponseData.Id;
            //         return new JsonResult
            //         {
            //             Data = new { Success = true, Message = objResponseData.Messsage },
            //             ContentEncoding = System.Text.Encoding.UTF8,
            //             JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //         };
            //     }
            //     else
            //     {
            //         //TempData["SwalStatusMsg"] = "error";
            //         //TempData["SwalMessage"] = "Something wrong";
            //         //TempData["SwalTitleMsg"] = "error!";
            //         return new JsonResult
            //         {
            //             Data = new { Success = false, Message = objResponseData.Messsage },
            //             ContentEncoding = System.Text.Encoding.UTF8,
            //             JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //         };
            //     }
            // }
            // //#endregion
            // #endregion


            // #region List Trustee
            // var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetTrustInfo?TrustId=" + SessionModel.TrustId);
            // var request = new RestRequest(Method.GET);
            // request.AddHeader("cache-control", "no-cache");
            // //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            // _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            // request.AddParameter("application/json", "", ParameterType.RequestBody);
            // IRestResponse response = client.Execute(request);
            // if (response.StatusCode.ToString() == "OK")
            // {
            //     TrusteeBO.TrusteeInfo _result = _JsonSerializer.Deserialize<TrusteeBO.TrusteeInfo>(response.Content);
            //     if (_result != null)
            //     {
            //         ViewBag.TrustDetails = _result;
            //         //return RedirectToAction("Index");
            //     }
            // }
            // #endregion

            // List<CustomMaster> TrusteeType = new List<CustomMaster>();
            // TrusteeType = Common.GetCustomMastersList(31);
            // ViewBag.TrusteeType = TrusteeType;
            return View();
        }
        public List<CustomMaster> GetTrustDropDownList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();


            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetTrustDropDownList");
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
            }
            return objUsermaster;
        }

        [HttpPost]
        public ActionResult TrusteeGeneralInfo(TrusteeBO.TrusteeInfo obj, HttpPostedFileBase Ceritifiedbyfile, HttpPostedFileBase registrationnofile, HttpPostedFileBase trustfile,HttpPostedFileBase TMProfffile)
        {

            obj.TrusteeInfoId = SessionModel.TrustId;
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Certified Document
            if (Ceritifiedbyfile != null)
            {
                extension = Path.GetExtension(Ceritifiedbyfile.FileName);
                ContentType = Ceritifiedbyfile.ContentType;
                using (Stream inputStream = Ceritifiedbyfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Certified = Convert.ToBase64String(Documentbyte);
                    obj.CeritifiedExtension = extension;
                    obj.CeritifiedbyContenttype = ContentType;
                }
            }
            #endregion
            #region Registration Document
            if (registrationnofile != null)
            {

                extension = Path.GetExtension(registrationnofile.FileName);
                ContentType = registrationnofile.ContentType;
                using (Stream inputStream = registrationnofile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Registration = Convert.ToBase64String(Documentbyte);
                    obj.RegistrationExtension = extension;
                    obj.RegistrationContenttype = ContentType;
                }
            }
            #endregion
            #region Registration Document
            if (trustfile != null)
            {

                extension = Path.GetExtension(trustfile.FileName);
                ContentType = trustfile.ContentType;
                using (Stream inputStream = trustfile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.TrusteeLogo = Convert.ToBase64String(Documentbyte);
                    obj.TrusteeExtension = extension;
                    obj.TrusteeContentType = ContentType;
                }
            }
            #endregion
            #region Registration Document
            if (TMProfffile != null)
            {

                extension = Path.GetExtension(TMProfffile.FileName);
                ContentType = TMProfffile.ContentType;
                using (Stream inputStream = TMProfffile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.TRMP = Convert.ToBase64String(Documentbyte);
                    obj.TRMPExtension = extension;
                    obj.TRMPContenttype = ContentType;
                }
            }
            #endregion
            #region Add Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddTrusteeInfo");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = "Data saved sussessfully!";
                    TempData["SwalTitleMsg"] = "Success...!";
                    return RedirectToAction("TrusteeGeneralInfo",new { RegNo = SessionModel.TrustRegNo});
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    //return RedirectToAction("Index");
                }
            }
            #endregion

            List<CustomMaster> TrusteeType = new List<CustomMaster>();
            TrusteeType = Common.GetCustomMastersList(31);
            ViewBag.TrusteeType = TrusteeType;
            return View();
        }

        [HttpGet]
        public ActionResult TrustList()
        {
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrustInfoList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.TrusteeInfo> _result = _JsonSerializer.Deserialize<List<TrusteeBO.TrusteeInfo>>(response.Content);
                if (_result != null)
                {
                    ViewBag.TrusteeList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }

        [HttpGet]
        public ActionResult CollageListForApply()
        {
            #region List Collage Apply List
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/CollageListApply");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.CollageList> _result = _JsonSerializer.Deserialize<List<TrusteeBO.CollageList>>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollageApplyList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }

        [HttpGet]
        public ActionResult CollageFacilitys()
        {
            TrusteeBO.CollageFacility modal = new TrusteeBO.CollageFacility();
            modal.Guid = SessionModel.ApplicantGuid;
            //ViewBag.Guid = eGuid;
            List<TrusteeBO.Trustee> trustees = new List<TrusteeBO.Trustee>();
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetCollageFacilityList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                TrusteeBO.CollageFacility _result = _JsonSerializer.Deserialize<TrusteeBO.CollageFacility>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollageFacilityMster = _result;
                    //return RedirectToAction("Index");
                }
            }
            ViewBag.TrusteeList = trustees;
            #endregion

            //Collage Faciliy Master from Enum
            //List<CustomMaster> CollageFacilityMster = new List<CustomMaster>();
            //CollageFacilityMster = Common.GetCustomMastersList(35);
            //ViewBag.CollageFacilityMster = CollageFacilityMster;
            //ViewData["TrustId"] = "0";
            //ViewData["CollageId"] = "0";

            //ViewBag.CollageFacilityMster = new List<CustomMaster>();

            //Trust List 
            //List<CustomMaster> TrustList = new List<CustomMaster>();
            //TrustList = GetTrustDropDownList(28);
            //ViewBag.TrustList = TrustList;


            //List<CustomMaster> RoleType = new List<CustomMaster>();
            //RoleType = Common.GetCustomMastersList(29);
            //ViewBag.RoleType = RoleType;

            //#region List Trustee
            //var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/TrusteeList");
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("cache-control", "no-cache");
            ////request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //request.AddParameter("application/json", "", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //if (response.StatusCode.ToString() == "OK")
            //{
            //    List<TrusteeBO.Trustee> _result = _JsonSerializer.Deserialize<List<TrusteeBO.Trustee>>(response.Content);
            //    if (_result != null)
            //    {
            //        ViewBag.TrusteeList = _result;
            //        //return RedirectToAction("Index");
            //    }
            //}
            //#endregion
            return View();
        }

        [HttpPost]
        public ActionResult CollageFacilitys(TrusteeBO.CollageFacility modal)
        {
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetCollageFacilityList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                TrusteeBO.CollageFacility _result = _JsonSerializer.Deserialize<TrusteeBO.CollageFacility>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollageFacilityMster = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            //Collage Faciliy Master from Enum
            //List<CustomMaster> CollageFacilityMster = new List<CustomMaster>();
            //CollageFacilityMster = Common.GetCustomMastersList(35);
            //ViewBag.CollageFacilityMster = CollageFacilityMster;

            ViewData["TrustId"] = 0; //modal.TrustId;
            ViewData["CollageId"] = 0; //modal.CollageId;
            //Trust List 
            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;


            List<CustomMaster> RoleType = new List<CustomMaster>();
            RoleType = Common.GetCustomMastersList(29);
            ViewBag.RoleType = RoleType;
            List<TrusteeBO.Trustee> trustees = new List<TrusteeBO.Trustee>();   
            #region List Trustee
            client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrusteeList");
            request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                trustees = _JsonSerializer.Deserialize<List<TrusteeBO.Trustee>>(response.Content);
                //if (_result != null)
                //{
                //    ViewBag.TrusteeList = _result;
                //    //return RedirectToAction("Index");
                //}
            }
            ViewBag.TrusteeList = trustees; 
            #endregion
            return View();
        }
        [HttpPost]
        public JsonResult CollageFacilitysAdd(TrusteeBO.CollageFacility modal)
        {
            modal.Guid = SessionModel.ApplicantGuid;
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddCollageFacility");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    //TempData["SwalStatusMsg"] = "success";
                    //TempData["SwalMessage"] = "Data saved sussessfully!";
                    //TempData["SwalTitleMsg"] = "Success...!";
                    return new JsonResult
                    {
                        Data = new { failure = true, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { failure = false, msg = "Failed" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            #endregion
            return new JsonResult
            {
                Data = new { failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetCollegeDropDownList(int TrustInfoId)
        {
            ResponseData objResponse = new ResponseData();
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetCollageDropDownList?TrustInfoId=" + TrustInfoId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    objUsermaster = JsonConvert.DeserializeObject<List<CustomMaster>>(objResponse.Data.ToString());
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objUsermaster, Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetCollageFaciltyList(int TrustInfoId)
        {
            ResponseData objResponse = new ResponseData();
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetCollageDropDownList?TrustInfoId=" + TrustInfoId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    objUsermaster = JsonConvert.DeserializeObject<List<CustomMaster>>(objResponse.Data.ToString());
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objUsermaster, Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetLandDetails(string AppGUID)
        {
            var LandData = ZapurseCommonlist.GetLandData(AppGUID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = LandData, Failure = false, msg = "Land Details" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult CollageAttachment(string guid)
        {
            var draftedApplication = ZapurseCommonlist.GetDraftApplication(guid);

            ViewData["TrustId"] = draftedApplication[0].iFKTst_ID;
            ViewData["CollageId"] = draftedApplication[0].clgID;
            ViewData["CourseId"] = draftedApplication[0].iFK_CORS_ID;
            return View();
        }

        [HttpPost]
        public ActionResult CollageAttachment(TrusteeBO.CollageAttachment obj)
        {
            List<TrusteeBO.DocumentsDetails> _DOCList = new List<TrusteeBO.DocumentsDetails>();

            TrusteeBO.DocumentsDetails _DocDetails = getDocdetails(obj.affidavit, "affidavit",0,obj.iFk_TrstId,obj.iFk_ClgId,obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.SalaryPayment, "SalaryPayment", 0, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.Bankstatement, "Bankstatement", 0, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.Annexure, "Annexure", 0, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.EsiDoc, "EsiDoc", 0, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsCnnctUnvrctyDrctnfile, "bIsCnnctUnvrctyDrctnfile", obj.bIsCnnctUnvrctyDrctn, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsTimeFrmfile, "bIsTimeFrm", obj.bIsTimeFrm, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsLadDwnfile, "bIsLadDwn", obj.bIsLadDwn, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsSffcentLandfile, "bIsSffcentLand", obj.bIsSffcentLand, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsAffidvtAsprGuidfile, "bIsAffidvtAsprGuid", obj.bIsAffidvtAsprGuid, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);


            _DocDetails = new TrusteeBO.DocumentsDetails();
            _DocDetails = getDocdetails(obj.bIsOtherDocfile, "bIsOtherDoc", obj.bIsOtherDoc, obj.iFk_TrstId, obj.iFk_ClgId, obj.iFk_CourseId);
            _DOCList.Add(_DocDetails);

            TrusteeBO.CollageattachmentAPI _rendmodal = new TrusteeBO.CollageattachmentAPI();
            _rendmodal.bIsAffidvtAsprGuid = obj.bIsAffidvtAsprGuid;
            _rendmodal.bIsCnnctUnvrctyDrctn = obj.bIsCnnctUnvrctyDrctn;
            _rendmodal.bIsLadDwn = obj.bIsLadDwn;
            _rendmodal.bIsOtherDoc = obj.bIsOtherDoc;
            _rendmodal.bIsSffcentLand = obj.bIsSffcentLand;
            _rendmodal.bIsTimeFrm = obj.bIsTimeFrm;
            _rendmodal.iFk_TrstId = obj.iFk_TrstId;
            _rendmodal.iFk_ClgId = obj.iFk_ClgId;
            _rendmodal.iFk_CourseId = obj.iFk_CourseId;
            _rendmodal.sSSOID = obj.sSSOID;

            #region Adddata
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddCollageAttachementMain");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            //var json = _JsonSerializer.Serialize(obj);
            request.AddParameter("application/json", _JsonSerializer.Serialize(_rendmodal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = _JsonSerializer.Deserialize<ResponseData>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    if(_DOCList != null)
                    {
                        foreach(TrusteeBO.DocumentsDetails item in _DOCList)
                        {
                            item.Id = objResponseData.ID;
                            item.EnumNo = 40;
                            client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/AddCollageAttachementFiles");
                            request = new RestRequest(Method.POST);
                            request.AddHeader("cache-control", "no-cache");
                            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                            _JsonSerializer.MaxJsonLength = Int32.MaxValue;

                            request.AddParameter("application/json", _JsonSerializer.Serialize(item), ParameterType.RequestBody);
                            response = client.Execute(request);
                            if (response.StatusCode.ToString() == "OK")
                            {
                            }
                        }
                    } 
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = "Data saved sussessfully!";
                    TempData["SwalTitleMsg"] = "Success...!";
                    return RedirectToAction("CollageAttachment",new { TrustId=obj.iFk_TrstId, CollageId=obj.iFk_ClgId,CourseId=obj.iFk_CourseId });
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    //return RedirectToAction("Index");
                }
            }
            #endregion

            return View();
        }


        public JsonResult SaveApplicationDetails(TrusteeBO.SaveApplicationModal applicationModal)
        {
            var json = JsonConvert.SerializeObject(applicationModal);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/SaveApplication");
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
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetNOCApplicationList(int departID)
        {
            var nocList = ZapurseCommonlist.GETNOCApplicationList(departID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GETNOCApplicationClgList(int departID)
        {
            var nocList = ZapurseCommonlist.GETNOCApplicationClgList(departID);
            return new JsonResult
            {
                Data = new { StatusCode = 1, Data = nocList, Failure = false, Message = "NOC List" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public TrusteeBO.DocumentsDetails getDocdetails(HttpPostedFileBase file,string Filetype,int Isyes,int TrustId,int CollageId,int CourseId)
        {
            TrusteeBO.DocumentsDetails _result = new TrusteeBO.DocumentsDetails();
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Certified Document
            if (file != null)
            {
                extension = Path.GetExtension(file.FileName);
                ContentType = file.ContentType;
                using (Stream inputStream = file.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    _result.file = Convert.ToBase64String(Documentbyte);
                    _result.Extension = extension;
                    _result.Contenttype = ContentType;
                    _result.Filetype = Filetype;
                    _result.Isyes = Isyes;
                    _result.TrustId = TrustId;
                    _result.CollageId = CollageId;
                    _result.CourseId = CourseId;
                }
            }
            #endregion
            return _result;
        }

        public ErrorBO Verificationdata(TrustRoot modal)
        {
            ErrorBO _res = new ErrorBO();
            #region VerifyDetails
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrustVerificationAPI");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _res = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (_res.ResponseCode == "1")
                {
                    //TempData["SwalStatusMsg"] = "success";
                    //TempData["SwalMessage"] = "Data saved sussessfully!";
                    //TempData["SwalTitleMsg"] = "Success...!";
                    SessionModel.TrustId = _res.Id;
                    //return new JsonResult
                    //{
                    //    Data = new { Success = true, Message = objResponseData.Messsage },
                    //    ContentEncoding = System.Text.Encoding.UTF8,
                    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    //};
                }
                else
                {
                    //TempData["SwalStatusMsg"] = "error";
                    //TempData["SwalMessage"] = "Something wrong";
                    //TempData["SwalTitleMsg"] = "error!";
                    //return new JsonResult
                    //{
                    //    Data = new { Success = false, Message = objResponseData.Messsage },
                    //    ContentEncoding = System.Text.Encoding.UTF8,
                    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    //};
                }
            }
            #endregion
            return _res;
        }

        public ActionResult testpage()
        {
            return View();
        }
    }
}