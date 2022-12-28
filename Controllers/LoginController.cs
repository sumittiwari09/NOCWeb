using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;

namespace NewZapures_V2.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        public static List<UserPermissions> GetPermissionDetails(int RoleId, int DepartmentId)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetPermissionDetails?RoleId=" + RoleId + "&DepartmentId=" + DepartmentId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<UserPermissions> permissions = new List<UserPermissions>();
            if (response.StatusCode.ToString() == "OK")
            {
                var responseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                permissions = JsonConvert.DeserializeObject<List<UserPermissions>>(responseData.Data.ToString());
            }
            return permissions;
        }
        public ActionResult Handshake()
        {

            HandshakeModel handshakeModel = new HandshakeModel();
            handshakeModel.SSOUserDetail = new SSOUserDetail();
            string BaseUrl = ConfigurationManager.AppSettings["SSOBaseUrl"].ToString();
            string AppName = ConfigurationManager.AppSettings["AppName"].ToString();
            string WebServiceUser = ConfigurationManager.AppSettings["WebServiceUser"].ToString();
            string WebServicePwd = ConfigurationManager.AppSettings["WebServicePwd"].ToString();

            using (RAJSSO.SSO SSO = new RAJSSO.SSO(AppName))
            {
                //try
                //{
                if (SSO.CreateSSOSession())
                {
                    var jsonUserDetail = "";

                    var jsonSSODetail = "";
                    var jsonUserDetails = "";
                    var jsonInternLogin = "";

                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    RAJSSO.SSOTokenDetail detail = SSO.GetSessionValue();

                    if (detail != null)
                    {

                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                        RAJSSO.SSOUserDetail UserDetail = SSO.GetUserDetail(detail.SSOID, WebServiceUser, WebServicePwd);
                        if (UserDetail != null)
                        {
                            SSOUserDetail _SSOUserDetail = new SSOUserDetail();
                            _SSOUserDetail.FirstName = UserDetail.FirstName;
                            _SSOUserDetail.IpPhone = UserDetail.IpPhone;
                            _SSOUserDetail.TelephoneNumber = UserDetail.TelephoneNumber;
                            _SSOUserDetail.State = UserDetail.State;
                            _SSOUserDetail.SSOID = UserDetail.SSOID;
                            _SSOUserDetail.PostalCode = UserDetail.PostalCode;
                            _SSOUserDetail.PostalAddress = UserDetail.PostalAddress;
                            _SSOUserDetail.Photo = UserDetail.Photo;
                            _SSOUserDetail.Mobile = UserDetail.Mobile;
                            _SSOUserDetail.MailPersonal = UserDetail.MailPersonal;
                            _SSOUserDetail.LastName = UserDetail.LastName;
                            _SSOUserDetail.MailOfficial = UserDetail.MailOfficial;
                            _SSOUserDetail.EmployeeNumber = UserDetail.EmployeeNumber;
                            _SSOUserDetail.DisplayName = UserDetail.DisplayName;
                            _SSOUserDetail.Designation = UserDetail.Designation;
                            _SSOUserDetail.DepartmentName = UserDetail.DepartmentName;
                            _SSOUserDetail.DepartmentId = UserDetail.DepartmentId;
                            _SSOUserDetail.DateOfBirth = UserDetail.DateOfBirth;
                            _SSOUserDetail.City = UserDetail.City;
                            _SSOUserDetail.BhamashahMemberId = UserDetail.BhamashahMemberId;
                            _SSOUserDetail.BhamashahId = UserDetail.BhamashahId;
                            _SSOUserDetail.AadhaarId = UserDetail.AadhaarId;
                            _SSOUserDetail.Gender = UserDetail.Gender;
                            _SSOUserDetail.OldSSOIDs = UserDetail.OldSSOIDs;
                            
                            SessionModel.SSOUserDetail = _SSOUserDetail;
                            jsonUserDetail = new JavaScriptSerializer().Serialize(_SSOUserDetail);

                            SSO.IncreaseSession();
                        }
                    }
                }
            }

                            return View();
        }
    }
}