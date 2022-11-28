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
    public class GeographicalController : Controller
    {
        // GET: Geographical
        ResponseData objResponse;
        public ActionResult Index()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                var ddlAddtype = GetAddTypes();
                var AllData = GetGeoGraphicalData();
                ViewBag.AddTypes = ddlAddtype;
                ViewBag.AllData = AllData;
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }

        public JsonResult SaveData(GeographicalMaster graphical)
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            var json = JsonConvert.SerializeObject(graphical);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SaveGeographical");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "bearer " + Token + "");
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
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Type = graphical.Type, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public List<GeographicalTableData> GetGeoGraphicalData()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetGeoGraphicalList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<GeographicalTableData> data = new List<GeographicalTableData>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    data = JsonConvert.DeserializeObject<List<GeographicalTableData>>(objResponse.Data.ToString());
                }
            }
            return data;
        }
        public List<Dropdown> GetAddTypes()
        {
            List<Dropdown> areas = new List<Dropdown>();

            areas.Add(new Dropdown
            {
                Id = "1",
                Text = "Country"
            });
            areas.Add(new Dropdown
            {
                Id = "2",
                Text = "State"
            });
            areas.Add(new Dropdown
            {
                Id = "3",
                Text = "District"
            });
            areas.Add(new Dropdown
            {
                Id = "4",
                Text = "City"
            });
            areas.Add(new Dropdown
            {
                Id = "5",
                Text = "Area"
            });

            return areas;
        }

        public JsonResult GeAllData(string Type, int CountryId, int StateId, int DistrictId, int CityID)
        {
            List<Dropdown> dropdowns = new List<Dropdown>();
            GeographicalMaster geographical = new GeographicalMaster();
            geographical.Type = Type;
            geographical.CountryId = CountryId;
            geographical.StateId = StateId;
            geographical.DistrictId = DistrictId;
            geographical.CityId = CityID;
            var json = JsonConvert.SerializeObject(geographical);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetGeographical");
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

                dropdowns = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = dropdowns, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}