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
    [NoDirectAccess]
    public class ManageMenuOrderController : Controller
    {
        // GET: ManageMenuOrder
        public ActionResult Index()
        {
            var List = GetMenusList();

            ViewBag.serviceDetailsList = List;

            return View();
        }

        public List<Dropdown> GetMenusList(string Type = "MenuList", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public JsonResult GetSubmenus(string Type = "Submenus", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            ResponseData objResponse = null;
            if (response.StatusCode.ToString() == "OK")
            {
                 objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = MenusList, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult UpdateDisplayID(string DisplayorderID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UpdateDisplayID?ID=" + DisplayorderID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            ResponseData objResponse = null;
            if (response.StatusCode.ToString() == "OK")
            {
                 objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                //MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = MenusList, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}