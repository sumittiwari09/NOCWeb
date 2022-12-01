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
           var universityList = ZapurseCommonlist.GetUniversities();

            ViewBag.universityCollection = universityList;  
            return View();
        }
        public ActionResult ShowDetails()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveDetails(BasicDetailsBO trg)
        {
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
                //        if (response.StatusCode.ToString() == "OK")
                //        {
                //            var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                //            TempData["IsUserDetailsExists"] = 1;
                //            TempData["msg"] = " Details Not Saved...";
                //            return RedirectToAction("BankMastert", "CreateDetails");
                //        } 
                //        else
                //         {
                //             TempData["IsUserDetailsExists"] = 1;
                //             TempData["msg"] = "Details Not Saved Due To Some Internal Issues...!";
                //             return RedirectToAction("AddPartner", "Role");
                //         }
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("CreateDetails");
        }

    }
}