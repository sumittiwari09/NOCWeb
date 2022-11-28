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

namespace NewZapures_V2.Controllers
{
    public class SlotController : Controller
    {
        // GET: Slot
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();

        public ActionResult Details()
        {
            List<SlotMaster> master = new List<SlotMaster>();
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/GetSlotMasterDetail?Type=1");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                //// request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var data = JsonConvert.DeserializeObject<SlotMaster>(response.Content);
                    ListSlotMasterData objResponseData = JsonConvert.DeserializeObject<ListSlotMasterData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000")
                    {
                        master = objResponseData.Data.ListRequest;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View(master);
        }

        public ActionResult create(SlotMaster master = null, string Id = "")
        {
            // string Id = Request.RequestContext.RouteData.Values["Id"].ToString();

            if (Id != "")
            {


                //Id = "aac5049a-b8a4-42e0-824a-c9491f06eed7";
                master = new SlotMaster();
                try
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/GetSlotMasterDetail?ID=" + Id + "");
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("cache-control", "no-cache");
                    // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddParameter("application/json", "", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        var data = JsonConvert.DeserializeObject<SlotMaster>(response.Content);
                        ListSlotMasterData objResponseData = JsonConvert.DeserializeObject<ListSlotMasterData>(response.Content);
                        if (objResponseData.ResponseCode == "001")
                        {
                            return RedirectToAction("SignOut", "Home");
                        }
                        else if (objResponseData.ResponseCode == "000")
                        {
                            master = objResponseData.Data.ListRequest.FirstOrDefault();
                        }
                    }
                }
                catch (Exception ex)
                {
                    string message = ex.Message;
                }
            }
            else
            {
                Id = Guid.NewGuid().ToString();

            }
            ViewBag.Id = Id;
            return View(master);
        }

        public JsonResult AddInnerData(long SlotColorId, int enquiry, string Id, int UpdateId = 0)
        {

            SlotEnquiryMasterTemp objservice = new SlotEnquiryMasterTemp();
            objservice.ColourId = SlotColorId;
            objservice.NoofEnquiry = enquiry;
            objservice.SlotMasterId = Id;
            objservice.Id = UpdateId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/InsertUpdateSlotEnquiryMaster");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", _JsonSerializer.Serialize(objservice), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {

                        Data = new { Data = objResponseData, failure = false, msg = "Success" },

                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData, failure = false, msg = "Success" },
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
        //public ActionResult AddInnerData(string color,int enquiry,string Id)
        //{
        //    return View();
        //}
        public JsonResult FillDataBlockArea(long DistrictId, long BlockId = 0, long AreaId = 0)
        {
            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Mastersddl/FillDataBlockArea?DistrictId=" + DistrictId + " &BlockId=" + BlockId + " &AreaId=" + AreaId);
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("cache-control", "no-cache");
            ////request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                MastersddlData objResponseData = JsonConvert.DeserializeObject<MastersddlData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListMasters, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListMasters, failure = false, msg = "Success" },
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

        public JsonResult FillSlotColor()
        {


            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Mastersddl/FillSlotColor");
            var request2 = new RestRequest(Method.GET);
            request2.AddHeader("cache-control", "no-cache");
            //request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response2 = client2.Execute(request2);
            if (response2.StatusCode.ToString() == "OK")
            {
                MastersddlData objResponseData = JsonConvert.DeserializeObject<MastersddlData>(response2.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListMasters, failure = false, msg = "Success" },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData.Data.ListMasters, failure = false, msg = "Success" },
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
        public ActionResult ShowInnerData(string Id)
        {
            List<SlotEnquiryMasterTemp> ser = new List<SlotEnquiryMasterTemp>();
            // List<Mastersddl> allServices = OneDigitalIdentity.Models.CommonUtils.GetAllServivcesList();//get data from application cache
            // if (allServices == null)
            // {
            var client3 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/GetAllSlotColor?ID=" + Id);
            var request3 = new RestRequest(Method.GET);
            request3.AddHeader("cache-control", "no-cache");
            //  request3.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request3.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response3 = client3.Execute(request3);
            if (response3.StatusCode.ToString() == "OK")
            {
                ListSlotEnquiryMasterTempData objResponseData = JsonConvert.DeserializeObject<ListSlotEnquiryMasterTempData>(response3.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return RedirectToAction("SignOut", "Home");
                }
                else if (objResponseData.ResponseCode == "000")
                {
                    // OneDigitalIdentity.Models.CommonUtils.SetServivcesList(objResponseData.Data.ListRequest);//save data into application cache
                    //ViewData["Category"] = new SelectList(objResponseData.Data.ListRequest, "MstCategoryID", "Category");
                    var listitem = new SelectList(objResponseData.Data.ListRequest);

                    ViewBag.list = listitem.Items;
                }

            }
            else
            {
            }
            return View();
        }

        public JsonResult DeleteSlotEnquiry(long Id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/DeleteSlotEnquiry?Id=" + Id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {

                        Data = new { Data = objResponseData, failure = false, msg = "Success" },

                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData, failure = false, msg = "Success" },
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

        public JsonResult DeleteFreezeDate(long Id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/DeleteFreezeDate?Id=" + Id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {

                        Data = new { Data = objResponseData, failure = false, msg = "Success" },

                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = objResponseData, failure = false, msg = "Success" },
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
        public ActionResult EditSlotEnquiry(SlotMaster master)
        {
            var SlotList = SlotDropdown.GetSlotList();

            string selectcheck = "";
            foreach (var item in SlotList)
            {
                int id = Convert.ToInt32(Request["Checkbox_" + item.Id]);
                if (id == 1)
                {
                    selectcheck = selectcheck + item.Id + ',';
                }

            }
            master.ActivateSlot = selectcheck;
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                master.CreatedByName = userdetailsSession.Username;
                master.CreatedByID = userdetailsSession.PartyId;

                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/InsertUpdateSlot");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", _JsonSerializer.Serialize(master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
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

        public ActionResult AdminfreezeDate(string Id)
        {
            ViewBag.Id = Id;
            SlotMaster master = new SlotMaster();
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/GetSlotMasterDetail?ID=" + Id + "");
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var data = JsonConvert.DeserializeObject<SlotMaster>(response.Content);
                    ListSlotMasterData objResponseData = JsonConvert.DeserializeObject<ListSlotMasterData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000")
                    {
                        master = objResponseData.Data.ListRequest.FirstOrDefault();
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View(master);
        }

        public ActionResult SaveFeezeDate(FreezeSlot freeze)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                freeze.CreatedByName = userdetailsSession.Username;
                freeze.CreateById = userdetailsSession.PartyId;

                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/InsertFreezeDate");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", _JsonSerializer.Serialize(freeze), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseData objResponseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
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

        public ActionResult ShowFreezeData(string Id)
        {
            List<FreezeSlot> master = new List<FreezeSlot>();
            try
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/GetFreezeSlotDetail?Id=" + Id);
                var request = new RestRequest(Method.GET);
                request.AddHeader("cache-control", "no-cache");
                // request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var data = JsonConvert.DeserializeObject<FreezeSlot>(response.Content);
                    ListFreezeSlotData objResponseData = JsonConvert.DeserializeObject<ListFreezeSlotData>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000")
                    {
                        master = objResponseData.Data.ListRequest;
                        //master = objResponseData.Data.ListRates;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return View(master);
        }

        public JsonResult ActiveRecordStatus(String Id, int TypeId = 0)
        {
            SlotMaster objservice = new SlotMaster();

            objservice.IsActive = TypeId;
            objservice.SlotMasterId = Id;

            var client2 = new RestClient(ConfigurationManager.AppSettings["URL"] + "Rate/SlotChangeStatus");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            //request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
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
    }
}