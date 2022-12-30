using NewZapures_V2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NewZapures_V2.Models.Common;
using System.Web.Script.Serialization;
using System.IO;
using System.Net.Mime;
using RestSharp;
using System.Configuration;
using Newtonsoft.Json;

namespace NewZapures_V2.Controllers
{
    public class UniverSityMasterController : Controller
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
                    obj.Documentextension = extension;
                    obj.DocumentContentType = ContentType;
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
                    return RedirectToAction("Create", "UniverSityMaster");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("Create", "UniverSityMaster");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return View();
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
