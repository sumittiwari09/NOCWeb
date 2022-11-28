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
    public class SupportController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        #region WalletRequestAccept/Reject:added by Shipra Garg
        [HttpGet]
        public ActionResult ClientWalletRechargeRequets()
        {
            ViewData["FromDate"] = DateTime.Now.ToString("dd-MM-yyyy");
            ViewData["ToDate"] = DateTime.Now.ToString("dd-MM-yyyy");

            var servicesCollection = (UserModelSession)Session["UserDetails"];
            //var servicesCollection = (Data)Session["UserAllDetails"];
            if (servicesCollection != null)
            {
                WalletRechargeHistory model = new WalletRechargeHistory();
                #region VendorDetails
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/GetClientDetails");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                //request.AddHeader("Content-Type", "application/json;");
                //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                request.AddHeader("LoginID", servicesCollection.PartyId);
                _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    WalletRequestResponse LoginObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response.Content);
                    if (LoginObj.ResponseCode == "1")
                    {
                        ViewData["ClientRechargeRequest"] = LoginObj.WalletRechargeLst;
                    }
                }
                else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                {
                    servicesCollection.PartyId = "";
                    //CurrentSessions.Token = "";
                    TempData["SwalStatusMsg"] = "warning";
                    TempData["SwalMessage"] = "Session Expired! Please login again.";
                    TempData["SwalFlag"] = "1";
                    TempData["SwalTitleMsg"] = "warning";
                    return RedirectToAction("login-alt", "authentication");
                }
                #endregion
                ViewData["FromDate"] = model.FromDate;
                ViewData["ToDate"] = model.ToDate;
            }
            return View();
        }
        [HttpPost]
        public ActionResult ClientWalletRechargeRequets(WalletRechargeHistory model)
        {
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            if (servicesCollection != null)
            {
                #region VendorDetails
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/GetClientDetails");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                //request.AddHeader("Content-Type", "application/json;");
                //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                request.AddHeader("LoginID", servicesCollection.PartyId);
                _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    WalletRequestResponse LoginObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response.Content);
                    if (LoginObj.ResponseCode == "1")
                    {
                        ViewData["ClientRechargeRequest"] = LoginObj.WalletRechargeLst;
                    }
                }
                else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                {
                    servicesCollection.PartyId = "";
                    //CurrentSessions.Token = "";
                    TempData["SwalStatusMsg"] = "warning";
                    TempData["SwalMessage"] = "Session Expired! Please login again.";
                    TempData["SwalFlag"] = "1";
                    TempData["SwalTitleMsg"] = "warning";
                    return RedirectToAction("login-alt", "authentication");
                }
                #endregion
                ViewData["FromDate"] = model.FromDate;
                ViewData["ToDate"] = model.ToDate;
            }
            return View();
        }

        public JsonResult AcceptRejectRequestWallet(ClientWalletRechage NType)
        {
            WalletRequestResponse ReqObj = new WalletRequestResponse();
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            if (servicesCollection != null)
            {
                #region AcceptRejectRequestWallet

                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/AcceptRejectRequest");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                //request.AddHeader("Content-Type", "application/json;");
                //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                request.AddHeader("LoginID", servicesCollection.PartyId);
                //_JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                request.AddParameter("application/json", _JsonSerializer.Serialize(NType), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ReqObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response.Content);

                }
                else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                {
                    servicesCollection.PartyId = "";
                    //CurrentSessions.Token = "";
                    TempData["Type"] = "warning";
                    TempData["Message"] = "Session Expired! Please login again.";
                    TempData["Flag"] = "-1";
                }
            }
            #endregion
            return new JsonResult
            {
                Data = new { Lst = ReqObj },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult DownloadReceipt(ClientWalletRechage model)
        {
            WalletRequestResponse ReqObj = new WalletRequestResponse();
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            if (servicesCollection != null)
            {
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/GetWalletRechargeReceipt");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request1.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                //request1.AddHeader("Content-Type", "application/json;");
                //request1.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                request.AddHeader("LoginID", servicesCollection.PartyId);
                //_JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                IRestResponse response1 = client.Execute(request);
                if (response1.StatusCode.ToString() == "OK")
                {
                    ReqObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response1.Content);
                    if (ReqObj.ResponseCode == "1")
                    {
                        string FilePath = Server.MapPath("~" + ReqObj.WalletRechargeLst.FirstOrDefault().DocumentURL.ToString());
                        byte[] byteData = System.IO.File.ReadAllBytes(FilePath);
                        string[] fname = ReqObj.WalletRechargeLst.FirstOrDefault().DocumentURL.ToString().Split('.');

                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = MimeMapping.GetMimeMapping("application/force-download");
                        Response.AddHeader("content-disposition", "attachment; filename=Zapures_" + DateTime.Now.ToString("ddMMyyyyHHmmss") + "." + fname[1]);
                        Response.BinaryWrite(byteData);
                        Response.Flush();

                        return View();
                    }
                    else
                    {
                        TempData["Type"] = "error";
                        TempData["Message"] = ReqObj.Messsage;
                        TempData["Flag"] = "-1";
                        return RedirectToAction("ClientWalletRechargeRequets", "Support", model);
                    }
                }
                else if (response1.StatusCode.ToString() == "Unauthorized" || response1.StatusCode.ToString() == "NotFound")
                {
                    servicesCollection.PartyId = "";
                    //CurrentSessions.Token = "";
                    TempData["Type"] = "warning";
                    TempData["Message"] = "Session Expired! Please login again.";
                    TempData["Flag"] = "-1";
                    return RedirectToAction("login-alt", "authentication");
                }
                else
                {
                    TempData["Type"] = "warning";
                    TempData["Message"] = "Details Not Found-Ex! Please try after sometime.";
                    TempData["Flag"] = "-1";
                    return RedirectToAction("ClientWalletRechargeRequets", "Support", model);
                }
            }
            return RedirectToAction("login-alt", "authentication");
        }
        #endregion
    }
}