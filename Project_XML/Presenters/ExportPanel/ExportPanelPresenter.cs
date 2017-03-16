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

        public object[] exportXML(string entries, Dictionary<string, string> reportArgs, string schemaPath)
        {
            CrsReport crs = new CrsReport();

            if (entries != null && !entries.Equals(""))
                return crs.NewReport(entries, reportArgs, schemaPath);
            else
                return null;
        }

        public void LogAction(string database, string server, string action, string status)
        {
            DateTime datetime = DateTime.UtcNow;

            // creating log entries
            string path = view.LogPath + "\\{0}.log";
            path = string.Format(path, datetime.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture));

            StringBuilder sb = new StringBuilder();

            if (database == "none")
            {
                sb.Append(string.Format("[{0} {1}]\nUser: {2}\nFile: {3}\nStatus: {4}\n\n", datetime.ToString("yyyy-mm-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                                                        , TimeZoneInfo.Local.ToString()
                                                        , view.Username
                                                        , server
                                                        , action + " " + status));
            }
            else
            {
                sb.Append(string.Format("[{0} {1}]\nUser: {2}\nDatabase: {3}\nServer: {4}\nStatus: {5}\n\n", datetime.ToString("yyyy-mm-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture)
                                                        , TimeZoneInfo.Local.ToString()
                                                        , view.Username
                                                        , database
                                                        , server
                                                        , action + " " + status));
            }

            view.LogMsg = System.Security.SecurityElement.Escape(sb.ToString()).Replace("\n", "<br>") + view.LogMsg;

            // export log into external file. log file is created for each day.
            Debug.WriteLine(path);
            File.AppendAllText(path, sb.ToString().Replace("\n", Environment.NewLine));
        }


        public void Import(string fullPath)
        {
            string action = "Upload";
            DbImportManager db = new DbImportManager();
            string ssqlconnectionstring = db.GetConnectionString("AeoiConnection");
            /*
             * 
             * Individual Sheet Excel
             * 
             */
            string indivSheetName = "Individual_tbl"; //also the sheet name in the excel file template

            int count = db.isTableExists(indivSheetName);

            if (count == 0)
                db.CreateIndividualTable();

            //declare variables - edit these based on your particular situation 
            string Import_FileName = fullPath;
            string fileExtension = Path.GetExtension(Import_FileName);
            //string ssqltable = "Individual_tbl";
            // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have    different 
            string myexceldataquery = "select * from [" + indivSheetName + "$]";
            try
            {
                //create our connection strings 
                string sexcelconnectionstring = string.Empty;
                if (fileExtension == ".xls")
                    sexcelconnectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    sexcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";


                //series of commands to bulk copy data from the excel file into our sql table 
                OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
                OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
                oledbconn.Open();
                OleDbDataReader dr = oledbcmd.ExecuteReader();
                SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
                bulkcopy.DestinationTableName = indivSheetName;
                bulkcopy.WriteToServer(dr);

                //System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ssqlconnectionstring);
                //string server = builder.DataSource;
                //string database = builder.InitialCatalog;
                //string status = "Success";

                //LogAction(database, server, action, status);

                /* while (dr.Read())
                 {
                     bulkcopy.WriteToServer(dr);
                 }*/

                dr.Close();
                oledbconn.Close();
            }
            catch (Exception ex)
            { Debug.WriteLine("Uploading error:" + ex.Message); }


            /*
           * 
           * Entity Sheet Excel
           * 
           */

            string entSheetName = "Entity_tbl"; //also the sheet name in the excel file template

            count = db.isTableExists(entSheetName);

            if (count == 0)
                db.CreateEntityTable();

            myexceldataquery = "select * from [" + entSheetName + "$]";
            try
            {
                //create our connection strings 
                string sexcelconnectionstring = string.Empty;
                if (fileExtension == ".xls")
                    sexcelconnectionstring = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    sexcelconnectionstring = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";

                //series of commands to bulk copy data from the excel file into our sql table 
                OleDbConnection oledbconn = new OleDbConnection(sexcelconnectionstring);
                OleDbCommand oledbcmd = new OleDbCommand(myexceldataquery, oledbconn);
                oledbconn.Open();
                OleDbDataReader dr = oledbcmd.ExecuteReader();
                SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
                bulkcopy.DestinationTableName = entSheetName;
                bulkcopy.WriteToServer(dr);

                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ssqlconnectionstring);
                string server = builder.DataSource;
                string database = builder.InitialCatalog;
                string status = "Success";
                LogAction(database, server, action, status);

                dr.Close();
                oledbconn.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Uploading error:" + ex.Message);
                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ssqlconnectionstring);
                string server = builder.DataSource;
                string database = builder.InitialCatalog;
                string status = "Failed";
                LogAction(database, server, action, status);
            }

            db.ImportEntityTable();
            db.ImportIndividualTable();

            DbExportManager db2 = new DbExportManager();

            view.AccountsList = db2.GetAllAccounts();
        }

        public void ClearLogs()
        {
            view.LogMsg = "";
        }
    }
}
