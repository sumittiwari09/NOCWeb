using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using static NewZapures_V2.Models.Common;
using System.Web.Mvc;
using System.Web;
using System.Web.Script.Serialization;

namespace NewZapures_V2.Controllers
{
    public class AddCourseController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: AddCourse
        public ActionResult CreateDetails()
        {
            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;

            List<CustomMaster> DegreeList = new List<CustomMaster>();
            DegreeList = Common.GetCustomMastersList(33);
            ViewBag.DegreeList = DegreeList;

            List<CustomMaster> CourseList = new List<CustomMaster>();
            CourseList = Common.GetCustomMastersList(30);
            ViewBag.CourseList = CourseList;

            ViewBag.AddCourseList = GetDetailsList(); 
            return View();
        }
        public List<CustomMaster> GetCourseDropDownList(int Enum)
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
        public List<CustomMaster> GetDegreeDropDownList(int Enum)
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
        public JsonResult GetCollegeDropDownList(int TrustInfoId)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "BasicDataDetails/GetCollageDropDownList?TrustInfoId=" + TrustInfoId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    objUsermaster = JsonConvert.DeserializeObject<List<CustomMaster>>(objResponse.Data.ToString());
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objUsermaster, Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        [HttpPost]
        public ActionResult SaveDetails(AddCourseBO trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/CourseDetailConfigure");
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
                    return RedirectToAction("CreateDetails", "AddCourse");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("CreateDetails", "AddCourse");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("CreateDetails");
        }

        public List<AddCourseBO> GetDetailsList()
        {
            List<AddCourseBO> objUsermaster = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/GetDetails");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AddCourseBO> objResponseData = new List<AddCourseBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponseData = JsonConvert.DeserializeObject<List<AddCourseBO>>(response.Content);
                //objResponseData = JsonConvert.DeserializeObject<List<AddCourseBO>>(objResponse.Data.ToString());

                objUsermaster = objResponseData;
            }

            return objUsermaster;
        }

       
    }
}