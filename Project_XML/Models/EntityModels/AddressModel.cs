using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_XML.Models.EntityModels
{
    public class AddressModel
    {
        public string CountryCode { get; set; }
        public string Street { get; set; }
        public string BuildingIdentifier { get; set; }
        public string SuiteIdentifier { get; set; }
        public string FloorIdentifier { get; set; }
        public string DistrictName { get; set; }
        public string POB { get; set; }
        public string PostCode { get; set; }
        public string City { get; set; }
        public string CountrySubentity { get; set; }
        public string FreeLine { get; set; }
        public string AddressType { get; set; }
        public bool isFixed { get; set; }
    }
}