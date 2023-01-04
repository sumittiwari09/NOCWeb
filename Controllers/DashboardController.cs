using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Configuration;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Metrica.Controllers
{
    public class DashboardController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        public ActionResult Index()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        public ActionResult WelcomeNoc()
        {
            return View();
        }
        public ActionResult WelcomeNocNew()
        {
            return View();
        }

        [ActionName("crypto-index")]
        public ActionResult CryptoIndex() => View();


        [ActionName("crm-index")]
        public ActionResult CrmIndex() => View();

        [ActionName("project-index")]
        public ActionResult ProjectIndex() => View();


        [ActionName("ecommerce-index")]
        public ActionResult EcommerceIndex() => View();


        [ActionName("helpdesk-index")]
        public ActionResult HelpdeskIndex() => View();

        [ActionName("hospital-index")]
        public ActionResult hospitalIndex() => View();

        public JsonResult TrustVerification(TrusteeBO.TrusteeInfo modal)
        {
            TrustRoot _trustapi = new TrustRoot();
            //modal.RegistrationNo = "COOP/2019/ALWAR/100658";
            #region Trust API
            var client = new RestClient("https://rajsahakarapp.rajasthan.gov.in/api/EntireSocietyDetail/GetSocietyDetailsByRegistrationNO?Reg_no=" + modal.RegistrationNo);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _trustapi = _JsonSerializer.Deserialize<TrustRoot>(response.Content);
                if (_trustapi.Status == "200" && _trustapi.Message == "Success")
                {
                    SessionModel.TrustRegNo = modal.RegistrationNo;
                    //ErrorBO _ress = Verificationdata(_trustapi);
                    return new JsonResult
                    {
                        Data = new { Success = true, Message = "Trust Information Get Successfully!!", res = _trustapi.Data },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    return new JsonResult
                    {
                        Data = new { Success = false, Message = "Enter Correct Registration Number", res = _trustapi },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            }
            //Console.WriteLine(response.Content);
            #endregion





            return new JsonResult
            {
                Data = new { Success = false, Message = "Error" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ErrorBO Verificationdata(TrustRoot modal)
        {
            ErrorBO _res = new ErrorBO();
            #region VerifyDetails
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Trustee/TrustVerification");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue;
            request.AddParameter("application/json", _JsonSerializer.Serialize(modal), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                _res = _JsonSerializer.Deserialize<ErrorBO>(response.Content);
                if (_res.ResponseCode == "1")
                {
                    //TempData["SwalStatusMsg"] = "success";
                    //TempData["SwalMessage"] = "Data saved sussessfully!";
                    //TempData["SwalTitleMsg"] = "Success...!";
                    SessionModel.TrustId = _res.Id;
                    //return new JsonResult
                    //{
                    //    Data = new { Success = true, Message = objResponseData.Messsage },
                    //    ContentEncoding = System.Text.Encoding.UTF8,
                    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    //};
                }
                else
                {
                    //TempData["SwalStatusMsg"] = "error";
                    //TempData["SwalMessage"] = "Something wrong";
                    //TempData["SwalTitleMsg"] = "error!";
                    //return new JsonResult
                    //{
                    //    Data = new { Success = false, Message = objResponseData.Messsage },
                    //    ContentEncoding = System.Text.Encoding.UTF8,
                    //    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    //};
                }
            }
            #endregion
            return _res;
        }

    }
}