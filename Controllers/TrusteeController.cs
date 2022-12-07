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

namespace NewZapures_V2.Controllers
{
    public class TrusteeController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        // GET: Trustee
        public ActionResult Index()
        {
            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(24);
            ViewBag.TrustList = TrustList;

            List<CustomMaster> RoleType = new List<CustomMaster>();
            RoleType = Common.GetCustomMastersList(25);
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

        [HttpGet]
        public ActionResult TrusteeGeneralInfo()
        {
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
            TrusteeType = Common.GetCustomMastersList(24);
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
        public ActionResult TrusteeGeneralInfo(TrusteeBO.TrusteeInfo obj, HttpPostedFileBase Ceritifiedbyfile,HttpPostedFileBase registrationnofile,HttpPostedFileBase trustfile)
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
    }
}