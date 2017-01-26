using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_XML.Models.EntityModels
{
    public class ConnectionStringModel
    {
        public int index { get; set; }
        public string name { get; set; }
        public string servername { get; set; }
        public string dbname { get; set; }
    }
}