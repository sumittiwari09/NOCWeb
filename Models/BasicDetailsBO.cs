using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewZapures_V2.Models
{
    public class BasicDetailsBO
    {
        public int? Id { get; set; }
        public string TrustInfoId { get; set; }
        public string CollegeName { get; set; }
        public int UniverSity { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int District { get; set; }
        public int Division { get; set; }
        public int SubDivision { get; set; }
        public int District1 { get; set; }
        public int Tehsil { get; set; }
        public int ParliamentArea { get; set; }
        public int AssembleArea { get; set; }
        public int CityTownVillage { get; set; }
        public int UrbanRular { get; set; }
        public int PanchayatSimiti { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public int PinCode { get; set; }
        public string Latitudedd { get; set; }
        public string Longitudedd { get; set; }
        public string AddiMobileNumber { get; set; }
        public string Websiteurl { get; set; }
        public string LandlineNumber { get; set; }
        public string CollageLogo { get; set; }
        public string CollageLogoExtension { get; set; }
        public string CollageLogoContenttype { get; set; }
        //public string Boys { get; set; }
        //public string Girls { get; set; }
        //public string CoEducation { get; set; }
        public string collegeLevel { get; set; }
        public string collegeMedium { get; set; }

        public List<ContactDetails> ContactDetails { get; set; }

    }

    public class ContactDetails
    {
        public string Name { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
    }
}