using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Common;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
          // GET: Rate
    public class RateController : Controller
     {
            JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
            CommonFunction objcf = new CommonFunction();

            public ActionResult SpecialRateMaster(int Id)
            {
                ViewBag.Id = Id;
                return View();
            }
            public ActionResult Details()
            {
                List<RateMaster> objUsermaster = new List<RateMaster>();
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/GetRateDetail");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var data = JsonConvert.DeserializeObject<RateMaster>(response.Content);
                    RateMasterData objResponseData = JsonConvert.DeserializeObject<RateMasterData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000")
                    {
                        objUsermaster = objResponseData.Data.ListRequest;
                    }
                }
                return View(objUsermaster);
            }

            public ActionResult Create(RateMaster objRatemaster = null)
            {
                GetDistrictType();
            long Id = Convert.ToInt64(Request.RequestContext.RouteData.Values["Id"]);
                if (Id != 0)
                {
                    objRatemaster = new RateMaster();
                    try
                    {
                        var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/GetRateDetail?Id=" + Id + "");
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("cache-control", "no-cache");
                        // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                        request.AddParameter("application/json", "", ParameterType.RequestBody);
                        IRestResponse response = client.Execute(request);
                        if (response.StatusCode.ToString() == "OK")
                        {
                            var data = JsonConvert.DeserializeObject<RateMaster>(response.Content);
                            RateMasterData objResponseData = JsonConvert.DeserializeObject<RateMasterData>(response.Content);
                            if (objResponseData.ResponseCode == "001")
                            {
                                return RedirectToAction("SignOut", "Home");
                            }
                            else if (objResponseData.ResponseCode == "000")
                            {
                                objRatemaster = objResponseData.Data.ListRequest.FirstOrDefault();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string message = ex.Message;
                    }
                }
                GetDepartmentServiceList();

                return View(objRatemaster);
            }

            #region Department List
            public ActionResult GetDepartmentServiceList()
            {
                List<DocumentBO> objList = new List<DocumentBO>();
                var client3 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetDepartmentServiceList");
                var request3 = new RestRequest(Method.GET);
                request3.AddHeader("cache-control", "no-cache");
                //  request3.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request3.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response3 = client3.Execute(request3);
                if (response3.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response3.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000")
                    {
                        if (objResponseData.Data == null || objResponseData.Data == "")
                        {
                            var listitem = new SelectList(null, "MstCategoryID", "Category");
                            ViewBag.Categorylist = objList;
                        }
                        else
                        {
                            objList = JsonConvert.DeserializeObject<List<DocumentBO>>(objResponseData.Data.ToString());
                            var listitem = new SelectList(objList, "MstCategoryID", "Category");
                            ViewBag.Categorylist = listitem.Items;
                        }
                    }
                }
                else
                {
                    return RedirectToAction("SignOut", "Home");
                }
                return View();
            }

            #endregion

            [HttpPost]
            public ActionResult Edit(RateMaster objservice)
            {
                try
                {
                    var userdetailsSession = (UserModelSession)Session["UserDetails"];
                    objservice.CollectionTime = Request["CollectionTime"];
                    //objservice.CreatedByName = userdetailsSession.Username;
                    //objservice.CreatedByID = userdetailsSession.PartyId;
                    //objservice.CreatedIP = objcf.getIPAdd();
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/InsertUpdateRate");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddParameter("application/json", _JsonSerializer.Serialize(objservice), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        ResponseDataBO objResponseData = JsonConvert.DeserializeObject<ResponseDataBO>(response.Content);
                        if (objResponseData.ResponseCode == "001")
                        {
                            return RedirectToAction("SignOut", "Home");
                        }
                        else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                        {
                            TempData["SwalStatusMsg"] = "success";
                            TempData["SwalMessage"] = objResponseData.Message;
                            TempData["SwalTitleMsg"] = "Success!";
                            return RedirectToAction("Details");
                        }
                        else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                        {
                            TempData["SwalStatusMsg"] = "warning";
                            TempData["SwalMessage"] = objResponseData.Message;
                            TempData["SwalTitleMsg"] = "warning!";
                            return RedirectToAction("Details");
                        }
                        else
                        {
                            TempData["SwalStatusMsg"] = "error";
                            TempData["SwalMessage"] = "Something wrong";
                            TempData["SwalTitleMsg"] = "error!";
                            return RedirectToAction("Details");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    return RedirectToAction("Details");
                }
                return RedirectToAction("Details");
            }

            [HttpPost]
            public ActionResult SpecialCategoryEdit(RateSpecialCategory objservice)
            {
                try
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/InsertUpdateSpecialCategory");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddParameter("application/json", _JsonSerializer.Serialize(objservice), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        ResponseDataBO objResponseData = JsonConvert.DeserializeObject<ResponseDataBO>(response.Content);
                        if (objResponseData.ResponseCode == "001")
                        {
                            return RedirectToAction("SignOut", "Home");
                        }
                        else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                        {
                            TempData["SwalStatusMsg"] = "success";
                            TempData["SwalMessage"] = objResponseData.Message;
                            TempData["SwalTitleMsg"] = "Success!";
                            return RedirectToAction("Details");
                        }
                        else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                        {
                            TempData["SwalStatusMsg"] = "warning";
                            TempData["SwalMessage"] = objResponseData.Message;
                            TempData["SwalTitleMsg"] = "warning!";
                            return RedirectToAction("Details");
                        }
                        else
                        {
                            TempData["SwalStatusMsg"] = "error";
                            TempData["SwalMessage"] = "Something wrong";
                            TempData["SwalTitleMsg"] = "error!";
                            return RedirectToAction("Details");
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";
                    return RedirectToAction("Details");
                }
                return RedirectToAction("Details");

            }
            public JsonResult ActiveRecordStatus(long Id = 0, int TypeId = 0)
            {
                RateMaster objservice = new RateMaster();
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                //objservice.ModifyByName = userdetailsSession.Username;
                //objservice.ModifyByID = userdetailsSession.PartyId;
                //objservice.ModifyIP = objcf.getIPAdd();
                objservice.IsActive = TypeId;
                objservice.RateMasterId = (int)Id;

                var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/ChangeStatus");
                var request2 = new RestRequest(Method.POST);
                request2.AddHeader("cache-control", "no-cache");
                // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request2.AddParameter("application/json", _JsonSerializer.Serialize(objservice), ParameterType.RequestBody);
                IRestResponse response2 = client2.Execute(request2);
                if (response2.StatusCode.ToString() == "OK")
                {
                    ServiceTypeDocumentData objResponseData = JsonConvert.DeserializeObject<ServiceTypeDocumentData>(response2.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return new JsonResult
                        {
                            Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        return new JsonResult
                        {
                            Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
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

            public JsonResult FillSpecialCategoryWiseRate(int Id, long RateMasterId)
            {
                RateSpecialCategory Obj = new RateSpecialCategory();
                Obj.RateMasterId = (Int32)RateMasterId;
                Obj.SpecialMasterid = Id;

                var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/FillSpecialCategoryWiseRate?ID=" + Id + " &RateMasterId=" + RateMasterId + "");
                var request2 = new RestRequest(Method.GET);
                request2.AddHeader("cache-control", "no-cache");
                //request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request2.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response2 = client2.Execute(request2);
                if (response2.StatusCode.ToString() == "OK")
                {
                    RateSpecialCategoryData objResponseData = JsonConvert.DeserializeObject<RateSpecialCategoryData>(response2.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return new JsonResult
                        {
                            Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        return new JsonResult
                        {
                            Data = new { Data = objResponseData.Data.ListRequest, failure = false, msg = "Success" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };

                    }
                }
                return new JsonResult
                {
                    Data = new { Data = "", failure = true, msg = "Failed" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };

            }

            #region For Datartment Show Distrct
            [HttpGet]
            public ActionResult GetDistrictType()
            {
                var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Mastersddl/GetDistrictList");
                var request2 = new RestRequest(Method.GET);
                request2.AddHeader("cache-control", "no-cache");
                // request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                IRestResponse response2 = client2.Execute(request2);
                if (response2.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response2.Content);
                    List<Mastersddl> objlist = JsonConvert.DeserializeObject<List<Mastersddl>>(objResponseData.Data.ToString());
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000")
                    {
                        //ViewData["District"] = new SelectList(objResponseData.Data.ListRequest, "ID", "Name");
                        var listitem = new SelectList(objlist, "ID", "Name");
                        ViewBag.Districtlist = listitem.Items;
                    }
                }
                return View();
            }
            #endregion

            #region Department List
            public JsonResult FillDepartmentServiceList(string districtID)
            {
                var listitem = new SelectList(null, "MstCategoryID", "Category");
                List<DocumentBO> objList = new List<DocumentBO>();
                var client3 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetDepartmentServiceList");
                var request3 = new RestRequest(Method.GET);
                request3.AddHeader("cache-control", "no-cache");
                //  request3.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request3.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response3 = client3.Execute(request3);
                if (response3.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response3.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return new JsonResult
                        {
                            Data = new { Data = listitem, failure = false, msg = "Success" },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    else if (objResponseData.ResponseCode == "000")
                    {
                        if (objResponseData.Data == null || objResponseData.Data == "")
                        {
                            return new JsonResult
                            {
                                Data = new { Data = listitem, failure = false, msg = "Success" },
                                ContentEncoding = System.Text.Encoding.UTF8,
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }
                        else
                        {
                            objList = JsonConvert.DeserializeObject<List<DocumentBO>>(objResponseData.Data.ToString());
                            listitem = new SelectList(objList, "MstCategoryID", "Category");
                            return new JsonResult
                            {
                                Data = new { Data = listitem, failure = false, msg = "Success" },
                                ContentEncoding = System.Text.Encoding.UTF8,
                                JsonRequestBehavior = JsonRequestBehavior.AllowGet
                            };
                        }
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
        }
   
}