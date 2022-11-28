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
    public class TargetController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;

        // GET: Target
        public ActionResult Create()
        {
            var serviceDetailsList = GetServicesAllDetails();
            ViewBag.serviceDetailsList = serviceDetailsList;
            return View();
        }

        public ActionResult ViewDetails()
        {
            // var targetList = ZapurseCommonlist.GetTargetDetails();
            return View();
            //return View(targetList);
        }


        public static List<Dropdown> GetServicesAllDetails()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCategoryAllInformation?Type=Service");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<GetservicesetailsAndroidNew> serviceDetails = new List<GetservicesetailsAndroidNew>();
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    serviceDetails = JsonConvert.DeserializeObject<List<GetservicesetailsAndroidNew>>(objResponse.Data.ToString());
            }

            var serviceList = serviceDetails.Select(s => new Dropdown
            {
                Id = s.CategoryId,
                Text = s.CategoryName
            }).ToList();

            return serviceList;
        }

        [HttpPost]
        public ActionResult SaveDetails(TargetMaster trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "TargetConfiguration/TargetDetails");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("Create");
        }

        [HttpPost]
        public JsonResult ViewUpdate(int rowID, int status)
        {
            ResponseData objResponse = new ResponseData();
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "TargetConfiguration/TargetDetailsUpdate?rowID=" + rowID + "&status=" + status);
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