﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_XML.Models.EntityModels
{
    public class CorrAccountModel
    {
        public string AcctNumber { set; get;}
        public string AcctHolder { set; get; }
        public int AcctHolderId { set; get; }
        public string Country { set; get; } 
        public string DocRefId { set; get; }
        public List<string> CorrAcctList { set; get; }
    }
}