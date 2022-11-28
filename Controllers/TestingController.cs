using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Common;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    public class TestingController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        public ActionResult CardDetail()
        {
            List<Testing> obj = new List<Testing>();
            List<Testing> objCaterMastermaster = new List<Testing>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Test/GetServicesDetailWithRate");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                //var data = _JsonSerializer.Deserialize<CategoryMaster>(response.Contentd);
                //CategoryMasterData objResponseData = _JsonSerializer.Deserialize<CategoryMasterData>(response.Content);
                //objUsermaster = objResponseData.ListRequest;

                if (d.ResponseCode == "001")
                {
                    return RedirectToAction("SignOut", "Home");
                }
                else if (d.ResponseCode == "000" && d.Data != null)
                {
                
                    var objResponseData = JsonConvert.DeserializeObject<ListTesting>(d.Data.ToString());
                    objCaterMastermaster = objResponseData.ListRequest;
                   var item = objCaterMastermaster.Select(p => new { p.CategoryName, p.CategoryId, p.ClassName }).Distinct();
                    foreach(var data in item )
                    {
                        Testing obj1 = new Testing();
                        obj1.ClassName = data.ClassName;
                        obj1.CategoryName = data.CategoryName;
                        obj1.CategoryId = data.CategoryId;
                        obj.Add(obj1);
                    }
                    
                }
            }
            ViewBag.objdata = obj;
            return View(objCaterMastermaster);
        }
    }
}