using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Common;

namespace NewZapures_V2.Controllers
{
    public class BankMasterController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;

        // GET: BankMaster
        public ActionResult CreateDetails()
        {
            return View();
        }

        public ActionResult ViewDetails()
        {
            var bankDetailsList = ZapurseCommonlist.GetBankDetails();

            return View(bankDetailsList);
        }

        [HttpPost]
        public ActionResult SaveDetails(BankMaster trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BankConfiguration/BankDetails");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    TempData["isSaved"] =1;
                    TempData["msg"] = " Details Saved...";
                    return RedirectToAction("CreateDetails","BankMaster");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("CreateDetails", "BankMaster");
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

        [HttpPost]
        public JsonResult ViewUpdate(int rowID, int status)
        {
            ResponseData objResponse = new ResponseData();
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BankConfiguration/BankDetailsUpdate?rowID=" + rowID + "&status=" + status);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}
