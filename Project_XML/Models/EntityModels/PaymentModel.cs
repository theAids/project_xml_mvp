using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_XML.Models.EntityModels
{
    public class PaymentModel
    {
        public string PaymentType { get; set; }
        public decimal? Amount { get; set; }
        public string CurrCode { get; set; }
    }
}