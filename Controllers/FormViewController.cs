using Newtonsoft.Json;
using NewZapures_V2.Models;
using NewZapures_V2.Models.Others;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Common;

namespace NewZapures_V2.Controllers
{
    public class FormViewController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();
        ResponseData objResponse;
        // GET: FormView
        public ActionResult DetailsList()
        {
            var applGUID = SessionModel.ApplicantGuid;
            List<CustomMaster> TrustList = new List<CustomMaster>();

            TrustList = GetTrustDropDownList(28);
            ViewBag.TrustList = TrustList;

            ViewBag.TrustDetails = TrustGeneralInformation();

            ViewBag.CollageApplyList = CollageListForApply();

            var subjects = GetSubjectDataList(applGUID);
            ViewBag.SubjectDataList = subjects;

            var LandData = ZapurseCommonlist.GetLandBuildingInfo(applGUID);
            var AcadmicData = ZapurseCommonlist.GetAcdmcData(applGUID);
            ViewBag.AllData = AcadmicData;

            ViewBag.applGUID = applGUID;
            ViewBag.landDataList = LandData;
            ViewBag.CollageFeeList = FeeList();
            ViewBag.StaffList = StaffDetails(applGUID);
            //ViewBag.collagelist = CollageList();
            #region List Trustee
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/TrusteeList?TrustId=" + SessionModel.TrustId.ToString());
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                List<TrusteeBO.Trustee> _result = _JsonSerializer.Deserialize<List<TrusteeBO.Trustee>>(response.Content);
                if (_result != null)
                {
                    ViewBag.TrusteeList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return View();
        }
        //Trust Commites member Details
        public List<CustomMaster> GetTrustDropDownList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();


            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetTrustDropDownList");
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

        //Trust General info
        [HttpGet]
        public TrusteeBO.TrusteeInfo TrustGeneralInformation()
        {
            string RegNo = SessionModel.TrustRegNo;
            bool status = false;
            TrusteeBO.TrusteeInfo _result = new TrusteeBO.TrusteeInfo();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetTrustInfo?TrustId=" + RegNo);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<TrusteeBO.TrusteeInfo>(response.Content);
                if (_result != null)
                {
                    //for (int i = 0; i < _result.Count; i++)
                    //{
                    if (_result.RegistrationNo != null)
                    {
                        status = true;
                        ViewBag.TrustDetails = _result;
                        ViewData["RegDate"] = _result.RegistrationDate;
                        SessionModel.TrustId = _result.TrusteeInfoId;
                    }
                    //}

                }

            }
            return _result;
        }

        //College List For Apply
        [HttpGet]
        public List<TrusteeBO.CollageList> CollageListForApply()
        {
            List<TrusteeBO.CollageList> _result = new List<TrusteeBO.CollageList>();
            #region List Collage Apply List
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/CollageListApply");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<TrusteeBO.CollageList>>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollageApplyList = _result;
                    //return RedirectToAction("Index");
                }
            }
            #endregion
            return _result;
        }

        //Subject List
        public List<AddCourseBO> GetSubjectDataList(string applGUID)
        {
            //List<AddCourseBO> res = new List<AddCourseBO>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "AddCourseData/GetSubjectList?Guid=" + SessionModel.ApplicantGuid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            List<AddCourseBO> _result = new List<AddCourseBO>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    _result = JsonConvert.DeserializeObject<List<AddCourseBO>>(objResponse.Data.ToString());
            }
            return _result;
            //return View(_result);
            // return RedirectToAction("EditApplication", "SubjectMaster", new { applGUID = SessionModel.ApplicantGuid });
        }
        //Fee List
        public TrusteeBO.CollageFeeMst FeeList()
        {
            TrusteeBO.CollageFeeMst obj = new TrusteeBO.CollageFeeMst();
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
                obj = _JsonSerializer.Deserialize<TrusteeBO.CollageFeeMst>(response.Content);
                //if (_result != null)
                //{
                //    ViewBag.CollageFeeList = _result;
                //    //return RedirectToAction("Index");
                //}
            }
            ViewBag.CollageFeeList = obj;
            return obj;
            //TempData["SwalStatusMsg"] = "success";
            //TempData["SwalMessage"] = "Data saved sussessfully!";
            //TempData["SwalTitleMsg"] = "Success...!";
            #endregion
        }

        //College Facility
        public TrusteeBO.CollageFacility CollageFacility()
        {
            TrusteeBO.CollageFacility modal = new TrusteeBO.CollageFacility();
            modal.Guid = SessionModel.ApplicantGuid;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Trustee/GetCollageFacilityList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            TrusteeBO.CollageFacility _result = new TrusteeBO.CollageFacility();
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<TrusteeBO.CollageFacility>(response.Content);
                if (_result != null)
                {
                    ViewBag.CollageFacilityMster = _result;
                    //return RedirectToAction("Index");
                }
            }
            return _result;
        }
        //Staff List
        public List<StaffBO.Staff> StaffDetails(string applGUID)
        {
            List<StaffBO.Staff> _result = new List<StaffBO.Staff>();
            #region List Staff
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Staff/StaffList?Guid=" + SessionModel.ApplicantGuid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _result = _JsonSerializer.Deserialize<List<StaffBO.Staff>>(response.Content);
                if (_result != null)
                {
                    ViewBag.StaffList = _result;
                    //return RedirectToAction("Index");
                }
            }
            // ViewBag.StaffList = _result;

            return _result;
            #endregion
        }

        //College list Data

        public JsonResult CollageListView()
        {
            try
            {
                List<TrusteeBO.CollageList> _result = new List<TrusteeBO.CollageList>();
                var client = new RestClient(ConfigurationManager.AppSettings["BaseURL"] + "Trustee/CollageListApply");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    _result = _JsonSerializer.Deserialize<List<TrusteeBO.CollageList>>(response.Content);
                    //if (_result != null)
                    //{
                    //   ViewBag.Collage = _result;
                    //    //return RedirectToAction("Index");
                    //}
                }
                return new JsonResult
                {
                    Data = new { Data = _result, Failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }



        }
    }
}