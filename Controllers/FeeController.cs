using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewZapures_V2.Controllers
{
    public class FeeController : Controller
    {
        ResponseData objResponse;
        public ActionResult Index()
        {
            var departments = GetDepartments();
            var feeTypes = ZapurseCommonlist.GetFeeType();
            var feeDataList = GetFeeData();
            ViewBag.Department = departments;
            ViewBag.feeType = feeTypes;
            ViewBag.feeDataList = feeDataList;
            return View();
        }

        public JsonResult SaveDetails(FeeMaster fee)
        {
            var json = JsonConvert.SerializeObject(fee);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Fee/addFeeDetails");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public List<AddDepartment> GetDepartments(string Type = "DepartmentList")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=0");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AddDepartment> departments = new List<AddDepartment>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                departments = JsonConvert.DeserializeObject<List<AddDepartment>>(objResponse.Data.ToString());
            }
            return departments;
        }
        public List<FeeMaster> GetFeeData()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Fee/GetFeeData");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<FeeMaster> feeMasters = new List<FeeMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                feeMasters = JsonConvert.DeserializeObject<List<FeeMaster>>(objResponse.Data.ToString());
            }
            return feeMasters;
        }


        public JsonResult GetDepartmentNOC(string MenuId = "0", string Type = "DEPTNOC")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = modules, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}