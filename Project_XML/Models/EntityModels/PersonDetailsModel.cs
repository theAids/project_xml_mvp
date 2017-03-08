using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_XML.Models.EntityModels
{
    public class PersonDetailsModel
    {
        public int PId { get; set; }
        public string PrecedingTitle { get; set; }
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string NamePrefix { get; set; }
        public string LastName { get; set; }
        public string GenerationIdentifier { get; set; }
        public string Suffix { get; set; }
        public string GeneralSuffix { get; set; }
        public string NameType { get; set; }
        public BirthDateModel Birthdate { get; set; }
        public string INVal { get; set; }
        public string[] ResCountryCode { get; set; }
        public List<AddressModel> Address { get; set; }
    }
}