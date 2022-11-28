using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace NewZapures_V2.Controllers
{
    public class ClientController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        
        #region Wallet Recharge: added by Shipra Garg
        [HttpGet]
        public ActionResult WalletRecharge()
        {
            try
            {
                var servicesCollection = (UserModelSession)Session["UserDetails"];
                if (servicesCollection != null)
                {

                    #region PayFrom
                    List<SelectListItem> _PayFromList = new List<SelectListItem>();
                    _PayFromList.Add(new SelectListItem { Text = "SBI", Value = "1" });
                    _PayFromList.Add(new SelectListItem { Text = "HDFC", Value = "2" });
                    _PayFromList.Add(new SelectListItem { Text = "BOB", Value = "3" });
                    ViewBag.PayFrom = new SelectList(_PayFromList, "Value", "Text");
                    #endregion

                    #region Company Bank Account Details
                    List<SelectListItem> _CompanyBankDetaillist = new List<SelectListItem>();
                    _CompanyBankDetaillist.Add(new SelectListItem { Text = "SBI Comany", Value = "11" });
                    _CompanyBankDetaillist.Add(new SelectListItem { Text = "HDFC Comany", Value = "22" });
                    _CompanyBankDetaillist.Add(new SelectListItem { Text = "BOB Comapy", Value = "33" });
                    ViewBag.CompanyBankDetail = new SelectList(_CompanyBankDetaillist, "Value", "Text");
                    #endregion

                    #region Self WRHistory
                    WalletRechargeHistory model = new WalletRechargeHistory();
                    model.Type = "MyPendingWalletRechargeHistory";
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/WalletRechargeHistory");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                    //request.AddHeader("Content-Type", "application/json;");
                    //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                    //request.AddHeader("LoginID", CurrentSessions.AtishayVendorID.ToString());
                    request.AddHeader("LoginID", servicesCollection.PartyId);
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        WalletRequestResponse LoginObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response.Content);
                        if (LoginObj.ResponseCode == "1")
                        {
                            ViewData["WalletRechargeHistory"] = LoginObj.WalletRechargeLst;
                        }
                    }
                    #endregion

                    //ViewData["BankName"] = new SelectList(null, "ListID", "ListName");
                    //ViewData["PayMode"] = new SelectList(null, "ListID", "ListName");
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }

        [HttpPost]
        public ActionResult WalletRecharge(ClientWalletRechage model, HttpPostedFileBase file)
        {
            try
            {
                #region PayFrom
                List<SelectListItem> _PayFromList = new List<SelectListItem>();
                _PayFromList.Add(new SelectListItem { Text = "SBI", Value = "1" });
                _PayFromList.Add(new SelectListItem { Text = "HDFC", Value = "2" });
                _PayFromList.Add(new SelectListItem { Text = "BOB", Value = "3" });
                ViewBag.PayFrom = new SelectList(_PayFromList, "Value", "Text");
                #endregion

                #region Company Bank Account Details
                List<SelectListItem> _CompanyBankDetaillist = new List<SelectListItem>();
                _CompanyBankDetaillist.Add(new SelectListItem { Text = "SBI Comany", Value = "11" });
                _CompanyBankDetaillist.Add(new SelectListItem { Text = "HDFC Comany", Value = "22" });
                _CompanyBankDetaillist.Add(new SelectListItem { Text = "BOB Comapy", Value = "33" });
                ViewBag.CompanyBankDetail = new SelectList(_CompanyBankDetaillist, "Value", "Text");
                #endregion
                var servicesCollection = (UserModelSession)Session["UserDetails"];
                if (servicesCollection != null)
                {
                    model.PartyId = servicesCollection.PartyId;
                    string fileName = "Zapurse_" + Guid.NewGuid().ToString() + file.FileName;
                    if (file != null && file.ContentLength > 0)
                    {
                        BinaryReader b = new BinaryReader(file.InputStream);
                        byte[] binData = b.ReadBytes(file.ContentLength);

                        string base64String = Convert.ToBase64String(binData, 0, binData.Length);
                        model.Receiptbase64 = base64String; //"data:image/png;base64," + base64String;

                        model.FileType = file.ContentType;


                        model.DocumentURL = "/images/WalletRecipts/" + fileName;
                    }

                    // save file in a folder


                    #region WalletRecharge
                    model.Platform = "Web";
                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/ClientWalletRequest");
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddHeader("LoginID", model.PartyId);
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    //request.AddParameter("application/json", "", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        WalletRequestResponse ReqObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response.Content);
                        if (ReqObj.ResponseCode == "1")
                        {
                            #region save Recipt
                            //string fileName = "Zapurse_" + Guid.NewGuid().ToString() + file.FileName;
                            string filePath = "~/images/WalletRecipts/" + fileName;
                            file.SaveAs(Server.MapPath(filePath));
                            #endregion
                            TempData["SwalStatusMsg"] = "success";
                            TempData["SwalMessage"] = "Request Sent Successfully!";
                            TempData["SwalFlag"] = "1";
                            TempData["SwalTitleMsg"] = "Success";
                        }
                        else
                        {
                            TempData["SwalStatusMsg"] = "error";
                            TempData["SwalMessage"] = "Some technical issue!Please Contact Support.";
                            TempData["SwalFlag"] = "-1";
                            TempData["SwalTitleMsg"] = "error";
                        }
                    }
                    else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                    {
                        servicesCollection.PartyId = "";
                        //servicesCollection.userDetails.Token = "";
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = "Session Expired! Please login again.";
                        TempData["Flag"] = "-1";
                        TempData["SwalTitleMsg"] = "warning";
                        return RedirectToAction("logout", "Home");
                    }
                    else
                    {
                        TempData["SwalMessage"] = "Some technical issue!Please Contact Support.";
                        TempData["SwalFlag"] = "-1";
                    }
                    #endregion                  
                    #region Self WRHistory
                    WalletRechargeHistory model1 = new WalletRechargeHistory();
                    model1.Type = "MyPendingWalletRechargeHistory";

                    client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/WalletRechargeHistory");
                    request = new RestRequest(Method.POST);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddHeader("LoginID", model.PartyId);
                    request.AddParameter("application/json", _JsonSerializer.Serialize(model1), ParameterType.RequestBody);
                    response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        WalletRequestResponse LoginObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response.Content);
                        if (LoginObj.ResponseCode == "1")
                        {
                            ViewData["WalletRechargeHistory"] = LoginObj.WalletRechargeLst;
                        }
                    }
                    #endregion
                    //#region BankDet
                    //model.Type = "CompanyBankDet";
                    //client = new RestClient(URL + "/ClientWalletRecharge.svc/GetCompanyBankDet");
                    //request = new RestRequest(Method.POST);
                    //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                    //request.AddHeader("Content-Type", "application/json;");
                    //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                    //request.AddHeader("LoginID", CurrentSessions.AtishayVendorID.ToString());
                    //_JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                    //request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    //response = client.Execute(request);
                    //if (response.StatusCode.ToString() == "OK")
                    //{
                    //    SwitchVendorResponseBO LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponseBO>(response.Content);
                    //    if (LoginObj.ResponseCode == "1")
                    //    {
                    //        ViewData["BankName"] = new SelectList(LoginObj.Data, "ListID", "ListName");
                    //    }
                    //    //TempData["Type"] = CommonFunctions.getResponseType(LoginObj.ResponseCode);

                    //}
                    //else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                    //{
                    //    CurrentSessions.AtishayVendorID = "";
                    //    CurrentSessions.Token = "";
                    //    TempData["Type"] = "warning";
                    //    TempData["Message"] = "Session Expired! Please login again.";
                    //    TempData["Flag"] = "-1";
                    //    return RedirectToAction("logout", "Home");
                    //}
                    //else
                    //{
                    //    TempData["Type"] = "error";
                    //    TempData["Message"] = "Details Not Found-Ex! Please try after sometime.";
                    //}
                    //#endregion
                    //#region Company Bank Details
                    //model.Type = "PaymentMode";
                    //client = new RestClient(URL + "/ClientWalletRecharge.svc/GetCompanyBankDet");
                    //request = new RestRequest(Method.POST);
                    //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                    //request.AddHeader("Content-Type", "application/json;");
                    //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());
                    //request.AddHeader("LoginID", CurrentSessions.AtishayVendorID.ToString());
                    //_JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
                    //request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                    //response = client.Execute(request);
                    //if (response.StatusCode.ToString() == "OK")
                    //{
                    //    SwitchVendorResponseBO LoginObj = _JsonSerializer.Deserialize<SwitchVendorResponseBO>(response.Content);
                    //    if (LoginObj.ResponseCode == "1")
                    //    {
                    //        ViewData["PayMode"] = new SelectList(LoginObj.Data, "ListID", "ListName");
                    //    }
                    //    //TempData["Type"] = CommonFunctions.getResponseType(LoginObj.ResponseCode);

                    //}
                    //else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                    //{
                    //    CurrentSessions.AtishayVendorID = "";
                    //    CurrentSessions.Token = "";
                    //    TempData["Type"] = "warning";
                    //    TempData["Message"] = "Session Expired! Please login again.";
                    //    TempData["Flag"] = "-1";
                    //    return RedirectToAction("logout", "Home");
                    //}
                    //else
                    //{
                    //    TempData["Type"] = "error";
                    //    TempData["Message"] = "Details Not Found-Ex! Please try after sometime.";
                    //}
                    //#endregion
                    return RedirectToAction("WalletRecharge", "Client");
                }
            }
            catch (Exception ex)
            {
                TempData["Type"] = "error";
                TempData["Message"] = "Details Not Found-Ex1! Please try after sometime.";
            }
            return View();
        }

        //public JsonResult UploadRecipt(string image)
        //{
        //    var image_array_1 = image.Split(';');
        //    var image_array_2 = image_array_1[1].Split(',');
        //    string img = image_array_2[1];
        //    string strm = img;
        //    //var myfilename = string.Format(@"{0}", Guid.NewGuid());
        //    string FilesName = "Zapurse_" + Guid.NewGuid().ToString() + ".jpeg";
        //    string fname = HttpContext.Server.MapPath("~/images/Docs/" + FilesName);
        //    //string filepath = "/Docs/Banners/abhishek" + myfilename + ".jpeg";
        //    var bytess = Convert.FromBase64String(strm);
        //    //File.WriteAllBytes(filepath, Convert.FromBase64String(strm));

        //    using (var imageFile = new FileStream(fname, FileMode.Create))
        //    {
        //        imageFile.Write(bytess, 0, bytess.Length);
        //        imageFile.Flush();
        //    }
        //    Documentlist obj = new Documentlist();
        //    obj.DocumentName = FilesName;
        //    ObjResponse = new ResponseData
        //    {
        //        statusCode = 1,
        //        imagename = FilesName
        //    };

        //    return Json(ObjResponse, JsonRequestBehavior.AllowGet);
        //    //return new JsonResult
        //    //{
        //    //    Data = new { Data = obj, failure = false, msg = "Success", isvalid = 1 },
        //    //    ContentType = FilesName,
        //    //    RecursionLimit=1,
        //    //    ContentEncoding = System.Text.Encoding.UTF8,
        //    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
        //    //};


        //}

        #endregion

        #region WR History :added By Shipra Garg
        [HttpGet]
        public ActionResult WalletRechargeHistory()
        {
            if (ViewData["ToDate"] == null)
            {
                ViewData["ToDate"] = DateTime.Now.ToString("dd-MM-yyyy");
                ViewData["FromDate"] = DateTime.Now.ToString("dd-MM-yyyy");
            }
            return View();
        }
        [HttpPost]
        public ActionResult WalletRechargeHistory(WalletRechargeHistory model)
        {
            ViewData["FromDate"] = model.FromDate;
            ViewData["ToDate"] = model.ToDate;
            #region VendorDetails
            //DataTable dt = new DataTable();

            model.Platform = "Web";
            model.Type = "MyWalletRechargeHistory";
            var servicesCollection = (UserModelSession)Session["UserDetails"];
            if (servicesCollection != null)
            {
                model.PartyId = servicesCollection.PartyId;
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Client/WalletRechargeHistory");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddHeader("LoginID", model.PartyId);
                request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                //request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    WalletRequestResponse LoginObj = _JsonSerializer.Deserialize<WalletRequestResponse>(response.Content);
                    if (LoginObj.ResponseCode == "1")
                    {
                        ViewData["WalletRechargeHistory"] = LoginObj.WalletRechargeLst;
                        //ListtoDataTable lsttodt = new ListtoDataTable();
                        //dt = lsttodt.ToDataTable(LoginObj.WalletRechargeLst);
                    }
                }
                else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                {
                    servicesCollection.PartyId = "";
                    //CurrentSessions.Token = "";
                    TempData["Type"] = "warning";
                    TempData["Message"] = "Session Expired! Please login again.";
                    TempData["Flag"] = "-1";
                    return RedirectToAction("logout", "Home");
                }
                #endregion                
            }
            return View();
        }
        #endregion

        #region Get Ledger Report
        [HttpGet]
        public ActionResult LedgerReport()
        {
            if (ViewData["ToDate"] == null)
            {
                ViewData["ToDate"] = DateTime.Now.ToString("dd-MM-yyyy");
                ViewData["FromDate"] = DateTime.Now.ToString("dd-MM-yyyy");
                ViewData["FilterVal"] = "";
            }
            return View();
        }
        [HttpPost]
        public ActionResult LedgerReport(LedgerReport model)
        {
            ViewData["ToDate"] = model.ToDate;
            ViewData["FromDate"] = model.FromDate;
            ViewData["FilterVal"] = model.FilterValue;
            try
            {
                #region VendorDetails
    
                var servicesCollection = (UserModelSession)Session["UserDetails"];
                DataTable dt = new DataTable();
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Reports/LedgerReport");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddHeader("LoginID", servicesCollection.PartyId);
                request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                //request.AddParameter("application/json", "", ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    LedgerReportDetails LoginObj = _JsonSerializer.Deserialize<LedgerReportDetails>(response.Content);
                    if (LoginObj.ResponseCode == "1")
                    {
                        ViewData["Translst"] = LoginObj.Data;
                        ListtoDataTable lsttodt = new ListtoDataTable();
                        dt = lsttodt.ToDataTable(LoginObj.Data);
                    }
                }
                else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                {
                    servicesCollection.PartyId = "";
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
            return View("LedgerReport");
        }
        public class ListtoDataTable
        {
            public DataTable ToDataTable<T>(List<T> items)
            {
                DataTable dataTable = new DataTable(typeof(T).Name);
                //Get all the properties by using reflection   
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names  
                    dataTable.Columns.Add(prop.Name);
                }
                foreach (T item in items)
                {
                    var values = new object[Props.Length];
                    for (int i = 0; i < Props.Length; i++)
                    {

                        values[i] = Props[i].GetValue(item, null);
                    }
                    dataTable.Rows.Add(values);
                }

                return dataTable;
            }
        }
        #endregion
    }
}