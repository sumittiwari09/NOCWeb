using Newtonsoft.Json;
using NewZapures_V2.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using static NewZapures_V2.Models.Partial;

namespace NewZapures_V2.Models
{
    public static class Common
    {
        static JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        public static List<CustomList> GetCommonListData(CustomListRequest req)
        {
            List<CustomList> objRegistratedUser = new List<CustomList>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "ListData/GetList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", _JsonSerializer.Serialize(req), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var objResp = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResp.ResponseCode == "001")
                {
                    return null;
                }
                if (objResp.Data != null)
                {
                    objRegistratedUser = JsonConvert.DeserializeObject<List<CustomList>>(objResp.Data.ToString());
                }
                else
                {
                    return objRegistratedUser;
                }
            }
            return objRegistratedUser;
        }
        public static List<CustomMaster> GetServiceProviderBaseonServiceId(int ServiceId)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();


            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GetServiceProviderBaseonServiceId?ServiceId=" + ServiceId);
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
                    objUsermaster = JsonConvert.DeserializeObject<List<CustomMaster>>(d.Data.ToString());
                }


            }
            return objUsermaster;
        }
        public static List<CustomMaster> GetCustomMastersList(int Enum)
        {
            List<CustomMaster> objUsermaster = new List<CustomMaster>();


            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetCustomList?EnumNo=" + Enum);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                var objResponseData = JsonConvert.DeserializeObject<ListCustom>(d.Data.ToString());

                objUsermaster = objResponseData.ListRequest;

            }
            return objUsermaster;
        }
        public class CommonFunction
        {
            public string getIPAdd()
            {
                string ipaddress;
                ipaddress = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (ipaddress == "" || ipaddress == null)
                {
                    ipaddress = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }
                return ipaddress;
            }

        }
        public static List<Documentlist> GetUploadDocumentlist(string Id = "")
        {
            List<Documentlist> objCaterMastermaster = new List<Documentlist>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/GetUploadDocumentlist?PartyId=" + Id);
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
                    objCaterMastermaster = JsonConvert.DeserializeObject<List<Documentlist>>(d.Data.ToString());
                }

            }
            return objCaterMastermaster;
        }
        public static List<RegistratedUser> GetRegistratedUsers()
        {
            List<RegistratedUser> objRegistratedUser = new List<RegistratedUser>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/GetRegistratedUsers");
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
                    objRegistratedUser = JsonConvert.DeserializeObject<List<RegistratedUser>>(d.Data.ToString());
                }

            }
            return objRegistratedUser;
        }
        public static List<ACtivedServicesHardwarelist> GetActivedServicesandHardwareList(string Id = "")
        {
            List<ACtivedServicesHardwarelist> objRegistratedUser = new List<ACtivedServicesHardwarelist>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/GetActivedServicesandHardwareList?Id=" + Id);
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
                    objRegistratedUser = JsonConvert.DeserializeObject<List<ACtivedServicesHardwarelist>>(d.Data.ToString());

                }
            }
            return objRegistratedUser;
        }
        public static List<PayTracking> GetPayTrackings(string Id = "")
        {
            List<PayTracking> objRegistratedUser = new List<PayTracking>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "User/GetPayTrackings?Id=" + Id);
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
                    objRegistratedUser = JsonConvert.DeserializeObject<List<PayTracking>>(d.Data.ToString());

                }
            }
            return objRegistratedUser;
        }
        public static List<Dropdown> GeAllData(string Type, int CountryId, int StateId, int DistrictId, int City, string Token)
        {
            List<Dropdown> dropdowns = new List<Dropdown>();
            GeographicalMaster geographical = new GeographicalMaster();
            geographical.Type = Type;
            geographical.CountryId = CountryId;
            geographical.StateId = StateId;
            geographical.DistrictId = DistrictId;
            geographical.CityId = City;
            var json = JsonConvert.SerializeObject(geographical);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetGeographical");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                {
                    dropdowns = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return dropdowns;
        }
        public static List<setting> GetSettings()
        {
            List<setting> objRegistratedUser = new List<setting>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetSetting");
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
                    objRegistratedUser = JsonConvert.DeserializeObject<List<setting>>(d.Data.ToString());

                }
            }
            return objRegistratedUser;
        }
        public static List<CustomEnum> GetCustomEnum()
        {
            List<CustomEnum> objRegistratedUser = new List<CustomEnum>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "Admin/GetCustomEnum");
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
                    objRegistratedUser = JsonConvert.DeserializeObject<List<CustomEnum>>(d.Data.ToString());

                }
            }
            return objRegistratedUser;
        }
        public static List<Dropdown> GeAllData(string Type, int CountryId, int StateId, int DistrictId, int City)
        {
            List<Dropdown> dropdowns = new List<Dropdown>();
            GeographicalMaster geographical = new GeographicalMaster();
            geographical.Type = Type;
            geographical.CountryId = CountryId;
            geographical.StateId = StateId;
            geographical.DistrictId = DistrictId;
            geographical.CityId = City;
            var json = JsonConvert.SerializeObject(geographical);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetGeographical");
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
                if (objResponse.Data != null)
                {
                    dropdowns = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
                }
            }
            return dropdowns;
        }
        public static List<Dropdown> GetDepartmentGroup(int DepartmentId)
        {
            List<Dropdown> obj = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "RoleMaster/FillDepartmentandGroup?Id=" + DepartmentId);
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
            return obj;
        }
        public static List<Dropdown> GetDepartmentGroupMaster(string Type, string PartyId)
        {

            List<Dropdown> obj = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "RoleMaster/FillDepartmentandGroupMaster?Type=" + Type + "&PartyId=" + PartyId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            _JsonSerializer.MaxJsonLength = Int32.MaxValue; // Whatever max lengt
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
            return obj;
        }
        public static List<Dropdown> GetDepartmentGroupRole(int DepartmentId, int GroupId)
        {
            List<Dropdown> obj = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "RoleMaster/FillDepartmentandGroupwithRole?DepartmentId=" + DepartmentId + "&GroupId=" + GroupId);
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
            return obj;
        }
        public static List<Dropdown> GetSubMenu(int MenuId)
        {
            List<Dropdown> obj = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "RoleMaster/GetSubMenu?Id=" + MenuId);
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
            return obj;
        }
        public static List<RoleMastertable> MappingDepartmentandGroupIndex()
        {
            List<RoleMastertable> Roles = new List<RoleMastertable>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "RoleMaster/GetRoleMasterInformation");
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
            return Roles;
        }
         public static List<Dropdown> GetMobileOperator()
        {
            List<Dropdown> Lst = new List<Dropdown>();

            Lst.Add(new Dropdown
            {
                Id = "1",
                Text = "AIRTEL",
                ID1 = "/images/MobileOperators/Airtel.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "2",
                Text = "IDEA",
                ID1 = "/images/MobileOperators/Idea.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "3",
                Text = "BSNL (Topup)",
                ID1 = "/images/MobileOperators/BSNL.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "4",
                Text = "BSNL (Special)",
                ID1 = "/images/MobileOperators/BSNL.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "5",
                Text = "JIO",
                ID1 = "/images/MobileOperators/Jio.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "6",
                Text = "VODAFONE",
                ID1 = "/images/MobileOperators/Vodafone.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "7",
                Text = "AIRTEL DTH",
                ID1 = "/images/MobileOperators/AirtelDTH.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "8",
                Text = "DISH TV",
                ID1 = "/images/MobileOperators/DishTV.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "9",
                Text = "RELIANCE BIGTV",
                ID1 = "/images/MobileOperators/BigTV.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "10",
                Text = "SUN DIRECT",
                ID1 = "/images/MobileOperators/SunDirect.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "11",
                Text = "TATA SKY",
                ID1 = "/images/MobileOperators/TataSky.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "12",
                Text = "VIDEOCON D2H",
                ID1 = "/images/MobileOperators/VideoconD2H.png"
            });

            Lst.Add(new Dropdown
            {
                Id = "13",
                Text = "JIO ONLINE",
                ID1 = "/images/MobileOperators/Jio.png"
            });
            return Lst.OrderBy(o=> o.Text).ToList();
        }
        public static List<ServiceProviderRateconfiguration> GetServiceProviderRate(int Id)
        {
            List<ServiceProviderRateconfiguration> Ratelist = new List<ServiceProviderRateconfiguration>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/GetServiceProviderRate?Id=" + Id);
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
                    Ratelist = JsonConvert.DeserializeObject<List<ServiceProviderRateconfiguration>>(d.Data.ToString());
                }

            }
            return Ratelist;
        }
        public static List<Dropdown> FillDepartment(int Type)
        {
            List<Dropdown> obj = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "RoleMaster/FillDepartment?Id=" + Type);
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
            return obj;
        }
        public static List<SLBMST> GetSlabRange(int Id)
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
            return Slablist;
        }

        public static List<Dropdown> GetListClientOfAdmin()
        {
            List<Dropdown> list_Client = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/USP_List_ClientInfo_Of_Admin");
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
                    list_Client = JsonConvert.DeserializeObject<List<Dropdown>>(d.Data.ToString());
                }

            }
            return list_Client;
        }

        public static List<NEWANNMST> GetNewAndAnnouncement(int Type = 0, int ShowType = 0)
        {
            List<NEWANNMST> _lstNewAnnoucobj = new List<NEWANNMST>();
            var client = new RestClient(ConfigurationManager.AppSettings["URL"] + "CommissionMaster/Admin_News_Announcement_Show?Type=" + Type + "&ShowType=" + ShowType);
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
                    _lstNewAnnoucobj = JsonConvert.DeserializeObject<List<NEWANNMST>>(d.Data.ToString());
                }

            }
            return _lstNewAnnoucobj;

        }
    }
}