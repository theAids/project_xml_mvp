using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_XML.Models.EntityModels
{
    public class AccountDetailsModel
    {
        public string AcctNumber { get; set; }
        public string AcctNumberType { get; set; }
        public bool isUndocumented { get; set; }
        public bool isClosed { get; set; }
        public bool isDormant { get; set; }
        public decimal AccountBalance { get; set; }
        public string ACurrCode { get; set; }
        public string PaymentType { get; set; }
        public decimal Amount { get; set; }
        public string PCurrCode { get; set; }
    }
}