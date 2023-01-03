using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public partial class GeographicalMaster : AddType
    {
        public int? CountryId { get; set; }
        public int? StateId { get; set; }
        public int? DistrictId { get; set; }
        public int? CityId { get; set; }
        public int? AreaId { get; set; }
        public string Name { get; set; }
        public string PinCode { get; set; }
        public bool? IsActive { get; set; }
    }

    public partial class AddType
    {
        public string Type { get; set; }
    }
    public class GeographicalTableData
    {
        public int countryID { get; set; }
        public int stateID { get; set; }
        public int districtId { get; set; }
        public int cityId { get; set; }
        public int areaId { get; set; }
        public string countryName { get; set; }
        public string stateName { get; set; }
        public string districtName { get; set; }
        public string cityName { get; set; }
        public string areaName { get; set; }
        public string Pincode { get; set; }
        public string EntryType { get; set; }
    }

    public class GeoLocationMaster
    {
       public string Type { get; set; }
        public int? Id { get; set; }
    }
}