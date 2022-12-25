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
    public class ProofOfDocumentController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: ProofOfDocument

        public ActionResult Index ()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SaveDetails(ProofOfDocument obj, HttpPostedFileBase certifiedcopy, HttpPostedFileBase AimsandObjective, HttpPostedFileBase ProjectmapandRoad,
            HttpPostedFileBase ProofofOwnership, HttpPostedFileBase Authorizationletters,
            HttpPostedFileBase ProjectReport, HttpPostedFileBase BalanceSheet, HttpPostedFileBase ESI)
        {
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region certifiedcopy
            if (certifiedcopy != null)
            {
                extension = Path.GetExtension(certifiedcopy.FileName);
            
                using (Stream inputStream = certifiedcopy.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.certifiedcopy = Convert.ToBase64String(Documentbyte);
                    obj.certifiedcopyExtension = extension;
                    obj.certifiedcopyContenttype = ContentType;
                }
            }
            #endregion
            #region AimsandObjective
            if (AimsandObjective != null)
            {
                extension = Path.GetExtension(AimsandObjective.FileName);

                using (Stream inputStream = AimsandObjective.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.AimsandObjective = Convert.ToBase64String(Documentbyte);
                    obj.AimsandObjectiveExtension = extension;
                    obj.AimsandObjectiveContenttype = ContentType;
                }
            }
            #endregion
            #region ProjectmapandRoad
            if (ProjectmapandRoad != null)
            {
                extension = Path.GetExtension(ProjectmapandRoad.FileName);

                using (Stream inputStream = ProjectmapandRoad.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.ProjectmapandRoad = Convert.ToBase64String(Documentbyte);
                    obj.ProjectmapandRoadExtension = extension;
                    obj.ProjectmapandRoadContenttype = ContentType;
                }
            }
            #endregion
            #region ProofofOwnership
            if (ProofofOwnership != null)
            {
                extension = Path.GetExtension(ProofofOwnership.FileName);

                using (Stream inputStream = ProofofOwnership.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.ProofofOwnership = Convert.ToBase64String(Documentbyte);
                    obj.ProofofOwnershipExtension = extension;
                    obj.ProofofOwnershipContenttype = ContentType;
                }
            }
            #endregion
            #region Authorizationletters
            if (Authorizationletters != null)
            {
                extension = Path.GetExtension(Authorizationletters.FileName);

                using (Stream inputStream = Authorizationletters.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.Authorizationletters = Convert.ToBase64String(Documentbyte);
                    obj.AuthorizationlettersExtension = extension;
                    obj.AuthorizationlettersContenttype = ContentType;
                }
            }
            #endregion
            #region ProjectReport
            if (ProjectReport != null)
            {
                extension = Path.GetExtension(ProjectReport.FileName);

                using (Stream inputStream = ProjectReport.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.ProjectReport = Convert.ToBase64String(Documentbyte);
                    obj.ProjectReportExtension = extension;
                    obj.ProjectReportContenttype = ContentType;
                }
            }
            #endregion
            #region BalanceSheet
            if (BalanceSheet != null)
            {
                extension = Path.GetExtension(BalanceSheet.FileName);

                using (Stream inputStream = BalanceSheet.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.BalanceSheet = Convert.ToBase64String(Documentbyte);
                    obj.BalanceSheetExtension = extension;
                    obj.BalanceSheetContenttype = ContentType;
                }
            }
            #endregion
            #region ESI
            if (ESI != null)
            {
                extension = Path.GetExtension(ESI.FileName);

                using (Stream inputStream = ESI.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    obj.ESI = Convert.ToBase64String(Documentbyte);
                    obj.ESIExtension = extension;
                    obj.ESIContenttype = ContentType;
                }
            }
            #endregion

            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(obj);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "ProofOfDoc/DocumentDetailSave");
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
                    return RedirectToAction("Index", "ProofOfDocument");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("Index", "ProofOfDocument");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return View();
        }


    }
}