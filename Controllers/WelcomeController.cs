using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System.Configuration;
using System.Web.Mvc;

namespace NewZapures_V2.Controllers
{

    public class WelcomeController : Controller
    {
        ResponseData objResponse;
        public ActionResult Index()
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

                    Session["UserAllDetails"] = d;
                }
                if (d.userDetails.ParentService != null && d.serviceName.Count <= 0&& userdetailsSession.Type!="5")
                {
                    TempData["IsServiceAvailable"] = 0;
                    TempData["ParentService"] = d.userDetails.ParentService;
                    return RedirectToAction("ServicePurchase", "LoginSignup");

                }
                if (userdetailsSession.Type == "5")
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (d.serviceName.Count <= 0 && userdetailsSession.Type != "5")
                {
                    TempData["IsServiceAvailable"] = 0;
                    return RedirectToAction("ServicePurchase", "LoginSignup");
                }
                else
                {
                    //if (d.userDetails.emailVerified == 1 && d.userDetails.adhaarVerified == 1 && d.userDetails.mobileVerified == 1 && d.userDetails.panVerified == 1)
                    //{
                    //    return RedirectToAction("Index", "Dashboard");
                    //}
                    //else
                    //{
                        ViewBag.AllData = d;
                        ViewBag.id = d.userDetails.partyId;
                        ViewBag.UserName = userdetailsSession.Username;
                        ViewBag.UserType = userdetailsSession.UserType;
                        return View();
                    //}
                }
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        
        public ActionResult OurServices()
        {
            var serviceDetails = ZapurseCommonlist.GetServicesAllDetails();
            var HardwareDetails = ZapurseCommonlist.GetHardwaresAllDetails();

            ViewBag.serviceDetailsList = serviceDetails;
            ViewBag.HardwareDetailsList = HardwareDetails;
            return View();
        }

        [NoDirectAccess]
        public ActionResult Finalpage()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            if (userdetailsSession.PartyId == "A000001")
            {

               return RedirectToAction("PaymentPendingCollection", "Home");
            }
            else
            {
                var Details = (Data)Session["UserAllDetails"];
                ViewBag.Service = Details.serviceName;
                ViewBag.UserType = Details.userDetails.partyId;
                return View();
            }
        }

        public ActionResult NoDirectAccess()
        {
            return View();
        }
    }
}