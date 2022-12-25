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

namespace NewZapures_V2.Controllers
{
    public class FeeDetailsController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        // GET: FeeDetails
        public ActionResult Index(string CourseId, string appNo)
        {
            // department list
            //List<CustomMaster> DepartMentList = new List<CustomMaster>();
            //DepartMentList = Common.GetCustomMastersList(37);
            //ViewBag.DepartMentList = DepartMentList;

            //// Course List
            //List<CustomMaster> CourseList = new List<CustomMaster>();
            //CourseList = Common.GetCustomMastersList(30);
            //ViewBag.CourseList = CourseList;

            ////Financial year
            //List<CustomMaster> FinancialYearList = new List<CustomMaster>();
            //FinancialYearList = Common.GetCustomMastersList(37);
            //ViewBag.FinancialYearList = FinancialYearList;
            CourseId = "1";
            TrusteeBO.CollageFeeMst obj = new TrusteeBO.CollageFeeMst();
            obj.CourseId = CourseId;
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/GetFeeDetailsList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                TrusteeBO.CollageFeeMst _result = _JsonSerializer.Deserialize<TrusteeBO.CollageFeeMst>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollageFeeList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }

        [HttpPost]
        public ActionResult Index(TrusteeBO.CollageFeeMst obj)
        {
            // department list
            List<CustomMaster> DepartMentList = new List<CustomMaster>();
            DepartMentList = Common.GetCustomMastersList(37);
            ViewBag.DepartMentList = DepartMentList;
            //var MyDepartmentList = (from m in DepartMentList select new { m.Id, m.Text });
            //ViewData["DepartMentList"] = new SelectList(MyDepartmentList.ToList(), "ListID", "ListName");



            // Course List
            List<CustomMaster> CourseList = new List<CustomMaster>();
            CourseList = Common.GetCustomMastersList(30);
            ViewBag.CourseList = CourseList;
            //var MyList = (from m in CourseList select new { m.Id, m.text });
            //ViewData["CourseList"] = new SelectList(MyList.ToList(), "ListID", "ListName");

            //Financial year
            List<CustomMaster> FinancialYearList = new List<CustomMaster>();
            FinancialYearList = Common.GetCustomMastersList(37);
            ViewBag.FinancialYearList = FinancialYearList;
            //var MyFinancialYearListList = (from m in FinancialYearList select new { m.Id, m.text });
            //ViewData["FinancialYearList"] = new SelectList(MyFinancialYearListList.ToList(), "ListID", "ListName");



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

        [HttpPost]
        public JsonResult SaveCollageFee(TrusteeBO.CollageFeeMst modal)
        {
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/AddCollageFee");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ErrorBO objResponseData = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (objResponseData.ResponseCode == "1")
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalMessage"] = "Data saved sussessfully!";
                    TempData["SwalTitleMsg"] = "Success...!";
                    return new JsonResult
                    {
                        Data = new { failure = true, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    return new JsonResult
                    {
                        Data = new { failure = false, msg = "Failed" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            #endregion
            return new JsonResult
            {
                Data = new { failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}