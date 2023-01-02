using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NewZapures_V2.Controllers
{
    public class LoginSignupController : Controller
    {
        // GET: LoginSignup
        string AdharPanToken = ConfigurationManager.AppSettings["AdharTokenForVerification"];

        ResponseData objResponse;
        public ActionResult Index()
        {
            var serviceDetailsList = ZapurseCommonlist.GetServicesAllDetails();
            var userTypes = ZapurseCommonlist.GetUserTypes();

            ViewBag.serviceDetailsList = serviceDetailsList;
            ViewBag.userTypesList = userTypes;

            var content = ViewContent();
            if (content.Count!= null)
            {
                ViewBag.TandC = content[0];
                ViewBag.TandCId = content[0].Id;
            }

            return View();
        }
        public ActionResult ServicePurchase()
        {
            var serviceDetailsList = ZapurseCommonlist.GetServicesAllDetails();
            var userTypes = ZapurseCommonlist.GetUserTypes();

            ViewBag.serviceDetailsList = serviceDetailsList;
            ViewBag.userTypesList = userTypes;

            var content = ViewContent();
            if (content != null)
            {
                ViewBag.TandC = content[0]; ViewBag.TandCId = content[0].Id;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            try
            {
                UserModelSession userModel = new UserModelSession();

                var json = JsonConvert.SerializeObject(login);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/Login");
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
                    if (objResponse.Data != null)
                    {
                        userModel = JsonConvert.DeserializeObject<UserModelSession>(objResponse.Data.ToString());
                        List<UserPermissions> permissions = LoginController.GetPermissionDetails(userModel.RoleId, userModel.DepartmentId);
                        List<NotificationMaster> notificationsData = ZapurseCommonlist.GetNotificationMaster();

                        if (userModel.PartyId == "A000001")
                        {
                            //if (permissions != null)
                            //{
                            Session["Token"] = objResponse.JWT;
                            Session["UserDetails"] = userModel;
                            Session["UserPermissions"] = permissions;
                            Session["notificationList"] = notificationsData;

                            //}
                            //return RedirectToAction("WelcomeNoc", "Dashboard");
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else if (userModel.IsActive == "0")
                        {
                            TempData["IsUserDetailsExists"] = 1;
                            TempData["msg"] = "Your Account is blocked Please Contact to admin...";
                            return RedirectToAction("login-alt", "authentication");
                        }
                        else if (userModel.Type == "11")
                        {
                            //if (permissions != null)
                            //{
                            Session["Token"] = objResponse.JWT;
                            Session["UserDetails"] = userModel;
                            Session["UserPermissions"] = permissions;
                            Session["notificationList"] = notificationsData;

                            //}
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            if (permissions != null)
                            {
                                Session["Token"] = objResponse.JWT;
                                Session["UserDetails"] = userModel;
                                Session["UserPermissions"] = permissions;
                                Session["notificationList"] = notificationsData;

                                return RedirectToAction("WelcomeNoc", "Dashboard");
                            }
                        }
                    }

                    if (objResponse.Message.Contains("User Details Not Found..."))
                    {
                        TempData["IsUserDetailsExists"] = 1;
                        TempData["msg"] = "Invalid Credentials Please Try Again...";
                        return RedirectToAction("login-alt", "authentication");

                    }
                    else
                    {
                        TempData["IsUserDetailsExists"] = 0;
                        return RedirectToAction("Index", "Welcome");
                    }
                }
                else
                {
                    TempData["IsUserDetailsExists"] = 1;
                    return RedirectToAction("login-alt", "authentication");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //[HttpPost]
        //public ActionResult Login(Login login)
        //{
        //    try
        //    {
        //        UserModelSession userModel = new UserModelSession();

        //        var json = JsonConvert.SerializeObject(login);
        //        var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/Login");
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("cache-control", "no-cache");
        //        //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
        //        request.AddParameter("application/json", json, ParameterType.RequestBody);
        //        request.AddHeader("Content-Type", "application/json");
        //        request.AddHeader("Accept", "application/json");
        //        IRestResponse response = client.Execute(request);

        //        if (response.StatusCode.ToString() == "OK")
        //        {
        //            objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
        //            if (objResponse.Data != null)
        //            {
        //                userModel = JsonConvert.DeserializeObject<UserModelSession>(objResponse.Data.ToString());
        //                List<UserPermissions> permissions = AdminAjaxRequestPageController.GetPermissionDetails(userModel.RoleId, userModel.DepartmentId);
        //                List<NotificationMaster> notificationsData = ZapurseCommonlist.GetNotificationMaster();

        //                if (userModel.PartyId == "A000001")
        //                {
        //                    //if (permissions != null)
        //                    //{
        //                    Session["Token"] = objResponse.JWT;
        //                    Session["UserDetails"] = userModel;
        //                    Session["UserPermissions"] = permissions;
        //                    Session["notificationList"] = notificationsData;

        //                    //}
        //                    return RedirectToAction("Index", "Dashboard");
        //                }
        //                else if (userModel.IsActive == "0")
        //                {
        //                    TempData["IsUserDetailsExists"] = 1;
        //                    TempData["msg"] = "Your Account is blocked Please Contact to admin...";
        //                    return RedirectToAction("login-alt", "authentication");
        //                }
        //                else if (userModel.Type == "6")
        //                {
        //                    //if (permissions != null)
        //                    //{
        //                    Session["Token"] = objResponse.JWT;
        //                    Session["UserDetails"] = userModel;
        //                    Session["UserPermissions"] = permissions;
        //                    Session["notificationList"] = notificationsData;

        //                    //}
        //                    return RedirectToAction("Index", "Welcome");
        //                }
        //                else
        //                {
        //                    if (permissions != null)
        //                    {
        //                        Session["Token"] = objResponse.JWT;
        //                        Session["UserDetails"] = userModel;
        //                        Session["UserPermissions"] = permissions;
        //                        Session["notificationList"] = notificationsData;

        //                        return RedirectToAction("Index", "Welcome");
        //                    }
        //                }
        //            }

        //            if (objResponse.Message.Contains("User Details Not Found..."))
        //            {
        //                TempData["IsUserDetailsExists"] = 1;
        //                TempData["msg"] = "Invalid Credentials Please Try Again...";
        //                return RedirectToAction("login-alt", "authentication");

        //            }
        //            else
        //            {
        //                TempData["IsUserDetailsExists"] = 0;
        //                return RedirectToAction("Index", "Welcome");
        //            }
        //        }
        //        else
        //        {
        //            TempData["IsUserDetailsExists"] = 1;
        //            return RedirectToAction("login-alt", "authentication");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public JsonResult ValidateUser(ResetPassword reset)
        {
            var json = JsonConvert.SerializeObject(reset);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/Reset");
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
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult ResetPassword(ResetPassword reset)
        {
            var json = JsonConvert.SerializeObject(reset);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/Reset");
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
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetHardwareList(string serviceList)
        {
            var Token = Session["Token"];
            ServiceIDs iDs = new ServiceIDs();
            iDs.serviceList = serviceList;
            var json = JsonConvert.SerializeObject(iDs);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetHardwareLisst");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<HardwareList> hardwares = new List<HardwareList>();

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                hardwares = JsonConvert.DeserializeObject<List<HardwareList>>(objResponse.Data.ToString());
            }

            //if (response.StatusCode.ToString() == "Unauthorized")
            //{
            //    TempData["Unauthorized"] = 1;
            //}
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = hardwares, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetAllHardwares(string Type)
        {
            var Token = Session["Token"];
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCategoryAllInformation?Type=" + Type);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<GetservicesetailsAndroidNew> serviceDetails = new List<GetservicesetailsAndroidNew>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                serviceDetails = JsonConvert.DeserializeObject<List<GetservicesetailsAndroidNew>>(objResponse.Data.ToString());
            }


            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = serviceDetails, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult SaveDetails(PartyMaster party)
        {
            var json = JsonConvert.SerializeObject(party);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/Add");
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
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult VerifyAdharPAN(string id_number, string url, string type)
        {
            AdharPANVerification t = new AdharPANVerification();
            t.id_number = id_number;
            var json = JsonConvert.SerializeObject(t);
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AdharPanToken);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            PANRoot data = new PANRoot();
            if (response.StatusCode.ToString() == "OK")
            {
                data = JsonConvert.DeserializeObject<PANRoot>(response.Content);
            }
            if (response.StatusCode.ToString() == "422")
            {
                data = JsonConvert.DeserializeObject<PANRoot>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = data.status_code, Data = data.data, ClientID = data.data.client_id, OperationType = type, Failure = false, Message = data.message_code },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult VerifyAdharWithOTP(string otp, string url, string client_id, string type)
        {
            AdharVerificationWithOTP t = new AdharVerificationWithOTP();
            t.otp = otp;
            t.client_id = client_id;

            var json = JsonConvert.SerializeObject(t);
            var client = new RestClient(url);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("Authorization", "Bearer " + AdharPanToken);
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            AadharFullDetails data = new AadharFullDetails();
            if (response.StatusCode.ToString() == "OK")
            {
                data = JsonConvert.DeserializeObject<AadharFullDetails>(response.Content);
            }
            if (response.StatusCode.ToString() == "InternalServerError")
            {
                data.status_code = 500;
                data.message_code = "InternalServerError";
            }

            return new JsonResult
            {
                Data = new { StatusCode = data.status_code, Data = data.data, Failure = false, Message = data.message_code },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult MobileVerify(string mobile, string messageContent)
        {
            MobileVerification verification = new MobileVerification();
            verification.Mobile = mobile;
            verification.Message = messageContent;

            var json = JsonConvert.SerializeObject(verification);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/VerifyMobile");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            MobileOTPResponse data = new MobileOTPResponse();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                data = JsonConvert.DeserializeObject<MobileOTPResponse>(objResponse.Data.ToString());
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = data, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public List<ServicesDetails> GetServicesDetails()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetServicesDetails");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<ServicesDetails> serviceDetails = new List<ServicesDetails>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                serviceDetails = JsonConvert.DeserializeObject<List<ServicesDetails>>(objResponse.Data.ToString());
            }
            return serviceDetails;
        }
        
        public List<Services> GetServices()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetServices");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Services> service = new List<Services>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                service = JsonConvert.DeserializeObject<List<Services>>(objResponse.Data.ToString());
            }
            return service;
        }
        public ActionResult LoginSecurity()
        {

            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetdetailsForUser?email=" + userdetailsSession.EmailId);
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "bearer " + Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                Data d = new Data();
                if (response.StatusCode.ToString() == "OK")
                {
                    objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    d = JsonConvert.DeserializeObject<Data>(objResponse.Data.ToString());
                }
                if (response.StatusCode.ToString() == "Unauthorized")
                {
                    TempData["Unauthorized"] = 1;
                }

                ViewBag.AllData = d;
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        public JsonResult UpdateVerificationStatus(string partyId, string ColName, string colvalue, int status)
        {
            var Token = Session["Token"];
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UpdateVerification?PartyId=" + partyId + "&colName=" + ColName + "&colValue=" + colvalue + "&status=" + status);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult SaveAadhaarData(AadharWithPartyId aadhar)
        {
            if (aadhar.Aadhaar != null)
            {
                var json = JsonConvert.SerializeObject(aadhar);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SaveAadhaarData");
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
                }

                return new JsonResult
                {
                    Data = new { StatusCode = objResponse.statusCode, Data = "", Failure = false, Message = objResponse.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        public JsonResult SavePurchase(AddPurchase purchase)
        {
            var json = JsonConvert.SerializeObject(purchase);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SavePurchaseData");
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
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, OrderId = objResponse.UserID, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult SavePurchaseAfterLogin(AddPurchase purchase)
        {
            var userdata = (UserModelSession)Session["UserDetails"];

            if (userdata != null)
            {
                purchase.PartyId = userdata.PartyId;
                purchase.OrderId = userdata.PartialOrderID;

                var json = JsonConvert.SerializeObject(purchase);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/SavePurchaseData");
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
                }

                return new JsonResult
                {
                    Data = new { StatusCode = objResponse.statusCode, OrderId = objResponse.UserID, RegistrationNo = userdata.RegistrationNo, Data = objResponse, Failure = false, Message = objResponse.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { StatusCode = "-1", Data = "", Failure = false, Message = "Unable to fatch the data" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

        public List<ContentPage> ViewContent()
        {
            ContentPage content = new ContentPage();

            //content.Id = Id;
            content.Type = "Data";

            var json = JsonConvert.SerializeObject(content);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/EditContentData");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<ContentPage> mainData = new List<ContentPage>();

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                mainData = JsonConvert.DeserializeObject<List<ContentPage>>(objResponse.Data.ToString());
            }
            return mainData;
        }
        public JsonResult UploadPan(UploadDoc upload)
        {
            if (upload.Image != null)
            {
                var json = JsonConvert.SerializeObject(upload);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UploadPan");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                object status = 0;
                if (response.StatusCode.ToString() == "OK")
                {
                    status = JsonConvert.DeserializeObject(response.Content);
                }

                return new JsonResult
                {
                    Data = new { StatusCode = 000, Failure = false, Message = "Pan Uploaded Successfully" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { StatusCode = 0, Failure = false, Message = "Image Can`t Be Null" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        public JsonResult UploadAadhaar(UploadDoc upload)
        {
            if (upload.Image != null)
            {
                var json = JsonConvert.SerializeObject(upload);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UploadAadhar");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                object status = 0;
                if (response.StatusCode.ToString() == "OK")
                {
                    status = JsonConvert.DeserializeObject(response.Content);
                }

                return new JsonResult
                {
                    Data = new { StatusCode = 000, Failure = false, Message = "Aadhaar Uploaded Successfully" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { StatusCode = 0, Failure = false, Message = "Image Can`t Be Null" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }
        public JsonResult UploadAddressProof(UploadDoc upload)
        {
            if (upload.Image != null)
            {
                var json = JsonConvert.SerializeObject(upload);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UploadAddressProof");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);
                object status = 0;
                if (response.StatusCode.ToString() == "OK")
                {
                    status = JsonConvert.DeserializeObject(response.Content);
                }

                return new JsonResult
                {
                    Data = new { StatusCode = 000, Failure = false, Message = "AddressProof Uploaded Successfully" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            else
            {
                return new JsonResult
                {
                    Data = new { StatusCode = 0, Failure = false, Message = "Image Can`t Be Null" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
        }

    }
}