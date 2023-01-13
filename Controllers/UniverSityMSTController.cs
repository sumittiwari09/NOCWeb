using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Common;

namespace NewZapures_V2.Controllers
{
    public class UniverSityMSTController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;

        // GET: ProofOfDocument

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateUniversity()
        {
            var formData = Request.Form;
            UniversityMaster obj = new UniversityMaster()
            {
                Type_id = formData["Type_id"],
                UniName = formData["UniName"],
                Website = formData["Website"],
                ContactDetails = JsonConvert.DeserializeObject<List<ContactDetails>>(formData["ContactDetails"]),
                IsActive = formData["IsActive"],
            };
            var postedFiles = Request.Files;

            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Document
            if (postedFiles != null && postedFiles.Count > 0)
            {
                var postedFile = postedFiles[0];
                extension = Path.GetExtension(postedFile.FileName);

                using (Stream inputStream = postedFile.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Document = Convert.ToBase64String(Documentbyte);

                }
            }
            #endregion
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(obj);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "UniversityMaster/University");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    TempData["isSaved"] = 1;
                    TempData["msg"] = " Details Saved...";
                    return RedirectToAction("UnimasterList", "UniverSityMST");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("UnimasterList", "UniverSityMST");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return View();
        }
        [HttpGet]
        public ActionResult UnimasterList()
        {
            #region List
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "UniversityMaster/UniList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<UniversityMaster> result = _JsonSerializer.Deserialize<List<UniversityMaster>>(response.Content);
                if (result != null)
                {
                    ViewBag.UniList = result;
                }
            }
            #endregion
            return View();
        }
       
        public ActionResult HomePage()
        {
            List<CustomMaster> MenuList = new List<CustomMaster>();
            MenuList = GetMenuList();
            ViewBag.MenuList = MenuList;
            return View();
        }
        [HttpPost]
        public ActionResult CreateHomePage(HomePage obj, HttpPostedFileBase Banner1, HttpPostedFileBase Banner2, HttpPostedFileBase Banner3,
                                            HttpPostedFileBase Url1, HttpPostedFileBase Url2, HttpPostedFileBase Url3, string[] Submenu)

        {
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            int i = 0;

            obj.Submenu = "";
            foreach (string val in Submenu)
            {
                if (i != 0)
                {
                    obj.Submenu += ",";
                }
                obj.Submenu += val;
                i++;
            }

            #region Banner1
            if (Banner1 != null)
            {
                extension = Path.GetExtension(Banner1.FileName);
                ContentType = Banner1.ContentType;
                using (Stream inputStream = Banner1.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Banner1 = Convert.ToBase64String(Documentbyte);
                    //obj.PanExtension = extension;
                    //obj.PanContentType = ContentType;
                }
            }
            #endregion
            #region Banner2
            if (Banner2 != null)
            {
                extension = Path.GetExtension(Banner2.FileName);
                ContentType = Banner2.ContentType;
                using (Stream inputStream = Banner2.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Banner2 = Convert.ToBase64String(Documentbyte);
                    //obj.PanExtension = extension;
                    //obj.PanContentType = ContentType;
                }
            }
            #endregion
            #region Banner3
            if (Banner3 != null)
            {
                extension = Path.GetExtension(Banner3.FileName);
                ContentType = Banner3.ContentType;
                using (Stream inputStream = Banner3.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Banner3 = Convert.ToBase64String(Documentbyte);
                    //obj.PanExtension = extension;
                    //obj.PanContentType = ContentType;
                }
            }
            #endregion
            #region Url1
            if (Url1 != null)
            {
                extension = Path.GetExtension(Url1.FileName);
                ContentType = Url1.ContentType;
                using (Stream inputStream = Url1.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Url1 = Convert.ToBase64String(Documentbyte);
                    //obj.PanExtension = extension;
                    //obj.PanContentType = ContentType;
                }
            }
            #endregion
            #region Url2
            if (Url2 != null)
            {
                extension = Path.GetExtension(Url2.FileName);
                ContentType = Url2.ContentType;
                using (Stream inputStream = Url2.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Url2 = Convert.ToBase64String(Documentbyte);
                    //obj.PanExtension = extension;
                    //obj.PanContentType = ContentType;
                }
            }
            #endregion
            #region Url3
            if (Url3 != null)
            {
                extension = Path.GetExtension(Url3.FileName);
                ContentType = Url3.ContentType;
                using (Stream inputStream = Url3.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Url3 = Convert.ToBase64String(Documentbyte);
                    //obj.PanExtension = extension;
                    //obj.PanContentType = ContentType;
                }
            }
            #endregion
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(obj);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "UniversityMaster/Home");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    TempData["isSaved"] = 1;
                    TempData["msg"] = " Details Saved...";
                    return RedirectToAction("HomePage", "UniverSityMST");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("HomePage", "UniverSityMST");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            // return View();
        }

        public List<CustomMaster> GetMenuList()
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "UniversityMaster/GetMenuList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                    objUsermaster = JsonConvert.DeserializeObject<List<CustomMaster>>(d.Data.ToString());
            }
            return objUsermaster;
        }

        public JsonResult GetSubmenuList(int menuId)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "UniversityMaster/GetsubmenuList?menuId=" + menuId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            ResponseData objRes = new ResponseData(); 
            if (response.StatusCode.ToString() == "OK")
            {
                objRes = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objRes.Data != null)
                    objUsermaster = JsonConvert.DeserializeObject<List<CustomMaster>>(objRes.Data.ToString());
            }
            
            return new JsonResult
            {
                Data = new { StatusCode = objRes.statusCode, Data = objUsermaster, Failure = false, msg = objRes.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}