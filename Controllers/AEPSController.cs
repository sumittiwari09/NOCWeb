using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;

namespace NewZapures_V2.Controllers
{
    public class AEPSController : Controller
    {
        NewZapures_V2.Models.AEPSTransaction.AEPSRequest _aepsRequest = new NewZapures_V2.Models.AEPSTransaction.AEPSRequest();
        NewZapures_V2.Models.AEPS.AEPSCommon _AEPSTransactionCommon = new Models.AEPS.AEPSCommon();
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        //AEPSTransferBL _AEPSTransferBL = new AEPSTransferBL();

        // GET: AEPSTransation
        public ActionResult Index()
        {
            var TransType = new List<NewZapures_V2.Models.AEPSTransaction.DropDownBO> { };
            TransType = GetTranTypeList();
            if (TempData["CASH_WITHDRAWAL"] != null && TempData["CASH_WITHDRAWAL"].ToString() != "")
            {
                TransType.RemoveAll(r => (r.ListValue == "310000"));

            }
            else if (TempData["BALANCE_ENQUIRY"] != null && TempData["BALANCE_ENQUIRY"].ToString() != "")
            {
                TransType.RemoveAll(r => (r.ListValue == "010000"));
            }
            _aepsRequest.TranTypeList = TransType;
            _aepsRequest.BankDetailsList = getbankName();
            return View(_aepsRequest);
        }
        public List<NewZapures_V2.Models.AEPSTransaction.BankiINNo> getbankName()
        {
            List<NewZapures_V2.Models.AEPSTransaction.BankiINNo> _BankiINNoList = new List<NewZapures_V2.Models.AEPSTransaction.BankiINNo>();
            JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
            string URL = ConfigurationManager.AppSettings["URL"].ToString();
            // StateBO objNoNeed = new StateBO();          
            try
            {
                var servicesCollection = (UserModelSession)Session["UserDetails"];
                var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "AEPS/GetAEPSBank");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("LoginID", servicesCollection.PartyId);
                //request.AddHeader("Authorization", "basic " + CurrentSessions.Token + "");
                //request.AddHeader("Content-Type", "application/json;");
                //request.AddHeader("Website", ConfigurationManager.AppSettings["website"].ToString());                

                //request.AddParameter("application/json", _JsonSerializer.Serialize(model), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    NewZapures_V2.Models.AEPSTransaction.BankiINNoAEPS _aepsbankList = _JsonSerializer.Deserialize<NewZapures_V2.Models.AEPSTransaction.BankiINNoAEPS>(response.Content);
                    foreach (var b in _aepsbankList.data)
                    {
                        var _BankiINNo = new NewZapures_V2.Models.AEPSTransaction.BankiINNo();
                        _BankiINNo.bankName = b.bankName;
                        if (_BankiINNoList.Count(x => x.bankName.ToLower() == _BankiINNo.bankName.ToLower()) == 0 && _BankiINNo.bankName != "Last Used Bank" && b.activeFlag == 1)
                        {
                            _BankiINNo.bankName = b.bankName;
                            _BankiINNo.iINNo = b.iINNo;
                            _BankiINNoList.Add(_BankiINNo);
                        }

                    }
                    return _BankiINNoList.OrderBy(s => s.bankName).ToList();

                }
                else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                {
                    //CurrentSessions.AtishayVendorID = "";
                    //CurrentSessions.Token = "";
                    return null;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        #region BindTransactionType
        public List<NewZapures_V2.Models.AEPSTransaction.DropDownBO> GetTranTypeList()
        {
            var TransType = new List<NewZapures_V2.Models.AEPSTransaction.DropDownBO> { };
            TransType = new List<NewZapures_V2.Models.AEPSTransaction.DropDownBO>
                    {
                        new NewZapures_V2.Models.AEPSTransaction.DropDownBO{ ListKeyStr="CASH WITHDRAWAL", ListValue = "010000" },
                        new NewZapures_V2.Models.AEPSTransaction.DropDownBO{ ListKeyStr="BALANCE ENQUIRY", ListValue = "310000" },
                    };
            return TransType;
        }
        #endregion

        #region FPRDService

        public ActionResult FPRDService()
        {
            //if (CurrentSessions.DMTServiceRunning == "1")
            //{
            //CurrentSessions.MoneyTransfer = 1;
            //TempData["Flag"] = "-1";
            //TempData["Type"] = "info";
            //TempData["Message"] = "Please wait for pervious AEPS request to complete ! Please Try after 5 minute.";
            //return RedirectToAction("Index", "AEPS");
            //}
            return RedirectToAction("Index", "AEPS");

        }
        [HttpPost]
        public ActionResult FPRDService(NewZapures_V2.Models.AEPSTransaction.AEPSRequest reqAEPS)
        {
            string DeviceProcess = string.Empty;
            Process[] localAll = Process.GetProcesses();
            if (reqAEPS.ScannerDevice == "Secugen")
            {
                DeviceProcess = "sgibiosrv";
            }
            else if (reqAEPS.ScannerDevice == "Mantra")
            {
                DeviceProcess = "MantraAVDMHost";
            }
            else if (reqAEPS.ScannerDevice == "Tatvik")
            {
                DeviceProcess = "TMF20RDService";
            }
            else if (reqAEPS.ScannerDevice == "Morpho")
            {
                DeviceProcess = "MorphoRdServiceL0Soft";
            }



            //Process[] pname = Process.GetProcessesByName(DeviceProcess);
            //if (pname.Length == 0)
            //{
            //    CurrentSessions.MoneyTransfer = 1;
            //    TempData["Flag"] = "-1";
            //    TempData["Type"] = "info";
            //    TempData["Message"] = "Please connect " + reqAEPS.ScannerDevice + " on your system.";
            //    return RedirectToAction("Transfer", "AEPSTransfer");
            //}
            NewZapures_V2.Models.AEPSTransaction.AEPSRequest _InnrerReqAEPS = new NewZapures_V2.Models.AEPSTransaction.AEPSRequest();
            NewZapures_V2.Models.AEPSTransaction.AEPSAPIResponse _AEPSAPIResponse = new NewZapures_V2.Models.AEPSTransaction.AEPSAPIResponse();
            HttpResponseMessage resAEPS = new HttpResponseMessage();
            try
            {
                if (reqAEPS.tranType == "010000" && (reqAEPS.Amount < 200 || reqAEPS.Amount > 10000))
                {
                    TempData["Flag"] = "-1";
                    TempData["Type"] = "info";
                    TempData["Message"] = "Amount Should be less than or equal 10000 Rs or greater  than 200 Rs. Please try Again!";
                    _InnrerReqAEPS.TranTypeList = GetTranTypeList();
                    _InnrerReqAEPS.BankDetailsList = getbankName();
                    return View("Transfer", _InnrerReqAEPS);
                }
                if (reqAEPS.tranType == "010000" && (reqAEPS.Amount >= 200 || reqAEPS.Amount <= 10000) || reqAEPS.tranType == "310000")
                {
                    if (reqAEPS.hdndata != null && reqAEPS.hdndata != "")
                    {
                        reqAEPS.AadhaarNo = _AEPSTransactionCommon.DecryptRaw(reqAEPS.EncryAadhaarNo);
                        reqAEPS.AadhaarNo = reqAEPS.AadhaarNo.Replace("-", "");
                        reqAEPS.AadhaarNo = reqAEPS.AadhaarNo.Replace("_", "");

                        if (_AEPSTransactionCommon.validateVerhoeff(reqAEPS.AadhaarNo))
                        {
                            //CurrentSessions.DMTServiceRunning = "1";

                            //FETCH DEVICE RECORDS                            
                            string thumbCaptureResponse = _AEPSTransactionCommon.ConvertFromBase64(_AEPSTransactionCommon.DecryptRaw(reqAEPS.hdndata));
                            if (thumbCaptureResponse != null && thumbCaptureResponse != "")
                            {
                                //string licenseKey = ConfigurationManager.AppSettings["AEPS_licenseKey"].ToString();
                                #region  XML CREATION
                                XmlDocument xmlResPonse = new XmlDocument();
                                xmlResPonse.LoadXml(thumbCaptureResponse);
                                if (xmlResPonse != null)
                                {
                                    XmlNode nodeDeviceInfo = xmlResPonse.SelectSingleNode("PidData/DeviceInfo");
                                    XmlNode rdsInfo = xmlResPonse.SelectSingleNode("PidData/Resp");
                                    XmlNode nodeSkey = xmlResPonse.SelectSingleNode("PidData/Skey");
                                    XmlNode nodeHmac = xmlResPonse.SelectSingleNode("PidData/Hmac");
                                    XmlNode nodeData = xmlResPonse.SelectSingleNode("PidData/Data");
                                    #endregion
                                    NewZapures_V2.Models.AEPSTransaction.AEPSRequestAPI _AEPSRequestAPI = new NewZapures_V2.Models.AEPSTransaction.AEPSRequestAPI();
                                    #region GET DEVICE CAPTURE RESPONSE
                                    NewZapures_V2.Models.AEPSTransaction.CaptureResponse _captureResponse = new NewZapures_V2.Models.AEPSTransaction.CaptureResponse();
                                    _captureResponse.PidDatatype = nodeData.Attributes["type"].Value;
                                    _captureResponse.Piddata = nodeData.InnerText;
                                    _captureResponse.ci = nodeSkey.Attributes["ci"].Value;
                                    _captureResponse.dc = nodeDeviceInfo.Attributes["dc"].Value;
                                    _captureResponse.dpID = nodeDeviceInfo.Attributes["dpId"].Value;
                                    _captureResponse.errCode = rdsInfo.Attributes["errCode"].Value;
                                    if (reqAEPS.ScannerDevice == "Morpho")
                                    {
                                        _captureResponse.errInfo = "Success";
                                    }
                                    else
                                    {
                                        _captureResponse.errInfo = rdsInfo.Attributes["errInfo"].Value;
                                    }
                                    _captureResponse.fCount = rdsInfo.Attributes["fCount"].Value;
                                    if (reqAEPS.ScannerDevice == "Secugen")
                                    {
                                        _captureResponse.fType = "0";
                                    }
                                    else
                                    {
                                        _captureResponse.fType = rdsInfo.Attributes["fType"].Value;
                                    }

                                    _captureResponse.hmac = nodeHmac.InnerText;
                                    //not Find in rdsInfo
                                    _captureResponse.iCount = "1";
                                    _captureResponse.mc = nodeDeviceInfo.Attributes["mc"].Value;
                                    _captureResponse.mi = nodeDeviceInfo.Attributes["mi"].Value;
                                    _captureResponse.nmPoints = rdsInfo.Attributes["nmPoints"].Value;
                                    //not find in rdsInfo
                                    if (reqAEPS.ScannerDevice == "Tatvik")
                                    {
                                        _captureResponse.pCount = "0";
                                        _captureResponse.pType = "0";
                                    }
                                    else
                                    {
                                        _captureResponse.pCount = "1";
                                        _captureResponse.pType = "1";
                                    }
                                    _captureResponse.qScore = rdsInfo.Attributes["qScore"].Value;
                                    _captureResponse.rdsID = nodeDeviceInfo.Attributes["rdsId"].Value;
                                    _captureResponse.rdsVer = nodeDeviceInfo.Attributes["rdsVer"].Value;
                                    _captureResponse.sessionKey = nodeSkey.InnerText;
                                    #endregion
                                    _AEPSRequestAPI.captureResponse = _captureResponse;
                                    #region  GET CARDNUMBERORUID
                                    NewZapures_V2.Models.AEPSTransaction.CardnumberORUID _cardnumberORUID = new NewZapures_V2.Models.AEPSTransaction.CardnumberORUID();
                                    _cardnumberORUID.adhaarNumber = reqAEPS.AadhaarNo;
                                    _cardnumberORUID.indicatorforUID = 0;
                                    _cardnumberORUID.nationalBankIdentificationNumber = reqAEPS.BankIINno;
                                    #endregion
                                    _AEPSRequestAPI.cardnumberORUID = _cardnumberORUID;
                                    #region Outer Variable
                                    _AEPSRequestAPI.AadhaarNo = reqAEPS.AadhaarNo;
                                    _AEPSRequestAPI.latitude = reqAEPS.latitude;
                                    _AEPSRequestAPI.longitude = reqAEPS.longitude;
                                    _AEPSRequestAPI.mobileNumber = reqAEPS.mobileNumber;
                                    _AEPSRequestAPI.DeviceIMEI = _AEPSTransactionCommon.GetMacAddress();
                                    if (reqAEPS.tranType == "010000")
                                    {
                                        _AEPSRequestAPI.transactionType = "CW";
                                        _AEPSRequestAPI.transactionAmount = reqAEPS.Amount;
                                    }
                                    else
                                    {
                                        _AEPSRequestAPI.transactionType = "BE";
                                    }
                                    _AEPSRequestAPI.Platform = "Web";
                                    if (_AEPSRequestAPI.latitude == null || _AEPSRequestAPI.longitude == null)
                                    {
                                        _AEPSRequestAPI.latitude = "26.8609893760128";
                                        _AEPSRequestAPI.longitude = "75.7955641903686";
                                    }
                                    #endregion
                                    var servicesCollection = (UserModelSession)Session["UserDetails"];
                                    var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "AEPS/AEPSTransfer");
                                    var request = new RestRequest(Method.POST);
                                    request.AddHeader("cache-control", "no-cache");
                                    request.AddHeader("LoginID", servicesCollection.PartyId);
                                    request.AddParameter("application/json", _JsonSerializer.Serialize(_AEPSRequestAPI), ParameterType.RequestBody);
                                    IRestResponse response = client.Execute(request);
                                    if (response.StatusCode.ToString() == "OK")
                                    {
                                        _AEPSAPIResponse = _JsonSerializer.Deserialize<NewZapures_V2.Models.AEPSTransaction.AEPSAPIResponse>(response.Content);
                                        //SUCCESS 010,FAIELD 011 ,009 Refundable,002 Pending,099 : Exception	
                                        if (_AEPSAPIResponse.APIResponseCode == "010" || _AEPSAPIResponse.APIResponseCode == "011" || _AEPSAPIResponse.APIResponseCode == "002")
                                        {
                                            //CurrentSessions.DMTServiceRunning = "0";
                                            //CurrentSessions.MoneyTransfer = 1;

                                            TempData["SwalStatusMsg"] = "error";
                                            TempData["SwalTitleMsg"] = "error!";
                                            if (_AEPSAPIResponse.APIResponseMessage == "Invalid encryption of Pid" || _AEPSAPIResponse.APIResponseMessage == "Invalid Encryption")
                                            {
                                                TempData["SwalMessage"] = "Fingerprint capture Failed. Please try again with proper Fingerprint";
                                            }
                                            else
                                            {
                                                TempData["SwalMessage"] = _AEPSAPIResponse.APIResponseMessage;
                                            }
                                            if (_AEPSAPIResponse.details != null)
                                            {
                                                if (_AEPSAPIResponse.details.transactionType == "AP" || _AEPSAPIResponse.details.transactionType == "CW" && (_AEPSAPIResponse.APIResponseCode == "010" || _AEPSAPIResponse.APIResponseCode == "002"))
                                                {
                                                    TempData["SwalStatusMsg"] = "success";
                                                    TempData["SwalTitleMsg"] = "success!";
                                                    //TempData["Type"] = "success";
                                                    TempData["SwalMessage"] = TempData["Message"] + " with  Your Transaction Bank  RRN   is " + _AEPSAPIResponse.details.bankRRN + " and Transaction amount is " + _AEPSAPIResponse.details.transactionAmount + " Status " + _AEPSAPIResponse.details.transactionStatus + " Your Current bankBalance  is " + _AEPSAPIResponse.details.balanceAmount;
                                                }
                                                else if (_AEPSAPIResponse.details.transactionType == "AP" || _AEPSAPIResponse.details.transactionType == "CW" && _AEPSAPIResponse.APIResponseCode == "011")
                                                {
                                                    TempData["SwalStatusMsg"] = "error";
                                                    TempData["SwalTitleMsg"] = "error!";
                                                    //TempData["Type"] = "error";
                                                    TempData["SwalMessage"] = TempData["Message"] + " with  Your Transaction Bank  RRN   is " + _AEPSAPIResponse.details.bankRRN + " and Transaction amount is " + _AEPSAPIResponse.details.transactionAmount + " Status " + _AEPSAPIResponse.details.transactionStatus;
                                                }
                                                else if (_AEPSAPIResponse.details.transactionType == "BE" && _AEPSAPIResponse.APIResponseCode == "010")
                                                {
                                                    TempData["SwalStatusMsg"] = "success";
                                                    TempData["SwalTitleMsg"] = "success!";
                                                    //TempData["Type"] = "success";
                                                    TempData["SwalMessage"] = TempData["Message"] + " Your Current bank  Balance  is " + _AEPSAPIResponse.details.balanceAmount;
                                                }
                                            }
                                            return RedirectToAction("Index", _InnrerReqAEPS);
                                        }
                                        else
                                        {
                                            //CurrentSessions.DMTServiceRunning = "0";
                                            //CurrentSessions.MoneyTransfer = 3;
                                            _InnrerReqAEPS.TranTypeList = GetTranTypeList();
                                            _InnrerReqAEPS.BankDetailsList = getbankName();
                                            //TempData["Flag"] = "-1";
                                            //TempData["Type"] = "error";
                                            TempData["SwalStatusMsg"] = "error";
                                            TempData["SwalTitleMsg"] = "error!";
                                            if (_AEPSAPIResponse.APIResponseMessage == "Invalid encryption of Pid" || _AEPSAPIResponse.APIResponseMessage == "Invalid Encryption")
                                            {
                                                TempData["SwalMessage"] = "Fingerprint capture Failed. Please try again with proper Fingerprint";
                                            }
                                            else
                                            {
                                                TempData["SwalMessage"] = _AEPSAPIResponse.APIResponseMessage;
                                            }
                                            return RedirectToAction("Home", _InnrerReqAEPS);

                                        }

                                    }
                                    else if (response.StatusCode.ToString() == "Unauthorized" || response.StatusCode.ToString() == "NotFound")
                                    {
                                        //CurrentSessions.AtishayVendorID = "";
                                        //CurrentSessions.Token = "";
                                        //CurrentSessions.MoneyTransfer = 1;
                                        //TempData["Type"] = "warning";
                                        //TempData["Message"] = "Session Expired.Please login again.";
                                        //TempData["Flag"] = "-1";
                                        TempData["SwalStatusMsg"] = "error";
                                        TempData["SwalTitleMsg"] = "error!";
                                        TempData["SwalMessage"] = "Session Expired.Please login again.";
                                        return RedirectToAction("login-alt", "authentication");
                                    }
                                    else
                                    {
                                        //CurrentSessions.MoneyTransfer = 1;
                                        //TempData["Flag"] = "-1";
                                        //TempData["Type"] = "error";
                                        //TempData["Message"] = "Something went wrong!Please Check in History";
                                        TempData["SwalStatusMsg"] = "error";
                                        TempData["SwalTitleMsg"] = "error!";
                                        TempData["SwalMessage"] = "Something went wrong!Please Check in History";
                                        return RedirectToAction("Home", _InnrerReqAEPS);
                                    }
                                }
                                else
                                {
                                    //CurrentSessions.DMTServiceRunning = "0";
                                    //CurrentSessions.MoneyTransfer = 1;
                                    _InnrerReqAEPS.AadhaarNo = "";
                                    _InnrerReqAEPS.Amount = 0;
                                    _InnrerReqAEPS.hdndata = "";
                                    TempData["SwalStatusMsg"] = "error";
                                    TempData["SwalTitleMsg"] = "error!";
                                    TempData["SwalMessage"] = "Capture Data element not in proper format please refresh page and try Again!";

                                    return RedirectToAction("Home", _InnrerReqAEPS);
                                }
                            }
                            else
                            {
                                //CurrentSessions.DMTServiceRunning = "0";
                                //CurrentSessions.MoneyTransfer = 1;
                                _InnrerReqAEPS.AadhaarNo = "";
                                _InnrerReqAEPS.Amount = 0;
                                _InnrerReqAEPS.hdndata = "";
                                TempData["SwalStatusMsg"] = "info";
                                TempData["SwalTitleMsg"] = "info!";
                                TempData["SwalMessage"] = "Capture Data element not in proper format please refresh page and try Again!";


                                return RedirectToAction("Home", _InnrerReqAEPS);
                            }

                        }
                        else
                        {
                            //CurrentSessions.MoneyTransfer = 1;
                            //CurrentSessions.DMTServiceRunning = "0";
                            _InnrerReqAEPS.TranTypeList = GetTranTypeList();
                            _InnrerReqAEPS.BankDetailsList = getbankName();
                            _InnrerReqAEPS.AadhaarNo = "";
                            _InnrerReqAEPS.Amount = 0;
                            TempData["SwalStatusMsg"] = "info";
                            TempData["SwalTitleMsg"] = "info!";
                            TempData["SwalMessage"] = "Invalid Aadhaar Number. Please try Again with valid Aadhaar number!";
                            return RedirectToAction("Home", _InnrerReqAEPS);
                        }
                    }
                    else
                    {
                        //CurrentSessions.DMTServiceRunning = "0";
                        //CurrentSessions.MoneyTransfer = 1;
                        _InnrerReqAEPS.AadhaarNo = "";
                        _InnrerReqAEPS.Amount = 0;
                        _InnrerReqAEPS.hdndata = "";
                        TempData["SwalStatusMsg"] = "info";
                        TempData["SwalTitleMsg"] = "info!";
                        TempData["SwalMessage"] = "Capture Data element not in proper format please refresh page and try Again!";

                        return RedirectToAction("Home", _InnrerReqAEPS);
                    }
                }
                else
                {
                    //CurrentSessions.DMTServiceRunning = "0";
                    //CurrentSessions.MoneyTransfer = 3;
                    TempData["SwalStatusMsg"] = "info";
                    TempData["SwalTitleMsg"] = "info!";
                    TempData["SwalMessage"] = "Amount Should be less than or equal 10000 Rs or greater than 200 Rs. Please try Again!";

                    _InnrerReqAEPS.TranTypeList = GetTranTypeList();
                    _InnrerReqAEPS.BankDetailsList = getbankName();
                    return RedirectToAction("Home", _InnrerReqAEPS);
                }

            }
            catch (Exception ex)
            {
                //CurrentSessions.MoneyTransfer = 1;
                //CurrentSessions.DMTServiceRunning = "0";
                _InnrerReqAEPS.AadhaarNo = "";
                _InnrerReqAEPS.Amount = 0;
                _InnrerReqAEPS.hdndata = "";
                TempData["SwalStatusMsg"] = "error";
                TempData["SwalTitleMsg"] = "error!";
                TempData["SwalMessage"] = "Data element not in proper format please refresh page and try Again!";

                return RedirectToAction("Home", _InnrerReqAEPS);
            }
        }
        #endregion

        public ActionResult Home()
        {
            var TransType = new List<NewZapures_V2.Models.AEPSTransaction.DropDownBO> { };
            TransType = GetTranTypeList();
            if (TempData["CASH_WITHDRAWAL"] != null && TempData["CASH_WITHDRAWAL"].ToString() != "")
            {
                TransType.RemoveAll(r => (r.ListValue == "310000"));

            }
            else if (TempData["BALANCE_ENQUIRY"] != null && TempData["BALANCE_ENQUIRY"].ToString() != "")
            {
                TransType.RemoveAll(r => (r.ListValue == "010000"));
            }
            _aepsRequest.TranTypeList = TransType;
            _aepsRequest.BankDetailsList = getbankName();
            return View(_aepsRequest);
        }
    }
}