using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NewZapures_V2.Controllers
{
    //[NoDirectAccess]
    public class MyPaymentsController : Controller
    {
        // GET: MPpayments
        ResponseData objResponse;
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


        public List<MyPayment> MyPayments(string partyId = null)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/MyPayment");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            List<MyPayment> payment = new List<MyPayment>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                payment = JsonConvert.DeserializeObject<List<MyPayment>>(objResponse.Data.ToString());
            }

            return payment;
        }


        public ActionResult GetPayments(int pageNumber, int pageSize)
        {
            List<MyPayment> payments = new List<MyPayment>();
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/MyPaymentOnScroll?pageNumber=" + pageNumber + "&pageSize=" + pageSize + "&PartyId=" + userdetailsSession.PartyId);
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
                if (objResponse.Data != null)
                {
                    payments = JsonConvert.DeserializeObject<List<MyPayment>>(objResponse.Data.ToString());

                }
            }
            return View(payments);
        }
    }
}