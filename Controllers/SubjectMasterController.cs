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
using static NewZapures_V2.Models.Common;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    public class SubjectMasterController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: SubjectMaster
        public ActionResult CreateData()
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

            List<CustomEnum> collegeListData = new List<CustomEnum>();
            collegeListData = GetCollegeList();
            ViewBag.collegeListData = collegeListData;

            GetSubjectDataList();

            return View();
        }
        public JsonResult GetSubjectDataList()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "AddCourseData/GetSubjectList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<AddCourseBO> _result = _JsonSerializer.Deserialize<List<AddCourseBO>>(response.Content);
                if (_result != null)
                {
                    ViewBag.SubjectDataList = _result;

                }
            }
            return Json(ViewBag);
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
                ViewBag.CollegeList = objUsermaster;
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objUsermaster, Failure = false, msg = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }



        public List<CustomEnum> GetCollegeList()
        {
            List<CustomEnum> objUsermaster = new List<CustomEnum>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "AddCourseData/GetCollegeList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<CustomEnum> _result = _JsonSerializer.Deserialize<List<CustomEnum>>(response.Content);
                if (_result != null)
                {
                    objUsermaster = _result;

                }
            }
            return objUsermaster;
        }

        [HttpPost]
        public ActionResult SaveDetails(AddCourseBO trg)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "AddCourseData/SubjectDetail");
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
                    // return RedirectToAction("CreateData", "SubjectMaster");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    // return RedirectToAction("CreateData", "AddCourse");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return RedirectToAction("CreateData");
        }
        //For Course List
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


        public JsonResult EditSubject(string type, int iPK_SubId, string sub)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "AddCourseData/GetSubjectListById?iPK_SubId=" + iPK_SubId + "&type=" + type + "&SubjectName=" + sub);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var Subjectlisting = new List<AddCourseBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                    Subjectlisting = JsonConvert.DeserializeObject<List<AddCourseBO>>(d.Data.ToString());

                return new JsonResult
                {
                    Data = new { StatusCode = d.statusCode, Data = Subjectlisting, Failure = false, Message = d.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


    }
}