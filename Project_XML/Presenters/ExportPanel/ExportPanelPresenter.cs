using Project_XML.Models.DbManager;
using Project_XML.Models.EntityModels;
using Project_XML.Schema;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Xml;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

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

                //populate return year dropdown list
                int[] yearList = new int[20];
                int year = DateTime.UtcNow.Year;

                for (int i = 0; i < 20; i++)
                {
                    yearList[i] = year;
                    year--;
                }

                view.YearList = yearList;

                DbExportManager db = new DbExportManager();

                //populate Accounts table
                view.AccountsList = db.GetAllAccounts();
            }

            view.LogPath = Directory.CreateDirectory(server.MapPath("~/logs")).FullName; // set log directory
        }

        public void ValidateRequest(bool RequestIsValid)
        {
            if (!RequestIsValid)
                UnauthenticatedRedirect(this, null);
        }

        public object[] exportXML(string entries, Dictionary<string, string> reportArgs)
        {
            CrsReport crs = new CrsReport();

            if (entries != null && !entries.Equals(""))
                return crs.NewReport(entries, reportArgs);
            else
                return null;
        }

        /*
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

            //export xml file
            File.AppendAllText(xmlpath, xmldoc.OuterXml);
            LogAction(conn);
        }
        */

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

        public void Import(string fullPath)
        {
            string sheetName = "Individual";
            string str = "SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '" + sheetName + "'";
            SqlConnection connection = new SqlConnection("Data Source = PH2160863W1\\SQLEXPRESS; Initial Catalog = AEOIDB;Integrated Security=True");
            SqlCommand myCommand = new SqlCommand(str, connection);
            SqlDataReader myReader = null;
            int count = 0;

            try
            {
                connection.Open();
                myReader = myCommand.ExecuteReader();
                while (myReader.Read())
                    count++;

                myReader.Close();
                connection.Close();
            }
            catch (Exception ex) { Debug.WriteLine("Uploading error:" + ex.Message); }
            if (count == 0)
            {

                //create Individual table 
                string databaseConnection_Ole = "Data Source = PH2160863W1\\SQLEXPRESS; Initial Catalog = AEOIDB; Integrated Security=SSPI; Provider=SQLOLEDB;";
                OleDbConnection dbcon = new OleDbConnection(databaseConnection_Ole);
                dbcon.Open();
                OleDbCommand dbcmd = dbcon.CreateCommand();
                dbcmd.CommandText = "CREATE TABLE " + sheetName +
                        " (" +
                        "[Name] VARCHAR(1000), [Address] VARCHAR(1000), " +
                        "[Date of birth] VARCHAR(1000), [Place of birth] VARCHAR(1000)," +
                        "[Jurisdiction of Tax Residence (Territory) (1)] VARCHAR(1000), [Jurisdiction of Tax Residence (Territory) (2)] VARCHAR(1000)," +
                        "[Jurisdiction of Tax Residence (Territory) (3)] VARCHAR(1000), [TIN(1)] VARCHAR(1000), [TIN(2)] VARCHAR(1000), [TIN(3)] VARCHAR(1000)," +
                        "[Reason for not obtaining the TIN (1)] VARCHAR(1000), [Reason for not obtaining the TIN (2)] VARCHAR(1000), [Reason for not obtaining the TIN (3)] VARCHAR(1000)," +
                        "[Account Number] VARCHAR(1000), [CPR Status] VARCHAR(1000), [Account Balance] VARCHAR(1000), [Gross amount interest] VARCHAR(1000)," +
                        "[Gross amount of dividend] VARCHAR(1000), [Gross amount of other income] VARCHAR(1000), [Gross proceeds] VARCHAR(1000)" +
                        ")";
                dbcmd.ExecuteNonQuery();
                dbcon.Close();
            }

            //declare variables - edit these based on your particular situation 
            string Import_FileName = fullPath;
            string fileExtension = Path.GetExtension(Import_FileName);
            string ssqltable = "Individual";
            // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have    different 
            string myexceldataquery = "select * from [" + ssqltable + "$]";
            try
            {
                //create our connection strings 
                string sexcelconnectionstring = string.Empty;
                if (fileExtension == ".xls")
                    sexcelconnectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    sexcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                string ssqlconnectionstring = "Data Source = PH2160863W1\\SQLEXPRESS; Initial Catalog = AEOIDB;Integrated Security=True";

                //series of commands to bulk copy data from the excel file into our sql table 
                OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
                OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
                oledbconn.Open();
                OleDbDataReader dr = oledbcmd.ExecuteReader();
                SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
                bulkcopy.DestinationTableName = ssqltable;
                while (dr.Read())
                {
                    bulkcopy.WriteToServer(dr);
                }
                dr.Close();
                oledbconn.Close();
            }
            catch (Exception ex)
            { Debug.WriteLine("Uploading error:" + ex.Message); }
        }

        public void ClearLogs()
        {
            view.LogMsg = "";
        }
    }
}
