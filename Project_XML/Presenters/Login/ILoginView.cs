using Project_XML.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

namespace Project_XML.Presenters.Login
{
    public interface ILoginView
    {
        HttpCookie Cookie { set; get; }
        bool loginErrorPanel_vis { set;}

    }
}
