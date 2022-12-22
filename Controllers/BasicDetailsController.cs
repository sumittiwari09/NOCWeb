using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NewZapures_V2.Models.Common;
using System.Web.Script.Serialization;
using System.IO;

namespace NewZapures_V2.Controllers
{
    public class BasicDetailsController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: BasicDetails
        public ActionResult CreateDetails()
        {
            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;

            var universityList = ZapurseCommonlist.GetUniversities();

            ViewBag.universityCollection = universityList;
            return View();
        }
        public ActionResult ShowDetails()
        {
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
        public ActionResult SaveDetails(BasicDetailsBO trg, HttpPostedFileBase collageLogo)
        {
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Collage Logo
            if (collageLogo != null)
            {
                extension = Path.GetExtension(collageLogo.FileName);
                ContentType = collageLogo.ContentType;
                using (Stream inputStream = collageLogo.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    trg.CollageLogo = Convert.ToBase64String(Documentbyte);
                    trg.CollageLogoExtension = extension;
                    trg.CollageLogoContenttype = ContentType;
                }
            }
            #endregion

            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/BasicDetailConfigure");
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
                    return RedirectToAction("CreateDetails", "BasicDetails");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("CreateDetails", "BasicDetails");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return RedirectToAction("CreateDetails");
        }

        [HttpGet]
        public ActionResult CollageList()
        {
            #region List Collage Apply List
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/CollageListApply");
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
                    ViewBag.collagelist = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();            
        }

    }
}