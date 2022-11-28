using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;
using System.Web.Mvc;

namespace NewZapures_V2.Controllers
{
    //[NoDirectAccess]
    public class DocumentLibraryController : Controller
    {
        ResponseData objResponse;
        public ActionResult Index()
        {

            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                var companyType = GetCompanyType();
                //var documentList = GetDocuments();
                ViewBag.companyType = companyType;
                //ViewBag.documentList = documentList;
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }


        public List<Dropdown> GetCompanyType(string CompanyType = "CompanyType")
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetDocumentLibraryData?Type=" + CompanyType);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> d = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                d = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return d;
        }
        public ActionResult GetDocuments(string DocumentList = "DocumentList", int companyId = 0)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetDocumentLibraryData?Type=" + DocumentList + "&companyId=" + companyId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<DocumentLibraryMaster> d = new List<DocumentLibraryMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                d = JsonConvert.DeserializeObject<List<DocumentLibraryMaster>>(objResponse.Data.ToString());
            }
            return View(d);
        }

        public JsonResult SaveDocumentList(List<DocumentLibraryMaster> documents)
        {
            var json = JsonConvert.SerializeObject(documents);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SaveDocumentList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + Token + "");
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