using Project_XML.Models.DbManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace Project_XML
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            DbAccountManager db = new DbAccountManager();
            db.CreateAccountsTable();
            //Default user, comment if necessary
            db.AddUser("admin", "admin", "user", "admin123", "Administrator");

        }
    }
}