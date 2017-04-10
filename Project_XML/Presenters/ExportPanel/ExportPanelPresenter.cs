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

        public void InitView(bool PageIsPostback, HttpServerUtility server, string messageRefID)
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

                view.MessageRefIDList = db.GetAllMsgRefId();

                List<string> test = db.GetAllMsgRefId();

                if (test != null && test.Count > 0)
                    view.CorrAccountsList = db.GetCorrAccounts(test.FirstOrDefault().ToString());
                else
                    view.CorrAccountsList = db.GetCorrAccounts(null); 

            }

            view.LogPath = Directory.CreateDirectory(server.MapPath("~/logs")).FullName; // set log directory
        }

        public void ValidateRequest(bool RequestIsValid)
        {
            if (!RequestIsValid)
                UnauthenticatedRedirect(this, null);
        }

        public object[] exportXML(string entries, Dictionary<string, string> reportArgs, 
            string schemaPath, string FSN, string typeCheck)
        {
            List<Dictionary<string, string>> accountList = new List<Dictionary<string, string>>();
            CrsReport crs = new CrsReport();

            if (entries != null && !entries.Equals(""))
            {
                List<string> accounts = new List<string>();
                if (entries.Split(',').Length >= 1)
                    accounts = entries.Split(',').ToList();
                else
                    accounts.Add(entries);

                if (typeCheck == "Correction")
                {
                    foreach (string str in accounts)
                    {
                        var accountListContent = new Dictionary<string, string>();
                        
                        accountListContent.Add("AcctNumber", str.Split(':')[4]);
                        accountListContent.Add("AcctHolderId", str.Split(':')[7]);
                        accountListContent.Add("Country", str.Split(':')[5]);
                        accountListContent.Add("DocSpecType", "OECD2");
                        accountListContent.Add("CorrFileSerialNumber", FSN.ToString());
                        accountListContent.Add("CorrDocRefId", str.Split(':')[3]);
                        accountListContent.Add("CorrAcctNumber", str.Split(':')[0]);

                        accountList.Add(accountListContent);
                    };

                }

                else if (typeCheck == "New")
                {
                    foreach (string str in accounts)
                    {
                        var accountListContent = new Dictionary<string, string>();

                        accountListContent.Add("AcctNumber", str.Split(':')[0]);
                        accountListContent.Add("AcctHolderId", str.Split(':')[1]);
                        accountListContent.Add("Country", str.Split(':')[2]);
                        accountListContent.Add("DocSpecType", "OECD1");
                        accountListContent.Add("CorrFileSerialNumber", null);
                        accountListContent.Add("CorrDocRefId", null);
                        accountListContent.Add("CorrAcctNumber", null);

                        accountList.Add(accountListContent);
                    };
                }
                return crs.NewReport(accountList, reportArgs, schemaPath);
            }

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


        public void Import(string fullPath, string typeCheck)
        {
            string action = "Upload";
            DbImportManager db = new DbImportManager();
            string ssqlconnectionstring = db.GetConnectionString("AeoiConnection");
                  
            if (typeCheck == "Corrected")
            {
                db.DeleteCorrAcctNum();
            }
                

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
            // make sure your sheet name is correct, here sheet name is sheet1, so you can change your sheet name if have different 
            string myexceldataquery = string.Empty;
            myexceldataquery = "select * from [" + indivSheetName + "$]";
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

                DataTable dt = new DataTable();
                dt.Load(dr);
                dt.Columns.Add("AcctType", typeof(string));
                
                foreach (DataRow row in dt.Rows)
                {
                    //need to set value to NewColumn column
                    row["AcctType"] = typeCheck;   // or set it to some other value
                }

                SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
                bulkcopy.DestinationTableName = indivSheetName;
                bulkcopy.WriteToServer(dt);

                dr.Close();
                oledbconn.Close();
                dt.Clear();
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

                DataTable dt = new DataTable();
                dt.Load(dr);
                dt.Columns.Add("AcctType", typeof(string));

                foreach (DataRow row in dt.Rows)
                {
                    //need to set value to NewColumn column
                    row["AcctType"] = typeCheck;   // or set it to some other value
                }
                
                SqlBulkCopy bulkcopy = new SqlBulkCopy(ssqlconnectionstring);
                bulkcopy.DestinationTableName = entSheetName;
                bulkcopy.WriteToServer(dt);

                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder(ssqlconnectionstring);
                string server = builder.DataSource;
                string database = builder.InitialCatalog;
                string status = "Success";
                LogAction(database, server, action, status);

                dr.Close();
                oledbconn.Close();
                dt.Clear(); 
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
            db.DeleteSourceTables();

            /*
            if (typeCheck == "New")
                view.AccountsList = db2.GetAllAccounts();
            else if (typeCheck == "Corrected")
                view.CorrAccountsList = db2.GetCorrAccounts(messageRef, corrAccounts); 
            */
        }

        public void ClearLogs()
        {
            view.LogMsg = "";
        }

        public List<string> ReturnCorrAcctNum ()
        {
            DbExportManager db = new DbExportManager();
            return db.GetCorrAcctNum(); 
        }
    }
}
