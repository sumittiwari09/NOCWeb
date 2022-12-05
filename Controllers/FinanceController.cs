using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System.Configuration;
using System.Web.Mvc;

namespace NewZapures_V2.Controllers
{
    public class FinanceController : Controller
    {
        ResponseData objResponse;
        public ActionResult Index()
        {
            
            return View();
        }

        public JsonResult SaveDetails(FinanceModalSave financeModal)
        {
            var json = JsonConvert.SerializeObject(financeModal);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Finance/addFinanceDetails");
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
    }
}