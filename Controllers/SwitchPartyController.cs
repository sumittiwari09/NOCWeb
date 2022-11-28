using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NewZapures_V2.Controllers
{
    public class SwitchPartyController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        // GET: SwitchClient
        //string URL = ConfigurationManager.AppSettings["URL"].ToString();

        #region Switch Client AddedBy:Shipra
        [HttpGet]
        public ActionResult Index()
        {
            var servicesCollection = (Data)Session["UserAllDetails"];
            ListRequest obj = new ListRequest();
            obj.PartyID = servicesCollection.userDetails.partyId;
            try
            {
                #region VendorType
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/VendorType");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddHeader("LoginID", servicesCollection.userDetails.partyId);
                request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                    if (LoginObj.ResponseCode == "1")
                    {
                        ViewData["PartyType"] = new SelectList(LoginObj.Data, "ListID", "ListName");
                    }
                    else if (LoginObj.ResponseCode == "0")
                    {
                        return RedirectToAction("Logout", "Home");
                    }
                    else
                    {
                        ViewData["PartyType"] = null;
                    }
                    //TempData["Type"] = CommonFunctions.getResponseType(LoginObj.ResponseCode);

                }
                else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                {
                    servicesCollection.userDetails.partyId = "";
                    //CurrentSessions.Token = "";
                    TempData["Type"] = "warning";
                    TempData["Message"] = "Session Expired! Please login again.";
                    TempData["Flag"] = "-1";
                    return RedirectToAction("logout", "Home");
                }
                else
                {
                    TempData["Type"] = "error";
                    TempData["Message"] = "Details Not Found-Ex! Please try after sometime.";
                }
                #endregion
            }
            catch (Exception ex)
            {
                TempData["Type"] = "error";
                TempData["Message"] = "Details Not Found-Ex1! Please try after sometime.";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Index(SwitchVendorRequest obj)
        {
            if (obj.PartyType != null)
            {
                var servicesCollection = (Data)Session["UserAllDetails"];
                obj.PartyId = servicesCollection.userDetails.partyId;
                try
                {
                    #region for vendorType
                    ListRequest objreq = new ListRequest();
                    objreq.PartyID = obj.PartyId;
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/VendorType");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddHeader("LoginID", servicesCollection.userDetails.partyId);
                    request.AddParameter("application/json", _JsonSerializer.Serialize(objreq), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                        if (LoginObj.ResponseCode == "1")
                        {
                            ViewData["PartyType"] = new SelectList(LoginObj.Data, "ListID", "ListName");
                        }
                    }
                    else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                    {
                        servicesCollection.userDetails.partyId = "";
                        //CurrentSessions.Token = "";
                        TempData["Type"] = "warning";
                        TempData["Message"] = "Session Expired! Please login again.";
                        TempData["Flag"] = "-1";
                        return RedirectToAction("logout", "Home");
                    }
                    #endregion

                    #region VendorDetails
                    client = new RestClient(ConfigurationManager.AppSettings["URL"] + "/SwitchParty/GetVendorDetails");
                    request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                    //request.AddHeader("Content-Type", "application/json;");
                    //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                    request.AddHeader("LoginID", servicesCollection.userDetails.partyId);
                    //_JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                    request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
                    response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                        if (LoginObj.ResponseCode == "1")
                        {
                            if (obj.PartyType == "Retailer")
                            {
                                ViewData["RetailerList"] = LoginObj.vendetList;
                            }
                            else if (obj.PartyType == "Distributor")
                            {
                                ViewData["DistributorList"] = LoginObj.vendetList;
                            }
                            else if (obj.PartyType == "SuperStockist")
                            {
                                ViewData["SuperStokistList"] = LoginObj.vendetList;
                            }
                        }
                    }
                    else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                    {
                        servicesCollection.userDetails.partyId = "";
                        //CurrentSessions.Token = "";
                        TempData["Type"] = "warning";
                        TempData["Message"] = "Session Expired! Please login again.";
                        TempData["Flag"] = "-1";
                        return RedirectToAction("logout", "Home");
                    }
                    #endregion                    

                    #region ASMList
                    client = new RestClient(ConfigurationManager.AppSettings["URL"] + "/SwitchVendor/GetASMList");
                    request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                    //request.AddHeader("Content-Type", "application/json;");
                    //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                    request.AddHeader("LoginID", servicesCollection.userDetails.partyId);
                    //_JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                    request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
                    response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                        if (LoginObj.ResponseCode == "1")
                        {
                            ViewData["ASMList"] = new SelectList(LoginObj.Data, "ListID", "ListName");
                        }
                        else
                        {
                            ViewData["ASMList"] = null;
                        }
                    }
                    else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                    {
                        servicesCollection.userDetails.partyId = "";
                        //CurrentSessions.Token = "";
                        TempData["Type"] = "warning";
                        TempData["Message"] = "Session Expired! Please login again.";
                        TempData["Flag"] = "-1";
                        return RedirectToAction("logout", "Home");
                    }
                    #endregion
                }
                catch (Exception ex)
                {
                    TempData["Type"] = "error";
                    TempData["Message"] = "Details Not Found-Ex1! Please try after sometime.";
                }
                return View("Index");
            }
            else
            {
                return RedirectToAction("Index", "SwitchClient");
            }
        }
        #endregion

        public JsonResult GetVenderTypeList()
        {
            #region
            var UserDetails = (UserModelSession)Session["UserDetails"];
            ListRequest obj = new ListRequest();
            //obj.PartyID = servicesCollection.userDetails.partyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "SwitchParty/VendorType");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddHeader("LoginID", UserDetails.PartyId);
            request.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                SwitchVendorResponse LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponse>(response.Content);
                if (LoginObj.ResponseCode == "1")
                {
                    //ViewData["PartyType"] = new SelectList(LoginObj.Data, "ListID", "ListName");
                    if (LoginObj.ResponseCode == "1")
                    {
                        return new JsonResult
                        {
                            Data = new { Lst = LoginObj.Data },
                            ContentEncoding = System.Text.Encoding.UTF8,
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                }

            }
            else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
            {
                UserDetails.PartyId = "";
                //CurrentSessions.Token = "";
                TempData["Type"] = "warning";
                TempData["Message"] = "Session Expired! Please login again.";
                TempData["Flag"] = "-1";
                //return RedirectToAction("login-alt", "authentication");
            }
            else
            {
                TempData["Type"] = "error";
                TempData["Message"] = "Details Not Found-Ex! Please try after sometime.";
            }

            #endregion
            return new JsonResult
            {
                Data = new { Details = "" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}