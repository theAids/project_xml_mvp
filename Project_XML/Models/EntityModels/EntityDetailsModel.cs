using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_XML.Models.EntityModels
{
    public class EntityDetailsModel
    {
        public int EntityId { get; set; }
        public string Name { get; set; }
        public string NameType { get; set; }
        public string AcctHolderType { get; set; }
        public string INVal { get; set; }
    }
}