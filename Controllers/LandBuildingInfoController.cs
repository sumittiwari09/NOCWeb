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

namespace NewZapures_V2.Controllers
{
    public class LandBuildingInfoController : Controller
    {
        // GET: LandBuildingInfo
        public ActionResult Index(string guid)
        {
            var draftApplications = ZapurseCommonlist.GetDraftApplication(guid);
            var courses = ZapurseCommonlist.GetCourseForDept(draftApplications[0].iFKDEPT_ID);
            ViewBag.CourseList = courses;
            return View();
        }

        [HttpPost]
        public ActionResult SaveDetails(LandInfoBO trg, HttpPostedFileBase LandAreaProof, HttpPostedFileBase LandConvertProof, HttpPostedFileBase OwnBuildingProof)
        {
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Land Area Proof
            if (LandAreaProof != null)
            {
                extension = Path.GetExtension(LandAreaProof.FileName);
                ContentType = LandAreaProof.ContentType;
                using (Stream inputStream = LandAreaProof.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    trg.LandAreaProof = Convert.ToBase64String(Documentbyte);
                    trg.LandAreaProofExtension = extension;
                    trg.LandAreaProofDocumentContent = ContentType;
                }
            }
            #endregion

            #region Upload Document 
            if (LandConvertProof != null)
            {
                extension = Path.GetExtension(LandConvertProof.FileName);
                ContentType = LandConvertProof.ContentType;
                using (Stream inputStream = LandConvertProof.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    trg.LandConvertProof = Convert.ToBase64String(Documentbyte);
                    trg.LandConvertProofExtension = extension;
                    trg.LandConvertProofDocumentContent = ContentType;
                }
            }
            #endregion

            #region Certificate Doc
            if (OwnBuildingProof != null)
            {
                extension = Path.GetExtension(OwnBuildingProof.FileName);
                ContentType = OwnBuildingProof.ContentType;
                using (Stream inputStream = OwnBuildingProof.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    trg.OwnBuildingProof = Convert.ToBase64String(Documentbyte);
                    trg.OwnBuildingProofExtension = extension;
                    trg.OwnBuildingProofDocumentContent = ContentType;
                }
            }
            #endregion
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "LandDetails/AddLandInfo");
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
                    return RedirectToAction("Index", "LandBuildingInfo");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("Index", "LandBuildingInfo");
                }               
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return RedirectToAction("CreateDetails");
        }
    }
}