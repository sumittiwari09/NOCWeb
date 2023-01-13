using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.EnterpriseServices.Internal;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    public class MasterController : Controller
    {
        // GET: Master
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        public ActionResult DepartmentMapping()
        {
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            List<AddDepartment> departmentList = new List<AddDepartment>();
            departmentList = GetDepartments();
            List<Dropdown> NocdeptName = new List<Dropdown>();
            NocdeptName = GetNOCDepartmentsName(1);
            ViewBag.NocdeptName = NocdeptName;
            List<Dropdown> Nocdepttype = new List<Dropdown>();
            Nocdepttype = GetNOCDepartmentsName(2);
            ViewBag.Nocdepttype = Nocdepttype;
            ViewBag.Department = departmentList;
            List<NOCDEPMAP> lstMap = new List<NOCDEPMAP>();
            lstMap = GetNOCDepartMaplst(0);
            return View(lstMap);
        }
        
        public ActionResult FinancialYear()
        {
            ViewBag.FinYear = GetFinacialyearlist();
            return View();
        }

        public List<FinYearView> GetFinacialyearlist()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetFinYearList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<FinYearView> groups = new List<FinYearView>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    groups = JsonConvert.DeserializeObject<List<FinYearView>>(objResponse.Data.ToString());
                }
            }
            return groups;
        }

        public ActionResult CreateFinacialyear(FinYear Master)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/InsertFinYear");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData CommonResponse = new ResponseData();
                CommonResponse = _JsonSerializer.Deserialize<ResponseData>(response.Content);
                if (CommonResponse.statusCode == 1)
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalTitleMsg"] = "Success!";
                    if (CommonResponse.Message == "Allready Exists In table")
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalTitleMsg"] = "warning!";
                    }
                    TempData["SwalMessage"] = CommonResponse.Message;
                    

                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";

                }
            }
            return RedirectToAction("FinancialYear", "Master");
        }

        public ActionResult AddDepartmentMapping(NOCDEPMAP Master)
        {
            try
            {
                Master.iStts = 1;
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/SaveNocDepartmentMapping");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else
                    if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("DepartmentMapping");
                    }
                    else if (objResponseData.ResponseCode == "001" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("DepartmentMapping");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("DepartmentMapping");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                return RedirectToAction("DepartmentMapping");
            }
            return View();

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
        public List<Dropdown> GetNOCDepartmentsName(int Type = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetNOCDepartmentsName?Type=" + Type);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> departments = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return departments;
        }
        public List<NOCDEPMAP> GetNOCDepartMaplst(int Departid)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetNOCDepartMaplst?Departid=" + Departid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<NOCDEPMAP> departments = new List<NOCDEPMAP>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<NOCDEPMAP>>(objResponse.Data.ToString());
                }
            }
            return departments;
        }
        public List<EVNTMST> GetNOCEventplst(int Departid)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/Event_View?iFk_DeptId=" + Departid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<EVNTMST> departments = new List<EVNTMST>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<EVNTMST>>(objResponse.Data.ToString());
                }
            }
            return departments;
        }

        #region Parameter Category

        public ActionResult ParameterCategoryMapping()
        {
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            return View();
        }
        //[ChildActionOnly]
        public ActionResult AddParameterCategoryMapping(int id = 0)
        {
            List<CustomMaster> UOM = new List<CustomMaster>();
            UOM = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.UOM));
            List<AddDepartment> departmentList = new List<AddDepartment>();
            departmentList = GetDepartments();
            ViewBag.Department = departmentList;
            List<Dropdown> PerameterCategorylst = new List<Dropdown>();
            PerameterCategorylst = GetPerameterCategorylst(0, 0, 3);
            //List<CustomMaster> UOM = new List<CustomMaster>();
            //UOM = Common.GetCustomMastersList(Convert.ToInt32(TypeDocument.UOM));
            ViewBag.UOM = UOM;
            ViewBag.PerameterCategorylst = PerameterCategorylst;
            ViewBag.Id = id;
            ViewBag.UOM = UOM;
            return PartialView("_AddParameterCategoryMapping");
        }
        public ActionResult InsertPerametertable(int Deptid = 0)
        {
            List<PARAMCAT> PerameterCategorylst = new List<PARAMCAT>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetPerameterTableList?Type=" + 0 + "&Deptid=" + Deptid + "&iFk_SelfId=" + 0 + "&EnumId=" + Convert.ToInt32(TypeDocument.UOM));
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<PARAMCAT> departments = new List<PARAMCAT>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<PARAMCAT>>(objResponse.Data.ToString());
                }
            }
            ViewBag.parameterlst = departments;
            return PartialView("_InsertPerametertable");
        }

        public List<Dropdown> GetPerameterCategorylst(int Deptid = 0, int iFk_SelfId = 0, int Typid = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetPerameterCategoryList?Type=" + Typid + "&Deptid=" + Deptid + "&iFk_SelfId=" + iFk_SelfId + "&EnumId=" + Convert.ToInt32(TypeDocument.UOM));
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> departments = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return departments;
        }
        public JsonResult InsertParameterCategoryMapping(PARAMCAT Master)
        {
            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Masters/InsertParameterCategoryMapping");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public JsonResult FillDataPerameter(int Deptid = 0, int iFk_SelfId = 0, int Typid = 0)
        {
            List<Dropdown> departments = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetPerameterCategoryList?Type=" + Typid + "&Deptid=" + Deptid + "&iFk_SelfId=" + iFk_SelfId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
                if (objResponse.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponse.ResponseCode == "000" && objResponse.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        #endregion
        #region ParameterValueConfiguration
        /// <summary>
        /// This method use to set parameter value
        /// Date:08/12/2022
        /// Created by:Abhishek Rohil   
        /// </summary>

        public ActionResult ParameterValueConfiguration()
        {
            List<AddDepartment> departmentList = new List<AddDepartment>();
            departmentList = GetDepartments();
            ViewBag.Department = departmentList;
            List<Dropdown> PerameterCategorylst = new List<Dropdown>();
            PerameterCategorylst = GetPerameterCategorylst(0, 0, 3);
            ViewBag.PerameterCategorylst = PerameterCategorylst;
            List<PARMTVALUCONFMSTView> lst = new List<PARMTVALUCONFMSTView>();
            lst = ParameterValueConfigurationlist();
            return View(lst);
        }


        public JsonResult FillPerameterValue(int Deptid = 0, int CrtgId = 0, int SubCrtgId = 0)
        {
            List<Dropdown> departments = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetCategorySubUOM?Deptid=" + Deptid + "&Category=" + CrtgId + "&SubCategory=" + SubCrtgId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
                if (objResponse.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponse.ResponseCode == "000" && objResponse.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult ParameterConfigCreate(PARMTVALUCONFMST Master)
        {
            try
            {
                Master.iStts = 1;
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/InsertParameterValueConfiguration");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else
                    if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("ParameterValueConfiguration");
                    }
                    else if (objResponseData.ResponseCode == "001" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("ParameterValueConfiguration");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("ParameterValueConfiguration");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";
                return RedirectToAction("ParameterValueConfiguration");
            }
            return RedirectToAction("ParameterValueConfiguration");
        }


        public List<PARMTVALUCONFMSTView> ParameterValueConfigurationlist(int Id = 0, string Appid = null)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/SelectParameterValueConfiguration?Id=" + Id + "&EnumId=" + Convert.ToInt32(TypeDocument.UOM) + "&CourseEnumId=" + Convert.ToInt32(TypeDocument.Course) + "&Appid=" + @Appid);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<PARMTVALUCONFMSTView> departments = new List<PARMTVALUCONFMSTView>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<PARMTVALUCONFMSTView>>(objResponse.Data.ToString());
                }
            }
            return departments;
        }

        #endregion


        public JsonResult FillCoursebyDepartment(int Deptid = 0)
        {
            List<Dropdown> departments = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GetCategorySubUOM?Deptid=" + Deptid + "&Category=" + Deptid + "&SubCategory=" + Convert.ToInt32(TypeDocument.Course) + "&Table=Course");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
                if (objResponse.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponse.ResponseCode == "000" && objResponse.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult ArchitectureDetail(string appNo)
        {
            List<PARMTVALUCONFMSTView> lst = new List<PARMTVALUCONFMSTView>();
            lst = ParameterValueConfigurationlist();
            ViewBag.Applicableid = appNo;

            return View(lst);
        }
        public ActionResult Testing()
        {
            return View();
        } 

        public ActionResult NewArchitectureDetail(string guid= null)
        {
            List<PARMTVALUCONFMSTView> lst = new List<PARMTVALUCONFMSTView>();
            lst = ParameterValueConfigurationlist(0, guid);
            ViewBag.Applicableid = guid;
            return View(lst);
        }

        public JsonResult InsertArchitectureDetail(List<ArchiMstDetail> Master)
        {

            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/InsertArchitectureDetail");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(Master.FirstOrDefault()), ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult BuildalltabelArchitecture(string item)
        {
            List<ArchiMstDetail> departments = new List<ArchiMstDetail>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/BuildalltabelArchitecture?sAppId=" + item);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    departments = JsonConvert.DeserializeObject<List<ArchiMstDetail>>(objResponse.Data.ToString());
                }
                if (objResponse.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponse.ResponseCode == "000" && objResponse.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = departments, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteImage(int ipk_ArchiMstDetId)
        {
            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/DeleteArchiMstDet?Id=" + ipk_ArchiMstDetId);
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public JsonResult UploadCsvFile(int iParamId, int iSubCatId, int iUomId, string Appid, string Type)
        {
            var attachedFile = System.Web.HttpContext.Current.Request.Files["CsvDoc"];
            if (attachedFile == null || attachedFile.ContentLength <= 0)
            {
                return new JsonResult
                {
                    Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            string folder = Server.MapPath(string.Format("~/UploadFloder/{0}/", Appid));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);

            }

            string gs = Server.MapPath("~/UploadFloder/" + Appid + "/");
            string FileName = Type + "-" + iParamId + "-" + iSubCatId + "-" + iUomId + Path.GetExtension(attachedFile.FileName);
            if (System.IO.File.Exists(gs + Type + "-" + iParamId + "-" + iSubCatId + "-" + iUomId))
            {
                System.IO.File.Delete(gs + Type + "-" + iParamId + "-" + iSubCatId + "-" + iUomId);
            }

            attachedFile.SaveAs(gs + FileName);
            ArchUpload upload = new ArchUpload();
            upload.sFK_AppId = Appid;
            upload.iParamId = iParamId;
            upload.iSubCatId = iSubCatId;   
            upload.iUomId = iUomId;
            upload.UploadUrl =FileName;
            upload.Type = Type;
            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/InsertArchupload");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(upload), ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            return new JsonResult
            {
                Data = new { Data = "", failure = false, msg = FileName, isvalid = 1 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult InsertArchitectureMst(int iParamId, int iSubCatId, int iUomId, string sAppId, string value)
        {
            ArchitectureMst mst = new ArchitectureMst();
            mst.iParamId = iParamId;
            mst.iSubCatId = iSubCatId;
            mst.iUom = iUomId;
            mst.iFK_AppId = sAppId;
            mst.Value = value;
            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/InsertArchitectureMst");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(mst), ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult NewArchitectureView(string applGUID = "abhc123")
        {
            List<PARMTVALUCONFMSTView> lst = new List<PARMTVALUCONFMSTView>();
            lst = ParameterValueConfigurationlist(0, applGUID);

            List<ArchiMstDetail> LstApesData = new List<ArchiMstDetail>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/AllGenerateArchtable?sAppId=" + applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    LstApesData = JsonConvert.DeserializeObject<List<ArchiMstDetail>>(d.Data.ToString());
                }

            }
            ViewBag.LstApesData = LstApesData;
            ViewBag.applGUID = applGUID;
            return View(lst);
        }

        public JsonResult AllGenerateArchtablelist(string applGUID)
        {
            List<ArchiMstDetail> LstApesData = new List<ArchiMstDetail>();
            List<ArchiMstData> LstData = new List<ArchiMstData>();
            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/AllGenerateArchtable?sAppId=" + applGUID);
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client2.Execute(request2);
            if (response.StatusCode.ToString() == "OK")
            {

                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    LstApesData = JsonConvert.DeserializeObject<List<ArchiMstDetail>>(objResponse.Data.ToString());
                    foreach (var item in LstApesData.Select(p => new { p.iParamId, p.iSubCatId, p.iUomId }).Distinct().ToList())
                    {
                        ArchiMstData obj = new ArchiMstData();
                        obj.iParamId = item.iParamId;
                        obj.iSubCatId = item.iSubCatId;
                        obj.iUomId = item.iUomId;
                        LstData.Add(obj);
                    }
                }
                if (objResponse.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = LstData, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponse.ResponseCode == "000" && objResponse.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = LstData, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        //public ActionResult NewArchitectureDetail(string guid)
        //{
        //    List<PARMTVALUCONFMSTView> lst = new List<PARMTVALUCONFMSTView>();
        //    lst = ParameterValueConfigurationlist();
        //    return View(lst);
        //}

        public ActionResult EventMaster()
        {
            List<AddDepartment> departmentList = new List<AddDepartment>();
            departmentList = GetDepartments();
            ViewBag.Department = departmentList;
            return View();
        }
        public ActionResult EventMasterTable(int DepartmentId,string type)
        {
            List<NOCDEPMAP> lstMap = new List<NOCDEPMAP>();
            List<EVNTMST> lstEvent = new List<EVNTMST>();
            if (type== "Search")
            {
                lstMap = GetNOCDepartMaplst(DepartmentId);
               
            }
            if (type == "View")
            {
                lstEvent = GetNOCEventplst(DepartmentId);
            }
            ViewBag.lstEvent=lstEvent;
            return View(lstMap);
        }

        public JsonResult InsertEvent(List<EventMstSave> MstSave)
        {
            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/InsertEvent");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(MstSave.FirstOrDefault()), ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.Message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }
            }

            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult CommiteeMaster( int Id=0)
        {
            List<Dropdown> Prtlst = new List<Dropdown>();
            Prtlst = Common.GetDropDown(8, "PARTYCODE");
            List<CommiteeMaster> Commiteelst = new List<CommiteeMaster>();
            Commiteelst = GetCommitee(8);
            ViewBag.Prtlst=Prtlst;
            ViewBag.deptid = 8;
            ViewBag.Commiteelst = Commiteelst;
            CommiteeMaster Mst = new CommiteeMaster();
            if (Id != 0)
            {
                Mst = Commiteelst.Where(p => p.iPk_CommiteeId == Id).FirstOrDefault();
            }
            return View(Mst);
        }
        [HttpPost]
        public ActionResult CommiteeMasterSave(CommiteeMaster Commitee)
        {
           Commitee.sComtMemLst = Request["sComtMemLst"].ToString();
  
            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/AllCommiteeSaveView");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json",_JsonSerializer.Serialize(Commitee), ParameterType.RequestBody);
            IRestResponse response = client2.Execute(request2);
            try
            {
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else
                    if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("CommiteeMaster");
                    }
                    else if (objResponseData.ResponseCode == "001" && objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("CommiteeMaster");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("CommiteeMaster");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalMessage"] = "Something wrong";
                TempData["SwalTitleMsg"] = "error!";

            }
            return RedirectToAction("CommiteeMaster");
        }

        public static List<CommiteeMaster> GetCommitee(int Id)
        {
            List<CommiteeMaster> obj = new List<CommiteeMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Masters/GetCommiteeList?Id=" + Id );
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    obj = JsonConvert.DeserializeObject<List<CommiteeMaster>>(objResponse.Data.ToString());
                }
            }
            return obj;
        }
    }
}