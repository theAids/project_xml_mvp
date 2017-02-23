using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_XML.Models.EntityModels
{
    public class ControllingPerson
    {
        public string PreceedingTitle { get; set; }
        public string Title { get; set; }
        public string Firstname { get; set; }
        public string MiddleName { get; set; }
        public string NamePrefix { get; set; }
        public string LastName { get; set; }
        public string GenerationIdentifier { get; set; }
        public string NameType { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthCity { get; set; }
        public string BirthCitySubentity { get; set; }
        public string BirthCountry { get; set; }
        public bool isIndividual { get; set; }
        public string CtrlPersonType { get; set; }
    }
}