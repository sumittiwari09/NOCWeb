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
    //[NoDirectAccess]
    public class ContentPageController : Controller
    {
        // GET: ContentPage
        ResponseData objResponse;

        public ActionResult IndexPage(string RowId = "")
        {
            var templatesType = AdminController.GetAllTemplatesType();
            if (RowId != "")
            {
                ContentPage content = new ContentPage();

                content.Id = RowId.ToString();
                content.Type = "DataForEdit";

                var acctualData = EditContent(content);

                ViewBag.templatesTypeList = templatesType;
                ViewBag.TemplateId = acctualData[0].EnumId;
                ViewBag.HTMLContent = acctualData[0].PageContent;
                ViewBag.PageTitle = acctualData[0].PageTitle;
                ViewBag.rowId = acctualData[0].Id;
            }
            else
            {
                ViewBag.templatesTypeList = templatesType;
                ViewBag.TemplateId = 0;
                ViewBag.HTMLContent ="";
                ViewBag.PageTitle = "";
                ViewBag.rowId ="0";
            }

            return View();
        }

        public ActionResult Index()
        {
            var Templates = GetAllTemplates();

            ViewBag.TemplatesList = Templates;

            return View();
        }
        public ActionResult GetDetailsForEdit(string Id = "")
        {
            var Templates = GetTemplateList();

            Templates = Templates.Where(wh => wh.Id == Id).ToList();
            ViewBag.Templates = Templates;
            return View();
        }
        public List<ContentPage> EditContent(ContentPage content)
        {
            var json = JsonConvert.SerializeObject(content);

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/EditContentData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

           List<ContentPage> contents = new List<ContentPage>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                contents = JsonConvert.DeserializeObject<List<ContentPage>>(objResponse.Data.ToString());

            }

            return contents;

        }
        public ActionResult List()
        {
            var Templates = GetTemplateList();
            ViewBag.Templates = Templates;
            return View();
        }
        public ActionResult ViewContent(string Id)
        {
            ContentPage content = new ContentPage();

            content.Id = Id;
            content.Type = "Data";

            var json = JsonConvert.SerializeObject(content);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/EditContentData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<ContentPage> mainData = new List<ContentPage>();

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                mainData = JsonConvert.DeserializeObject<List<ContentPage>>(objResponse.Data.ToString());
            }

            ViewBag.Data = mainData[0];
            return View();
        }
        public List<ContentPage> GetAllTemplates()
        {
            ContentPage content = new ContentPage();
            content.Type = "DataNew";

            var json = JsonConvert.SerializeObject(content);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/EditContentData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<ContentPage> allTemplates = new List<ContentPage>();

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                allTemplates = JsonConvert.DeserializeObject<List<ContentPage>>(objResponse.Data.ToString());
            }

            return allTemplates;

        }
        public JsonResult SaveContent(ContentPage content)
        {
            var json = JsonConvert.SerializeObject(content);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SaveContentData");
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
        public JsonResult UpdateContent(ContentPage content)
        {
            var json = JsonConvert.SerializeObject(content);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/EditContentData");
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
        public List<Dropdown> GetTemplateList()
        {
            ContentPage content = new ContentPage();

            content.Type = "List";

            var json = JsonConvert.SerializeObject(content);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/EditContentData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<Dropdown> Templates = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                Templates = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }

            return Templates;

        }

        public ActionResult SummerNote()
        {

            return View();
        }
    }
}