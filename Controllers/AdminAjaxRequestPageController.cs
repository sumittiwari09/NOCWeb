using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Common;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    [NoDirectAccess]
    public class AdminAjaxRequestPageController : Controller
    {
        // GET: AdminAjaxRequestPage
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonFunction objcf = new CommonFunction();

       public ActionResult Testing(int Id)
        {
            return View();
        }
        public ActionResult GenerateArchtable(int iParamId,int iSubCatId,int iUomId,string sAppId= "abc123")
        {
            List<ArchiMstDetail> LstApesData = new List<ArchiMstDetail>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Masters/GenerateArchtable?iParamId=" + iParamId + "&iSubCatId="+ iSubCatId + "&iUomId="+ iUomId + "&sAppId="+ sAppId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    LstApesData = JsonConvert.DeserializeObject<List<ArchiMstDetail>>(d.Data.ToString());
                }

            }
            return View(LstApesData);
        }
        public ActionResult GenerateAepslist(int Id, int CommissionMasterId)
        {
            List<UserCommission> LstApesData = new List<UserCommission>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "CommissionMaster/Admin_AEPS_Commission_Show?ServiceProviderid=" + Id + "&CommissionMasterId=" + CommissionMasterId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    LstApesData = JsonConvert.DeserializeObject<List<UserCommission>>(d.Data.ToString());
                }

            }
            return View(LstApesData);
        }

        public JsonResult SetCommissionAEPS(int iTxnAmtFm, int iTxnAmtTo, Decimal Amount, int CommissionMasterId, int UserType, int ServiceId, string IsdefaultPartyId, int ServiceProvider)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            UserCommission master = new UserCommission();
            master.CommissionAmount = Amount;
            master.TransactionAmountFrom = iTxnAmtFm;
            master.TransactionAmountTo = iTxnAmtTo;
            master.CommissionMasterId = CommissionMasterId;
            master.UserType = UserType;
            master.ServiceProviderId = ServiceProvider;
            master.PartyId = PartyId;
            master.ServiceId = ServiceId;
            master.DefaultUserPartyId = IsdefaultPartyId;
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/Admin_AEPS_Commission_Save");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", _JsonSerializer.Serialize(master), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                return new JsonResult
                {
                    Data = new { Data = objResponseData.Message, isvalid = 1, failure = false, msg = objResponseData.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "error" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult GenerateSlabData(int Id)
        {
            List<SLBMST> Slablist = new List<SLBMST>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/Admin_SlabData_Mst_Show?Serviceid=" + Id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    Slablist = JsonConvert.DeserializeObject<List<SLBMST>>(d.Data.ToString());
                }

            }
            return View(Slablist);
        }
        public ActionResult AddNewbranch(string Id)
        {
            List<Tree> _Tree = new List<Tree>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "RoleMaster/TreeList?PartyId=" + Id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    _Tree = JsonConvert.DeserializeObject<List<Tree>>(d.Data.ToString());
                }

            }
            ViewBag.partyId = Id;
            return View(_Tree);
        }
        public ActionResult GenerateRecharges(int OperaterId, int UserType, int ServiceId, int Id)
        {
            int OperaterEnum = Convert.ToInt32(TypeDocument.OperaterName);
            int ServiceProvider = Convert.ToInt32(TypeDocument.ServiceProvider);
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            List<UserCommission> Commissionlist = new List<UserCommission>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GetRechargeData?OperaterId=" + OperaterId + "&UserType=" + UserType + "&ServiceId=" + ServiceId + "&OperaterNameEnum=" + OperaterEnum + "&ServiceProviderEnum=" + ServiceProvider + "&PartyId=" + PartyId + "&Id=" + Id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    Commissionlist = JsonConvert.DeserializeObject<List<UserCommission>>(d.Data.ToString());
                }

            }
            return View(Commissionlist);

        }
        public ActionResult GenerateRechargesUserWise(int OperaterId, int UserType, int ServiceId, int Id)
        {
            int OperaterEnum = Convert.ToInt32(TypeDocument.OperaterName);
            int ServiceProvider = Convert.ToInt32(TypeDocument.ServiceProvider);
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            List<UserCommission> Commissionlist = new List<UserCommission>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GenerateRechargesUserWise?OperaterId=" + OperaterId + "&UserType=" + UserType + "&ServiceId=" + ServiceId + "&OperaterNameEnum=" + OperaterEnum + "&ServiceProviderEnum=" + ServiceProvider + "&PartyId=" + PartyId + "&Id=" + Id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (d.Data != null)
                {
                    Commissionlist = JsonConvert.DeserializeObject<List<UserCommission>>(d.Data.ToString());
                }

            }
            ViewBag.usertype = UserType;
            return View(Commissionlist);

        }

        public JsonResult UpdateSlabData(string Type, int Id, int status)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/Admin_SlabData_Mst_Update?Type=" + Type + "&Id=" + Id + "&status=" + status);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                return new JsonResult
                {
                    Data = new { Data = objResponseData.Message, isvalid = 1, failure = false, msg = objResponseData.Message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "error" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult InsertRecharges(int OperaterId, int UserType, int ServiceId, int Id)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;

            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/InsertRechargeData?OperaterId=" + OperaterId + "&UserType=" + UserType + "&ServiceId=" + ServiceId + "&PartyId=" + PartyId + "&Id=" + Id);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                return new JsonResult
                {
                    Data = new { Data = "", failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "error" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult InsertUserRecharges(int OperaterId, int UserType, int ServiceId, int Id, string IsdefaultPartyId = null)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;

            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/InsertUserRechargeData?OperaterId=" + OperaterId + "&UserType=" + UserType + "&ServiceId=" + ServiceId + "&PartyId=" + PartyId + "&Id=" + Id + "&IsdefaultPartyId=" + IsdefaultPartyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {

                return new JsonResult
                {
                    Data = new { Data = "", failure = false, msg = "Success" },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "error" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult RateCreate(int Id)
        {
            return View();
        }
        public JsonResult FillGeographicalData(string Type, int CountryId = 0, int StateId = 0, int DistrictId = 0, int City = 0)
        {

            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = (string)Session["Token"];

            List<Dropdown> data = new List<Dropdown>();
            data = Common.GeAllData(Type, CountryId, StateId, DistrictId, City, Token);
            //List<GlobalClass> objUsermaster = new List<GlobalClass>();
            //var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetGeographicalData?Id=" + Id + "&districtId="+ districtId+" &Type=" +Type);
            //var request = new RestRequest(Method.GET);
            //request.AddHeader("cache-control", "no-cache");
            ////request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //request.AddParameter("application/json", "", ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            //if (response.StatusCode.ToString() == "OK")
            //{
            //    var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            //    //var objResponseData = JsonConvert.DeserializeObject<ListCustom>(d.Data.ToString());
            //    //objUsermaster = objResponseData.ListRequest;
            //    if (d.ResponseCode == "001")
            //    {
            //        return new JsonResult
            //        {
            //            Data = new { Data = objUsermaster, failure = false, msg = "Success" },
            //            ContentEncoding = System.Text.Encoding.UTF8,
            //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //        };
            //    }
            //    else if (d.ResponseCode == "000" && d.statusCode == 1)
            //    {
            //        objUsermaster = JsonConvert.DeserializeObject<List<GlobalClass>>(d.Data.ToString());
            //        return new JsonResult
            //        {
            //            Data = new { Data = objUsermaster, failure = false, msg = "Success" },
            //            ContentEncoding = System.Text.Encoding.UTF8,
            //            JsonRequestBehavior = JsonRequestBehavior.AllowGet
            //        };
            //    }
            //}

            return new JsonResult
            {
                Data = new { Data = data, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #region Vivek
        public JsonResult UploadRequiredDocuments(UploadDoc upload)
        {
            var json = JsonConvert.SerializeObject(upload);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UploadRequiredDocument");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            UploadDoc doc = new UploadDoc();

            if (response.StatusCode.ToString() == "OK")
            {
                doc = JsonConvert.DeserializeObject<UploadDoc>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = doc.DocumentID, Data = doc ,Failure = false, Message = "Document Uploaded Successfully" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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
      
     
        #endregion
        public JsonResult FillDepartmentandGroup(int DepartmentId = 0)
        {
            List<Dropdown> data = new List<Dropdown>();
            data = Common.GetDepartmentGroup(DepartmentId);
            return new JsonResult
            {
                Data = new { Data = data, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult FillDepartmentandGroupMaster(string Type)
        {
            UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            string PartyId = servicesCollectiondata.PartyId;
            List<Dropdown> data = new List<Dropdown>();
            data = Common.GetDepartmentGroupMaster(Type, PartyId);
            return new JsonResult
            {
                Data = new { Data = data, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult FillSubMenu(int MenuId = 0)
        {
            List<Dropdown> data = new List<Dropdown>();
            data = Common.GetSubMenu(MenuId);
            return new JsonResult
            {
                Data = new { Data = data, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult FillDepartmentandGroupandRole(int DepartmentId, int GroupId)
        {
            List<Dropdown> data = new List<Dropdown>();
            data = Common.GetDepartmentGroupRole(DepartmentId, GroupId);
            return new JsonResult
            {
                Data = new { Data = data, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetUserList(int Type, string PartyId)
        {
            List<Dropdown> obj = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GetPartyInformationBasedonParentId?Type=" + Type + "&PartyId=" + PartyId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    obj = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return new JsonResult
            {
                Data = new { Data = obj, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult FillDepartment(int Type)
        {
            List<Dropdown> data = new List<Dropdown>();
            data = Common.FillDepartment(Type);
            return new JsonResult
            {
                Data = new { Data = data, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult Newitem()
        {
            return View();
        }

    }
}