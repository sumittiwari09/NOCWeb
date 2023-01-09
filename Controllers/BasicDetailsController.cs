using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static NewZapures_V2.Models.Common;
using System.Web.Script.Serialization;
using System.IO;

namespace NewZapures_V2.Controllers
{
    public class BasicDetailsController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: BasicDetails
        public ActionResult CreateDetails()
        {
            List<CustomMaster> TrustList = new List<CustomMaster>();
            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;

            var universityList = ZapurseCommonlist.GetUniversities();
            var collegeType = ZapurseCommonlist.GetCollegeType();

            ViewBag.universityCollection = universityList;
            ViewBag.collegeTypeList = collegeType;

            #region Get District
            List<Dropdown> DistrictList = new List<Dropdown>();
            DistrictList = Common.GetLocationDropDown("District");
            ViewBag.DistrictList = DistrictList;
            #endregion
            return View();
        }
        public ActionResult ShowDetails()
        {
            return View();
        }
        public List<CustomMaster> GetTrustDropDownList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();


            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/GetTrustDropDownList");
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
        public ActionResult SaveDetails(BasicDetailsBO trg, HttpPostedFileBase collageLogo)
        {
            byte[] Documentbyte;
            string extension = string.Empty;
            string ContentType = string.Empty;
            #region Collage Logo
            if (collageLogo != null)
            {
                extension = Path.GetExtension(collageLogo.FileName);
                ContentType = collageLogo.ContentType;
                using (Stream inputStream = collageLogo.InputStream)
                {
                    MemoryStream memoryStream = inputStream as MemoryStream;
                    if (memoryStream == null)
                    {
                        memoryStream = new MemoryStream();
                        inputStream.CopyTo(memoryStream);
                    }
                    Documentbyte = memoryStream.ToArray();
                    trg.CollageLogo = Convert.ToBase64String(Documentbyte);
                    trg.CollageLogoExtension = extension;
                    trg.CollageLogoContenttype = ContentType;
                }
            }
            #endregion

            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/BasicDetailConfigure");
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
                    return RedirectToAction("CreateDetails", "BasicDetails");
                }
                else
                {
                    TempData["isSaved"] = 0;
                    TempData["msg"] = " Details Not Saved...";
                    return RedirectToAction("CreateDetails", "BasicDetails");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //return RedirectToAction("CreateDetails");
        }

        [HttpPost]
        public JsonResult ContactSaveDetails(BasicDetailsBO trg)
        {
            try
            {
                trg.TrustInfoId = SessionModel.TrustId;
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(trg);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/BasicDetailConfigure");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    //TempData["SwalStatusMsg"] = "success";
                    //TempData["SwalMessage"] = "Data saved sussessfully!";
                    //TempData["SwalTitleMsg"] = "Success...!";
                    //ContactList();

                    return new JsonResult
                    {
                        Data = new { StatusCode = objResponse.statusCode, Failure = true },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    //return RedirectToAction("CreateDetails","BasicDetails");
                }
                else
                {
                    //TempData["SwalStatusMsg"] = "error";
                    //TempData["SwalMessage"] = "Something wrong";
                    //TempData["SwalTitleMsg"] = "error!";

                    return new JsonResult
                    {
                        Data = new { StatusCode = objResponse.statusCode,Failure = false },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                    //return RedirectToAction("CreateDetails", "BasicDetails");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }           
        }

        [HttpGet]
        public ActionResult CollageList()
        {
            #region List Collage Apply List
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/CollageListApply");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.CollageList> _result = _JsonSerializer.Deserialize<List<TrusteeBO.CollageList>>(response.Content);
                if (_result != null)
                {
                    ViewBag.collagelist = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }

        //[HttpGet]
        //public ActionResult ContactList()
        //{
        //    #region List Contact Apply List
        //    var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "BasicDataDetails/ContactDetailConfigure");
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("cache-control", "no-cache");
        //    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //    request.AddParameter("application/json", "", ParameterType.RequestBody);
        //    IRestResponse response = client.Execute(request);
        //    if (response.StatusCode.ToString() == "OK")
        //    {
        //        List<BasicDetailsBO> _result = _JsonSerializer.Deserialize<List<BasicDetailsBO>>(response.Content);
        //        if (_result != null)
        //        {
        //            ViewBag.ContactList = _result;
        //            //return RedirectToAction("Index");
        //        }
        //    }
        //    #endregion
        //    return View();
        //}

        public JsonResult GetTehsilList(string DistrictId)
        {
            try
            {
                var id = Convert.ToInt32(DistrictId);
                List<Dropdown> TehsilList = new List<Dropdown>();
                TehsilList = Common.GetLocationDropDown("Tehsil", id);
                var List = TehsilList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();

                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }

        public JsonResult GetCityList(string Id)
        {
            try
            {
                var id = Convert.ToInt32(Id);
                List<Dropdown> DistrictList = new List<Dropdown>();
                DistrictList = Common.GetLocationDropDown("City",id);
                var List= DistrictList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();
                
                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {                
                throw;
            }
        }

        public JsonResult GetBlockList(string Id)
        {
            try
            {
                var id = Convert.ToInt32(Id);
                List<Dropdown> BlockList = new List<Dropdown>();
                BlockList = Common.GetLocationDropDown("Block", id);
                var List = BlockList.Select(x => new { x.Text, x.Id }).ToList().Select(y => new SelectListItem { Text = y.Text, Value = Convert.ToString(y.Id) }).OrderBy(x => x.Text).ToList();

                return Json(List.OrderBy(X => X.Text).ToList(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}