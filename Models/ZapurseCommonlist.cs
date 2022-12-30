using BO.Models;
using Newtonsoft.Json;
using NewZapures_V2.Controllers;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Device.Location;
using static NewZapures_V2.Models.TrusteeBO;

namespace NewZapures_V2.Models
{
    public class ZapurseCommonlist
    {
        //private GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
        //private string Latitude = "";
        //private string Longitude = "";

        public static List<GlobalClass> GetCategoryTypeList()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Department"

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Services"

            });


            return Lst;

        }
        public static List<GlobalClass> GetMode()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Temporary",
                label= "radio-danger"

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Permanent",
                label = "radio-primary"

            });
            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Both",
                label = "radio-success"

            });

            return Lst;

        }
        public static List<GlobalClass> GetServicesRate()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "GST"

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "SSTC"

            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "AMC"

            });
            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Testing Rate"

            });
            return Lst;
        }


        public static List<GetservicesetailsAndroidNew> GetServicesAllDetails()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCategoryAllInformation?Type=Service");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<GetservicesetailsAndroidNew> serviceDetails = new List<GetservicesetailsAndroidNew>();
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if(objResponse.Data!= null)
                    serviceDetails = JsonConvert.DeserializeObject<List<GetservicesetailsAndroidNew>>(objResponse.Data.ToString());
            }
            return serviceDetails;
        }
        public static List<GetservicesetailsAndroidNew> GetHardwaresAllDetails()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCategoryAllInformation?Type=Hardware");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<GetservicesetailsAndroidNew> serviceDetails = new List<GetservicesetailsAndroidNew>();
            ResponseData objResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    serviceDetails = JsonConvert.DeserializeObject<List<GetservicesetailsAndroidNew>>(objResponse.Data.ToString());
            }
            return serviceDetails;
        }
        public static List<GlobalClass> GetHardwarelist()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Card swipe machie",
                strId = "card-swipe-machine-500x500.jpg"

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Thumb machine",
                strId = "ThumbMachine.jpg"

            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "AMC",
                strId = "ThumbMachine.jpg"

            });
            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Testing Rate",
                strId = "ThumbMachine.jpg"

            });
            return Lst;
        }
        public static List<GlobalClass> GetTypelist()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "White Label"
            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Stockist"

            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Distributer"

            });
            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Retailer"
            });
            Lst.Add(new GlobalClass
            {
                Id = 5,
                Text = "User"
            });
            return Lst;
        }
        public static List<Dropdown> GetDepartmentlist()
        {
            List<Dropdown> Lst = new List<Dropdown>();

            AdminController controller = new AdminController();

            Lst = controller.GetDepartments().Select(s => new Dropdown { Id = s.DepartmentID.ToString(), Text = s.DepartmentName }).Distinct().ToList();

            return Lst;
        }
        public static List<Dropdown> GetRoles()
        {
            List<Dropdown> Lst = new List<Dropdown>();

            AdminController controller = new AdminController();

            Lst = controller.GetRoles();

            return Lst;
        }
        public static List<Dropdown> GetCircle()
        {
            List<Dropdown> Lst = new List<Dropdown>();

            Lst.Add(new Dropdown
            {
                Text = "Andhra Pradesh Telangana"
            });
            Lst.Add(new Dropdown
            {
                Text = "Assam"
            });
            Lst.Add(new Dropdown
            {
                Text = "Bihar Jharkhand"
            });
            Lst.Add(new Dropdown
            {
                Text = "Chennai"
            });
            Lst.Add(new Dropdown
            {
                Text = "Delhi NCR"
            });
            Lst.Add(new Dropdown
            {
                Text = "Gujarat"
            });
            Lst.Add(new Dropdown
            {
                Text = "Haryana"
            });
            Lst.Add(new Dropdown
            {
                Text = "Himachal Pradesh"
            });
            Lst.Add(new Dropdown
            {
                Text = "Jammu Kashmir"
            });
            Lst.Add(new Dropdown
            {
                Text = "Karnataka"
            });
            Lst.Add(new Dropdown
            {
                Text = "Kerala"
            });
            Lst.Add(new Dropdown
            {
                Text = "Kolkata"
            }); Lst.Add(new Dropdown
            {
                Text = "Madhya Pradesh Chhattisgarh"
            }); Lst.Add(new Dropdown
            {
                Text = "Maharashtra Goa"
            }); Lst.Add(new Dropdown
            {
                Text = "Mumbai"
            }); Lst.Add(new Dropdown
            {
                Text = "North East"
            }); Lst.Add(new Dropdown
            {
                Text = "Orissa"
            }); Lst.Add(new Dropdown
            {
                Text = "Punjab"
            }); Lst.Add(new Dropdown
            {
                Text = "Rajasthan"
            });
            Lst.Add(new Dropdown
            {
                Text = "Tamil Nadu"
            });
            Lst.Add(new Dropdown
            {
                Text = "UP East"
            });
            Lst.Add(new Dropdown
            {
                Text = "UP West"
            });
            Lst.Add(new Dropdown
            {
                Text = "West Bengal"
            });
            return Lst.OrderBy(o => o.Text).ToList();
        }
        public static List<OperatorsList> GetMobileOperator()
        {
            List<OperatorsList> Lst = new List<OperatorsList>();

            Lst.Add(new OperatorsList
            {
                OperatorId = "1",
                OperatorName = "Airtel",
                OperatorImage = "Airtel.png",
                Type = "Mobile"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "2",
                OperatorName = "Idea",
                OperatorImage = "Idea.png",
                Type = "Mobile"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "3",
                OperatorName = "BSNL (Topup)",
                OperatorImage = "BSNL.png",
                Type = "Mobile"

            });

            Lst.Add(new OperatorsList
            {
                OperatorId = "4",
                OperatorName = "BSNL (Special)",
                OperatorImage = "BSNL.png",
                Type = "Mobile"

            });

            Lst.Add(new OperatorsList
            {
                OperatorId = "5",
                OperatorName = "JIO",
                OperatorImage = "RelianceJIO.png",
                Type = "Mobile"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "6",
                OperatorName = "Vodafone",
                OperatorImage = "Vodafone.png",
                Type = "Mobile"

            });


            return Lst;
        }
        public static List<OperatorsList> GetDTHOperator()
        {
            List<OperatorsList> Lst = new List<OperatorsList>();

            Lst.Add(new OperatorsList
            {
                OperatorId = "7",
                OperatorName = "Airtel DTH",
                OperatorImage = "AirtelDTH.png",
                Type = "DTH"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "8",
                OperatorName = "Dish TV",
                OperatorImage = "DishTV.png",
                Type = "DTH"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "11",
                OperatorName = "Tata Sky",
                OperatorImage = "TataSky.png",
                Type = "DTH"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "10",
                OperatorName = "Sun Direct",
                OperatorImage = "SunDirect.png",
                Type = "DTH"

            });
            Lst.Add(new OperatorsList
            {
                OperatorId = "12",
                OperatorName = "Videocon D2H",
                OperatorImage = "Videocon.png",
                Type = "DTH"

            });


            return Lst;
        }
        public static List<Dropdown> GetUserTypes()
        {
            List<Dropdown> dropdowns = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetUserType");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if(objResponse !=null)
                   dropdowns = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return dropdowns;
        }
        public static List<Dropdown> GetMenusList(string Type = "MenuList", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public static List<Dropdown> GetUniversities(string MenuId = "0", string Type = "University")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return modules;
        }
        public static List<Dropdown> GetNotificationTypeMaster(string MenuId = "0", string Type = "NotificationTypeMaster")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return modules;
        }
        public static List<Dropdown> GetNotificationDirectionFlow(string MenuId = "0", string Type = "NotificationFlowDirection")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return modules;
        }
        public static List<Dropdown> GetServideProviderList(string Type = "ServiceProvider")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=0");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> departments = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                departments = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return departments;
        }
        public static List<WalletLeft> GetWalletAmount(string partyID,string Type = "GetWalletAmount")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + partyID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<WalletLeft> wallet = new List<WalletLeft>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                wallet = JsonConvert.DeserializeObject<List<WalletLeft>>(objResponse.Data.ToString());
            }
            return wallet;
        }
        public static List<AadhaarDetails> GetAadhaarDetails(string partyID)
        {

            AdminController controller = new AdminController();

            List<AadhaarDetails> details = controller.GetAadhaarDetails(partyID);

            return details;
        }       
        public static List<NotificationMaster> GetNotificationMaster()
        //public static List<NotificationMaster> GeNotificationMaster(sting serviceTypeId, int descriptionId)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetNotificationMaster");
            //var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetNotificationMaster?serviceTypeId=" + serviceTypeId + "&descriptionId=" + descriptionId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<NotificationMaster> notificationsMaster = new List<NotificationMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                var responseData = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if(responseData.Data!= null)
                    notificationsMaster = JsonConvert.DeserializeObject<List<NotificationMaster>>(responseData.Data.ToString());
            }
            return notificationsMaster;
        }
        public static ResponseData SaveNotificationTransactionData(NotificationTransectionUserListRequest requestData)
        {
            var json = JsonConvert.SerializeObject(requestData);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/SaveNotificationTransactionAndUserList");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            ResponseData requestResponse = new ResponseData();
            if (response.StatusCode.ToString() == "OK")
            {
                 requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
             
            }
            return requestResponse;
        }
        public static List<NotificationOperationData> NotificationOperation(NotificationOperationRequest notificationOperation)
        {
            var json = JsonConvert.SerializeObject(notificationOperation);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/NotificationOperation");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

            ResponseData requestResponse = new ResponseData();
            List<NotificationOperationData> notificationData = new List<NotificationOperationData>();
            if (response.StatusCode.ToString() == "OK")
            {
                 requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
            }

            if (notificationOperation.Type == "MarkasRead" || notificationOperation.Type == "Delete")
            {
                requestResponse.Data = null;
                notificationData = new List<NotificationOperationData>();
            }
            else
            {
                if (requestResponse.Data != null)
                {
                    notificationData = JsonConvert.DeserializeObject<List<NotificationOperationData>>(requestResponse.Data.ToString());
                }
                else
                {
                    notificationData = new List<NotificationOperationData>();
                }
            }
            return notificationData;
        }
        public static List<CommissionRates> GetCommissionRatesList(string partyID, int PartyType, int operatorId, int serviceId, int serviceProviderId)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCommissionRates?partyID=" + partyID + "&PartyType=" + PartyType + "&operatorId=" + operatorId + "&serviceId=" + serviceId + "&serviceProviderId=" + serviceProviderId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<CommissionRates> commissionRates = new List<CommissionRates>();
            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (objResponse.Data != null)
                    commissionRates = JsonConvert.DeserializeObject<List<CommissionRates>>(objResponse.Data.ToString());
                else
                    commissionRates = new List<CommissionRates>();

            }
            return commissionRates;
        }

        #region Transection Table Data
        public static List<CommissionDistributionMaster> GetCommissionDistribution()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetCommissionDistribution");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<CommissionDistributionMaster> commissionRates = new List<CommissionDistributionMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                commissionRates = JsonConvert.DeserializeObject<List<CommissionDistributionMaster>>(objResponse.Data.ToString());
            }
            return commissionRates;
        }
        public static List<TransectionMaster> GetTransections()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetTransections");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<TransectionMaster> commissionRates = new List<TransectionMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                commissionRates = JsonConvert.DeserializeObject<List<TransectionMaster>>(objResponse.Data.ToString());
            }
            return commissionRates;
        }
        #endregion

        public static Dictionary<string, string> GetIPAddress()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST

            // Get the IP
         
            var etc = Dns.GetHostEntry(hostName).AddressList;
            var IPv6Address = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            var IPv4Address = Dns.GetHostEntry(hostName).AddressList[etc.Length-1].ToString();

            Dictionary<string, string> IpAddressCollection = new Dictionary<string, string>();
            IpAddressCollection.Add("IPv6_Address", IPv6Address);
            IpAddressCollection.Add("IPv4_Address", IPv4Address);
            IpAddressCollection.Add("Hostname", hostName);

            //watcher = new GeoCoordinateWatcher();
            //// Catch the StatusChanged event.  
            //watcher.StatusChanged += Watcher_StatusChanged;
            //// Start the watcher.  
            //watcher.Start();

            return IpAddressCollection;
        }


        //private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e) // Find GeoLocation of Device  
        //{
        //    try
        //    {
        //        if (e.Status == GeoPositionStatus.Ready)
        //        {
        //            // Display the latitude and longitude.  
        //            if (watcher.Position.Location.IsUnknown)
        //            {
        //                Latitude = "0";
        //                Longitude = "0";
        //            }
        //            else
        //            {
        //                Latitude = watcher.Position.Location.Latitude.ToString();
        //                Longitude = watcher.Position.Location.Longitude.ToString();
        //            }
        //        }
        //        else
        //        {
        //            Latitude = "0";
        //            Longitude = "0";
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Latitude = "0";
        //        Longitude = "0";
        //    }
        //}

        public static List<Dropdown> GetCollegeType(string Type = "CollegeType", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public static List<Dropdown> GetDistrict(string Type = "District", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        
        public static List<Dropdown> GetTehsil(int MenuId,string Type = "Tehsil")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<Dropdown> GetProjectSource(string Type = "ProjectSource", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }

        public static List<BankMaster> GetBankDetails(string partyID = "", string Type = "BankDetails")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + partyID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<BankMaster> bankDetailsList = new List<BankMaster>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                bankDetailsList = JsonConvert.DeserializeObject<List<BankMaster>>(objResponse.Data.ToString());
            }
            return bankDetailsList;
        }

        
        public static List<Dropdown> GetFeeType(string MenuId = "0", string Type = "FeeType")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> modules = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                modules = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return modules;
        }

        public static List<TrusteeBO.TrusteeMember> GetTrusteeMember(int MenuId, string Type = "TrusteeMember")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<TrusteeBO.TrusteeMember> trusteeList = new List<TrusteeBO.TrusteeMember>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                trusteeList = JsonConvert.DeserializeObject<List<TrusteeBO.TrusteeMember>>(objResponse.Data.ToString());
            }
            return trusteeList;
        }

        public static List<DraftApplication> GetDraftApplication(string applGUID = "")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "BasicDataDetails/GetDarftApplications?applGUID="+ applGUID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<DraftApplication> draftApplication = new List<DraftApplication>();
            if (response.StatusCode.ToString() == "OK")
            {
                var requestResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                if (requestResponse.Data != null)
                    draftApplication = JsonConvert.DeserializeObject<List<DraftApplication>>(requestResponse.Data.ToString());
            }
            return draftApplication;
        }

        public static List<Dropdown> GetCourseForDept(int MenuId, string Type = "Course")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                ResponseData objResponse = JsonConvert.DeserializeObject<ResponseData>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.Data.ToString());
            }
            return MenusList;
        }
        public enum ModalSize
        {
            Small,
            Medium,
            Large
        }
    }
}