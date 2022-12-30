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

namespace NewZapures_V2.Controllers.User
{
    //[NoDirectAccess]
    public class UserDetailsController : Controller
    {
        // GET: UserDetails
        ResponseData objResponse;
        public ActionResult Index()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/RegisteredUserData");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                request.AddHeader("authorization", "bearer " + Token + "");
                request.AddParameter("application/json", "", ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                List<RegistredUser> registereduser = new List<RegistredUser>();
                if (response.StatusCode.ToString() == "OK")
                {
                    objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                    registereduser = JsonConvert.DeserializeObject<List<RegistredUser>>(objResponse.Data.ToString());
                }
                ViewBag.UserDetails = registereduser;

                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }


        //public JsonResult UpdateUser(UpdateUserDetails userDetails)
        public JsonResult UpdateUser(string partyId, int status, string email, string username, string Parent)
        {
            //var userdetailsSession = (UserModelSession)Session["UserDetails"];

            var Token = Session["Token"];
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/ActiveinactiveUser?PartyId=" + partyId + "&status=" + status + "&username=" + username + "&emailaddress=" + email + "&parentID=" + Parent);
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
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Failure = false, Message = objResponse.Message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}