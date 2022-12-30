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
        public ActionResult Create(UniversityMaster obj, HttpPostedFileBase Document)
        {
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Document
            if (Document != null)
            {
                extension = Path.GetExtension(Document.FileName);

                using (Stream inputStream = Document.InputStream)
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
        public ActionResult Edit()
        {
            return View();
        }
        public ActionResult Delete()
        {
            return View();
        }
    }
}