using Newtonsoft.Json;
using NewZapures_V2.Helper;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Controllers
{
    //[NoDirectAccess]
    public class RoleController : Controller
    {
        // GET: Role
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();

        public ResponseData ObjResponse = null;
        #region Menu Master
        public ActionResult MenuIndex()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            List<MenuMasters> menus = new List<MenuMasters>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetMenuMasterList");
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
                    menus = JsonConvert.DeserializeObject<List<MenuMasters>>(d.Data.ToString());
                }
            }


            var permissions = (List<UserPermissions>)Session["UserPermissions"];
            if (permissions != null)
            {
                var data = permissions.Where(wh => wh.SubMenu.Contains("Menu Index")).ToList();
                ViewBag.PagePermission = data;
            }
            ViewBag.PartyId = PartyId;
            return View(menus);
        }

        public ActionResult SubMenuIndex()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            List<SubMenuMaster> menus = new List<SubMenuMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetSubMenuMasterList");
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
                    menus = JsonConvert.DeserializeObject<List<SubMenuMaster>>(d.Data.ToString());
                }

            }
            var permissions = (List<UserPermissions>)Session["UserPermissions"];
            if (permissions != null)
            {
                var data = permissions.Where(wh => wh.SubMenu.Contains("Submenu Index")).ToList();
                ViewBag.PagePermission = data;
            }
            ViewBag.PartyId = PartyId;
            return View(menus);
        }
        public ActionResult MenuCreate(MenuMasters master)
        {
            MenuMasters obj = new MenuMasters();
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertMenuMaster");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalTitleMsg"] = "Success!";
                        if (objResponseData.Message == "Allready Exists In table")
                        {
                            TempData["SwalStatusMsg"] = "warning";
                            TempData["SwalTitleMsg"] = "warning!";
                        }
                        TempData["SwalMessage"] = objResponseData.Message;

                        return RedirectToAction("MenuIndex", "Role");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("MenuIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("MenuIndex", "Role");
                    }
                }
            }
            else
            {
                if (Id != 0)
                {

                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetMenuMaster?Id=" + Id);
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
                            obj = JsonConvert.DeserializeObject<MenuMasters>(d.Data.ToString());
                        }

                    }
                }
            }
            return View(obj);
        }
        #endregion
        #region Sub Menu Master
        public ActionResult SubMenuCreate(SubMenuMaster master)
        {
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            SubMenuMaster obj = new SubMenuMaster();
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertSubMenuMaster");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("SubMenuIndex", "Role");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("SubMenuIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("SubMenuIndex", "Role");
                    }
                }
            }
            else
            {
                if (Id != 0)
                {

                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetSubMenuMaster?Id=" + Id);
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
                            obj = JsonConvert.DeserializeObject<SubMenuMaster>(d.Data.ToString());
                        }

                    }
                }
                List<ShowMenuDropDown> Menulist = new List<ShowMenuDropDown>();
                Menulist = GetMenulist();
                ViewBag.Menulist = Menulist;
            }
            return View(obj);
        }
        #endregion
        #region RoleInformation
        public ActionResult RoleIndex()
        {
            List<RoleInformation> Roles = new List<RoleInformation>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetRoleInformation");
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
                    Roles = JsonConvert.DeserializeObject<List<RoleInformation>>(d.Data.ToString());
                }

            }
            return View(Roles);
        }
        public ActionResult RoleInformation(RoleInformation Master)
        {
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            RoleInformation obj = new RoleInformation();
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertRoleInformation");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("RoleIndex", "Role");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("RoleIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("RoleIndex", "Role");
                    }
                }
            }
            else
            {
                if (Id > 0)
                {

                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetSelectRoleInformation?Id=" + Id);
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
                            obj = JsonConvert.DeserializeObject<RoleInformation>(d.Data.ToString());

                        }

                    }

                }
                List<Dropdown> data = new List<Dropdown>();
                data = Common.GetDepartmentGroup(0);
                ViewBag.departmentlist = data;
            }
            return View(obj);
        }
        #endregion
        #region RoleMappingwithMenu
        public ActionResult RoleMappingIndex()
        {
            List<RoleMappingwithMenu> menus = new List<RoleMappingwithMenu>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetRoleMappingMenuList");
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
                    menus = JsonConvert.DeserializeObject<List<RoleMappingwithMenu>>(d.Data.ToString());
                }

            }
            return View(menus);
        }
        public ActionResult RoleMappingwithMenuCreate(RoleMappingwithMenu Master)
        {
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            RoleMappingwithMenu obj = new RoleMappingwithMenu();
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertRoleMappingwithMenu");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("RoleMappingIndex", "Role");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("RoleMappingIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("RoleMappingIndex", "Role");
                    }
                }
            }
            else
            {
                if (Id > 0)
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetSelectRoleWithMenu?Id=" + Id);
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
                            obj = JsonConvert.DeserializeObject<RoleMappingwithMenu>(d.Data.ToString());

                        }

                    }
                }
                List<Dropdown> data = new List<Dropdown>();
                data = Common.GetDepartmentGroup(0);
                ViewBag.departmentlist = data;
                List<ShowMenuDropDown> Menulist = new List<ShowMenuDropDown>();
                Menulist = GetMenulist();
                ViewBag.Menulist = Menulist;
            }
            return View(obj);
        }
        #endregion
        #region RoleMappingwithSubMenuCreate

        public ActionResult RoleSubMenuIndex()
        {
            List<MenuMappingwithSubmenu> obj = new List<MenuMappingwithSubmenu>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetMappingRoleWithSubMenu?Id=" + 0);
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
                    obj = JsonConvert.DeserializeObject<List<MenuMappingwithSubmenu>>(d.Data.ToString());

                }

            }
            return View(obj);
        }
        public ActionResult RoleMappingwithSubMenuCreate(MenuMappingwithSubmenu Master)
        {
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            MenuMappingwithSubmenu obj = new MenuMappingwithSubmenu();
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertRoleMappingwithSubMenu");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("RoleSubMenuIndex", "Role");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("RoleSubMenuIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("RoleSubMenuIndex", "Role");
                    }
                }
            }
            else
            {
                if (Id > 0)
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetMappingRoleWithSubMenu?Id=" + Id);
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
                            obj = JsonConvert.DeserializeObject<List<MenuMappingwithSubmenu>>(d.Data.ToString()).FirstOrDefault();

                        }

                    }
                }

                List<Dropdown> data = new List<Dropdown>();
                data = Common.GetDepartmentGroup(0);
                ViewBag.departmentlist = data;
                List<ShowMenuDropDown> Menulist = new List<ShowMenuDropDown>();
                Menulist = GetMenulist();
                ViewBag.Menulist = Menulist;
            }


            return View(obj);
        }
        #endregion
        #region rolemastername
        public ActionResult RoleMasterIndex()
        {
            UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            string PartyId = servicesCollectiondata.PartyId;

            List<RoleMastertable> Roles = new List<RoleMastertable>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetRoleMasterInformation?PartyId=" + PartyId);
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
                    Roles = JsonConvert.DeserializeObject<List<RoleMastertable>>(d.Data.ToString());
                }

            }

            var permissions = (List<UserPermissions>)Session["UserPermissions"];
            if (permissions != null)
            {
                var data = new List<UserPermissions>(); //permissions.Where(wh => wh.SubMenu.Contains("Menu Index")).ToList();
                ViewBag.PagePermission = data;
            }


            return View(Roles);
        }
        public ActionResult RoleMasterCreate(RoleMastertable Master)
        {
            UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            Master.PartyId = servicesCollectiondata.PartyId;

            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            RoleMastertable obj = new RoleMastertable();
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertRoleMasterCreate");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("RoleMasterIndex", "Role");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("RoleMasterIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("RoleMasterIndex", "Role");
                    }
                }
            }
            else
            {
                if (Id > 0)
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetRoleMasterInformation?Id=" + Id);
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
                            obj = JsonConvert.DeserializeObject<List<RoleMastertable>>(d.Data.ToString()).FirstOrDefault();

                        }

                    }
                }

            }
            return View(obj);
        }
        #endregion

        #region RoleMappingDepartmentandGroup
        public ActionResult RoleMappingDepartmentandGroupIndex()
        {
            UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            List<MappingRoleWithDepartmentandGroup> Roles = new List<MappingRoleWithDepartmentandGroup>();

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetRoleMappingDepartmentandGroup?Id=" + PartyId);
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
                    Roles = JsonConvert.DeserializeObject<List<MappingRoleWithDepartmentandGroup>>(d.Data.ToString());
                }

            }
            return View(Roles);
        }
        public ActionResult RoleMappingDepartmentandGroupCreate(MappingRoleWithDepartmentandGroup Master)
        {
            //int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            //MappingRoleWithDepartmentandGroup obj = new MappingRoleWithDepartmentandGroup();
            UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var Type = servicesCollectiondata.Type;
            var partyId = servicesCollectiondata.PartyId;
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertMappingRoleWithDepartmentandGroup");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                    if (objResponseData.ResponseCode == "001")
                    {
                        return RedirectToAction("SignOut", "Home");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.Message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("RoleMappingDepartmentandGroupIndex", "Role");
                    }
                    else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("RoleMappingDepartmentandGroupIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("RoleMappingDepartmentandGroupIndex", "Role");
                    }
                }
            }
            //else
            //{
            //    if (Id > 0)
            //    {
            //        var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "RoleMaster/GetRoleMappingDepartmentandGroup?Id=" + Id);
            //        var request = new RestRequest(Method.GET);
            //        request.AddHeader("cache-control", "no-cache");
            //        //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //        request.AddParameter("application/json", "", ParameterType.RequestBody);
            //        IRestResponse response = client.Execute(request);
            //        if (response.StatusCode.ToString() == "OK")
            //        {
            //            var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            //            if (d.Data != null)
            //            {
            //                obj = JsonConvert.DeserializeObject<List<MappingRoleWithDepartmentandGroup>>(d.Data.ToString()).FirstOrDefault();

            //            }

            //        }
            //    }
            //    List<Dropdown> data = new List<Dropdown>();
            //    data = Common.GetDepartmentGroup(0);
            //    ViewBag.departmentlist = data;
            //    List<RoleMastertable> Rolelist = new List<RoleMastertable>();
            //    Rolelist = Common.MappingDepartmentandGroupIndex();
            //    ViewBag.RoleList = Rolelist;
            //}
            //return View(obj);
            ViewBag.Type = Type;
            ViewBag.partyId = partyId;
            return View();
        }
        #endregion

        #region UserPagingPermission
        public ActionResult Userpermission(int MappingId, int GroupId)
        {
            List<UserPagingPermission> obj = new List<UserPagingPermission>();
            if (obj == null)
            {
                return RedirectToAction("SignOut", "Home");
            }
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetUserPagingPermission?MappingId=" + MappingId + "&GroupId=" + GroupId);
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
                    obj = JsonConvert.DeserializeObject<List<UserPagingPermission>>(d.Data.ToString());

                }

            }
            ViewBag.mapping = MappingId;
            return View(obj);
        }
        public ActionResult UserPermissionforUser(int MappingId, int GroupId)
        {
            UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            if (servicesCollectiondata == null)
            {
                return RedirectToAction("SignOut", "Home");
            }
            int RoleId = servicesCollectiondata.RoleId;
            int DepartmentId = servicesCollectiondata.DepartmentId;
            List<UserPagingPermission> obj = new List<UserPagingPermission>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetSeletctedUserPagingPermission?MappingId=" + MappingId + "&GroupId=" + GroupId + "&DepartmentId=" + DepartmentId + "&RoleId=" + RoleId);
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
                    obj = JsonConvert.DeserializeObject<List<UserPagingPermission>>(d.Data.ToString());

                }

            }
            ViewBag.mapping = MappingId;
            return View(obj);
        }
        #endregion

        #region CreateGroup
        public ActionResult CreateGroup()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];

            if (servicesCollectiondata != null)
            {
                string partyId = servicesCollectiondata.PartyId;

                var groups = GetGroups(partyId);
                var Menus = GetSelectMenulist(partyId, "Menulist");


                ViewBag.groups = groups;
                ViewBag.Menus = Menus;

                return View();
            }
            else
            {
                return RedirectToAction("login-alt", "authentication");
            }
        }
        public List<AddGroup> GetGroups(string partyId = null, string Type = "GroupList")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=0" + "&PartyId=" + partyId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AddGroup> groups = new List<AddGroup>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                groups = JsonConvert.DeserializeObject<List<AddGroup>>(objResponse.Data.ToString());
            }
            return groups;
        }
        public List<Dropdown> GetSelectMenulist(string partyId, string Type, int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetMenulist?PartyId=" + partyId + "&Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> groups = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                groups = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return groups;
        }
        public List<Dropdown> GetRoleList(string Type = "RoleList", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> groups = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                groups = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return groups;
        }
        
        public List<Dropdown> GetDepartmentList(string Type = "Department", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> groups = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                groups = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return groups;
        }
        public List<PartyMaster> GetPartyMasterList(string Type = "SelectpartyMaster", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<PartyMaster> partylist = new List<PartyMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                partylist = JsonConvert.DeserializeObject<List<PartyMaster>>(objResponse.Data.ToString());
            }
            return partylist;
        }
        public JsonResult GetSelectSubmenulist(int MenuId)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            string partyId = servicesCollectiondata.PartyId;

            List<Dropdown> lst = new List<Dropdown>();
            lst = GetSelectMenulist(partyId, "SubMenulist", MenuId);
            return new JsonResult
            {
                Data = new { Data = lst, failure = false, msg = "Success", isvalid = 1 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        
        public JsonResult GetTehsil(string distID)
        {
            var tehsilList  = ZapurseCommonlist.GetTehsil(distID);
            return new JsonResult
            {
                Data = new { Data = tehsilList, failure = false, msg = "Success", isvalid = 1 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

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

        #region Notification

        public ActionResult NotificationIndex()
        {

            var notificationMaster = ZapurseCommonlist.GetNotificationMaster();

            ViewBag.notificationMaster = notificationMaster;
            return View();
        }
        public ActionResult NotificationMaster()
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            if (servicesCollectiondata != null)
            {
                var Menus = ZapurseCommonlist.GetMenusList();
                var NotificationForList = ZapurseCommonlist.GetNotificationTypeMaster();
                var NotificationDirectionFlow = ZapurseCommonlist.GetNotificationDirectionFlow();
                ViewBag.Menus = Menus;
                ViewBag.NotificationForList = NotificationForList;
                ViewBag.NotificationDirectionFlow = NotificationDirectionFlow;

                return View();
            }
            else
            {
                return RedirectToAction("login-alt", "authentication");
            }
        }
        
        public ActionResult NotificationList()
        {
            return View();
        }

        public JsonResult SaveNotificationMaster(NotificationMaster notification)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];

            notification.CreatedBy = servicesCollectiondata.PartyId;
            var json = JsonConvert.SerializeObject(notification);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/SaveNotificationMaster");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            ResponseData objResponse = new ResponseData();
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
        
        public JsonResult ReadNotification(NotificationOperationRequest notification)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];

            notification.PartyId = servicesCollectiondata.PartyId;
            var json = JsonConvert.SerializeObject(notification);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/NotificationOperation");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            ResponseData objResponse = new ResponseData();
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
        
        public JsonResult DeleteNotification(NotificationOperationRequest notification)
        {
            var servicesCollectiondata = (UserModelSession)Session["UserDetails"];

            notification.PartyId = servicesCollectiondata.PartyId;
            var json = JsonConvert.SerializeObject(notification);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/NotificationOperation");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            ResponseData objResponse = new ResponseData();
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


        #endregion

        public JsonResult ChangeStatus(string TableId, int type, int Id)
        {
            Activeclass objRatemaster = new Activeclass();
            objRatemaster.Id = Id;
            objRatemaster.Tablename = TableId;
            objRatemaster.status = type;


            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Admin/ChangeStatus");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            //request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(objRatemaster), ParameterType.RequestBody);
            IRestResponse response = client2.Execute(request2);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }

            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }

        public JsonResult OperationStatus(string Typedata, int MappingId, int PermissionId, int MstGroupId, int status)
        {
            Permissionclass obj = new Permissionclass();
            obj.Type = Typedata;
            obj.MappingId = MappingId;
            obj.PermissionId = PermissionId;
            obj.MstGroupId = MstGroupId;
            obj.status = status;
            //if (status == 0)
            //{
            //    obj.status = 1;
            //}
            //else
            //{
            //    obj.status = 0;
            //}


            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/OperationStatus");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            //request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client2.Execute(request2);
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseDataBO objResponseData = _JsonSerializer.Deserialize<ResponseDataBO>(response.Content);
                if (objResponseData.ResponseCode == "001")
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.ResponseCode == "000" && objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }

            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public List<ShowMenuDropDown> GetMenulist()
        {
            List<ShowMenuDropDown> menus = new List<ShowMenuDropDown>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetMenuMasterList");
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
                    List<MenuMasters> objlist = new List<MenuMasters>();
                    objlist = JsonConvert.DeserializeObject<List<MenuMasters>>(d.Data.ToString());
                    var menulist = objlist.Select(p => new { p.MenuId, p.MenuName }).ToList();
                    foreach (var item in menulist)
                    {
                        ShowMenuDropDown obj = new ShowMenuDropDown();
                        obj.MenuId = item.MenuId;
                        obj.MenuName = item.MenuName;
                        menus.Add(obj);
                    }
                }
            }
            return menus;
        }

        #region Partner and System User
        public ActionResult AddPartner()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];

            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                ViewBag.userType = userdetailsSession.Type;
                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        [HttpPost]
        public ActionResult SaveDetails(PartyMaster party)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(party);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/AddPartnerDetails");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    if (objResponse.Message.Contains("Details Already Exists..!"))
                    {
                        TempData["IsUserDetailsExists"] = 1;
                        TempData["msg"] = "User Details Already Exists, Details Not Saved...";
                        return RedirectToAction("AddPartner", "Role");
                    }
                    else
                    {
                        var NotificationList = (List<NotificationMaster>)Session["notificationList"];
                        var TowhomSendNotification = NotificationList.Where(wh => wh.SubMenuName.Contains("Add Partner") && wh.EventName == "Save").FirstOrDefault();

                        if (TowhomSendNotification != null)
                        {
                            var data = new NotificationTransectionUserListRequest { PartyID = userdetailsSession.PartyId, FlowDirection = TowhomSendNotification.SendtoFlow, configId = TowhomSendNotification.ConfId, specificUser = null };

                            ZapurseCommonlist.SaveNotificationTransactionData(data);
                        }

                        TempData["IsUserDetailsExists"] = 0;
                        TempData["msg"] = "Details Saved Successfully...!";
                        return RedirectToAction("AddPartner", "Role");
                    }
                }
                else
                {
                    TempData["IsUserDetailsExists"] = 1;
                    TempData["msg"] = "Details Not Saved Due To Some Internal Issues...!";
                    return RedirectToAction("AddPartner", "Role");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ActionResult AddSystemUser()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {
                ViewBag.userType = userdetailsSession.Type;
               var departmentList =  GetDepartmentList();
               var districtList = ZapurseCommonlist.GetDistrict();
                ViewBag.document = departmentList;
                ViewBag.districts = districtList;


                return View();
            }
            else
            {
                return RedirectToAction("SignOut", "Home");
            }
        }
        [HttpPost]
        public ActionResult Savesystemuserdetails(PartyMaster party)
        {
            try
            {
                var userdetailsSession = (UserModelSession)Session["UserDetails"];
                party.ParentId = userdetailsSession.PartyId;
                var json = JsonConvert.SerializeObject(party);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/AddSystemUserDetails");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);

                    if (objResponse.Message.Contains("Details Already Exists..!"))
                    {
                        TempData["IsUserDetailsExists"] = 1;
                        TempData["msg"] = "User Details Already Exists, Details Not Saved...";
                        return RedirectToAction("AddSystemUser", "Role");
                    }
                    else
                    {
                        TempData["IsUserDetailsExists"] = 0;
                        TempData["msg"] = "Record Saved Successfully!";
                        return RedirectToAction("AddSystemUser", "Role");
                    }
                }
                else
                {
                    TempData["IsUserDetailsExists"] = 1;
                    TempData["msg"] = "Details Not Saved Due To Some Internal Issues...!";
                    return RedirectToAction("AddSystemUser", "Role");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult GetHierarchyList()
        {
            UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            var PartyId = servicesCollectiondata.PartyId;
            List<Tree> _Tree = new List<Tree>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/TreeList?PartyId=" + PartyId);
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
            ViewBag.partyId = PartyId;
            return View(_Tree);
        }

        #endregion
    }
}