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
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/TrusteeList");
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
        public ActionResult Index(TrusteeBO.Trustee obj, HttpPostedFileBase aadhaarfile, HttpPostedFileBase panfile, HttpPostedFileBase profilefile)
        {
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
            #region Add Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/AddTrustee");
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
            client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/TrusteeList");
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

        public ActionResult DownloadDocuments(int id)
        {
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/DocumentDetail?Id=" + id);
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

        public ActionResult EditApplication(string applicationNo, string trustName, int trustID, string clgName, string dptname, string cours, int deptID, int courseID,int clgID)
        {
            ViewBag.appNo = applicationNo;
            ViewBag.trustName = trustName.Trim();
            ViewBag.trustID = trustID;
            ViewBag.clgname = clgName;
            ViewBag.dept = dptname;
            ViewBag.cours = cours;
            ViewBag.dptID = deptID;
            ViewBag.courseID = courseID;
            ViewBag.clgID = clgID;

            var trusteeMember = ZapurseCommonlist.GetTrusteeMember(trustID);
            ViewBag.trusteeMember = trusteeMember;

            return View();
        }

        public ActionResult ApplicationPreview(string applicationNo, string trustName, int trustID, string clgName, string dptname, string cours, int deptID,int courseID, int clgID)
        {
            ViewBag.appNo = applicationNo;
            ViewBag.trustName = trustName.Trim();
            ViewBag.trustID = trustID;
            ViewBag.clgname = clgName;
            ViewBag.dept = dptname;
            ViewBag.cours = cours;
            ViewBag.dptID = deptID;
            ViewBag.courseID = courseID;
            ViewBag.clgID = clgID;
            var trusteeMember = ZapurseCommonlist.GetTrusteeMember(trustID);
            ViewBag.trusteeMember = trusteeMember;

            return View();
        }

        //public ActionResult CollageAttachment()
        //{
        //    return View();
        //}
        [HttpGet]
        public ActionResult TrusteeGeneralInfo()
        {
            //var client = new RestClient("https://api.sewadwaar.rajasthan.gov.in/app/live/master/getmasterdata/service?client_id=88d28d9b-408d-4b41-ab9e-5f704825ce4c");
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            //request.AddHeader("UserName","Doit");
            //request.AddHeader("Password","Doit@123");
            //request.AddHeader("ProjectCode","WSKANBZATL");
            //request.AddHeader("MasterDataID","1");
            //request.AddHeader("IsNew","1");
            //request.AddHeader("IsActive","1");
            //request.AddHeader("ModificationDate", "01-01-2017");
            ////request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //request.AddParameter("application/json", "", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);


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

            List<CustomMaster> TrusteeType = new List<CustomMaster>();
            TrusteeType = Common.GetCustomMastersList(31);
            ViewBag.TrusteeType = TrusteeType;
            return View();
        }
        public List<CustomMaster> GetTrustDropDownList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();


            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/GetTrustDropDownList");
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
        public ActionResult TrusteeGeneralInfo(TrusteeBO.TrusteeInfo obj, HttpPostedFileBase Ceritifiedbyfile, HttpPostedFileBase registrationnofile, HttpPostedFileBase trustfile)
        {

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
            #region Add Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/AddTrusteeInfo");
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
                    return RedirectToAction("TrusteeGeneralInfo");
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
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/TrustInfoList");
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
        public ActionResult CollageFacilitys(string appNo)
        {
            //Collage Faciliy Master from Enum
            //List<CustomMaster> CollageFacilityMster = new List<CustomMaster>();
            //CollageFacilityMster = Common.GetCustomMastersList(35);
            //ViewBag.CollageFacilityMster = CollageFacilityMster;
            ViewData["TrustId"] = "0";
            ViewData["CollageId"] = "0";

            ViewBag.CollageFacilityMster = new List<CustomMaster>();

            //Trust List 
            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;


            List<CustomMaster> RoleType = new List<CustomMaster>();
            RoleType = Common.GetCustomMastersList(29);
            ViewBag.RoleType = RoleType;

            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/TrusteeList");
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
        public ActionResult CollageFacilitys(TrusteeBO.CollageFacility modal)
        {

            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/GetCollageFacilityList");
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

            ViewData["TrustId"] = modal.TrustId;
            ViewData["CollageId"] = modal.CollageId;
            //Trust List 
            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;


            List<CustomMaster> RoleType = new List<CustomMaster>();
            RoleType = Common.GetCustomMastersList(29);
            ViewBag.RoleType = RoleType;

            #region List Trustee
            client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/TrusteeList");
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
            return View();
        }
        [HttpPost]
        public JsonResult CollageFacilitysAdd(TrusteeBO.CollageFacility modal)
        {
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/AddCollageFacility");
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
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = "Data saved sussessfully!";
                    TempData["SwalTitleMsg"] = "Success...!";
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
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "BasicDataDetails/GetCollageDropDownList?TrustInfoId=" + TrustInfoId);
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
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "BasicDataDetails/GetCollageDropDownList?TrustInfoId=" + TrustInfoId);
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

        public ActionResult CollageAttachment(string TrustId,string CollageId,string CourseId, string appNo)
        {
            ViewData["TrustId"] = TrustId;
            ViewData["CollageId"] = CollageId;
            ViewData["CourseId"] = CourseId;
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
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/AddCollageAttachementMain");
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
                            client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/AddCollageAttachementFiles");
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
    }
}