using Project_XML.Models.DbManager;
using Project_XML.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;

namespace Project_XML.Presenters.ExportPanel
{
    public class ExportPanelPresenter
    {
        IExportPanelView view;
        public event EventHandler UnauthenticatedRedirect;

        public ExportPanelPresenter(IExportPanelView view)
        {
            if (view == null)
                throw new ArgumentNullException("View cannot be null.");
            this.view = view;
        }

        public void InitView(bool PageIsPostback, HttpServerUtility server)
        {
            if (!PageIsPostback)
            {
                //view.ConnStrings = DbConnManager.GetConnectionStrings(); // database table source      
            }
            view.LogPath = Directory.CreateDirectory(server.MapPath("~/logs")).FullName; // set log directory
        }

        public void ValidateRequest(bool RequestIsValid)
        {
            if (!RequestIsValid)
                UnauthenticatedRedirect(this, null);
        }

        public void exportXML(string connectionName)
        {

            DateTime datetime = DateTime.UtcNow;

            // get connection string name based from clicked LinkButton
            ConnectionStringModel conn = DbConnManager.GetConnectionStrings().Find(cs => cs.name == connectionName);

            string xmlpath = view.LogPath + "\\{0} {1}.xml";
            xmlpath = string.Format(xmlpath, datetime.ToString("yyyy-MM-dd HHmmss", System.Globalization.CultureInfo.InvariantCulture), conn.dbname);

            XmlDocument xmldoc = null;

            DbExportManager db = new DbExportManager();
            // execute corresponding actions for each db connection strings
            switch (connectionName)
            {
                case "PeopleConnection":
                    xmldoc = db.ExtractPeopleData();
                    break;
                case "TimeConnection":
                    xmldoc = db.ExtractTimeData();
                    break;
            }

            //export xml file
            File.AppendAllText(xmlpath, xmldoc.OuterXml);
            LogAction(conn);
        }

        protected void LogAction(ConnectionStringModel conn)
        {
            DateTime datetime = DateTime.UtcNow;

            // creating log entries
            string path = view.LogPath + "\\{0}.log";
            path = string.Format(path, datetime.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));

            StringBuilder sb = new StringBuilder();

            sb.Append(string.Format("[{0} {1}]\nUser: {2}\nDatabase: {3}\nServer: {4}\n", datetime.ToString("yyyy-mm-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                                                    , TimeZoneInfo.Local.ToString()
                                                    , view.Username
                                                    , conn.dbname
                                                    , conn.servername));

            view.LogMsg = System.Security.SecurityElement.Escape(sb.ToString()).Replace("\n", "<br>") + view.LogMsg;

            // export log into external file. log file is created for each day.
            Debug.WriteLine(path);
            File.AppendAllText(path, sb.ToString().Replace("\n", Environment.NewLine));
        }

        public void ClearLogs()
        {
            view.LogMsg = "";
        }
    }
}