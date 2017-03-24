﻿using Project_XML.Models.EntityModels;
using Project_XML.Presenters.ExportPanel;
using Project_XML.Presenters.UserMenu_uc;
using Project_XML.Views.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using static System.Net.Mime.MediaTypeNames;

namespace Project_XML.Views
{
    public partial class ExportPanel : System.Web.UI.Page, IExportPanelView
    {
        private ExportPanelPresenter presenter;

        public string Username
        {
            get
            {
                Literal curr = UserMenu1.FindControl("currentUser") as Literal;
                return curr.Text;
            }
        }

        public string LogMsg
        {
            set{ logPanel.Text = value;}
            get{ return logPanel.Text; }
        }

        public string LogPath
        {
            set; get;
        }

        public int[] YearList
        {
            set
            {
                newYear.DataSource = value;
                newYear.DataBind();
            }
        }

        public List<AccountModel> AccountsList
        {
            set
            {
              accountsList.DataSource = value;
              accountsList.DataBind();

            CorrRepeater.DataSource = value;
            CorrRepeater.DataBind(); 
            }
        }
        // Seperate public... for dropdown and table 

        public string UploadID
        {
            set
            {
                uploadID.Text = value;
            }
        }
        /*********************** Code-Behind *********************************/

        public ExportPanel()
        {
            presenter = new ExportPanelPresenter(this);
            presenter.UnauthenticatedRedirect += new EventHandler(UnauthenticatedRedirect);
            
        }

        protected void Page_PreInit(object sender, EventArgs e)
        {
            presenter.ValidateRequest(Request.IsAuthenticated);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            UserMenuPresenter umPresenter = new UserMenuPresenter(UserMenu1);
            UserMenu1.AttachPresenter(umPresenter);
            umPresenter.RenderMenu(HttpContext.Current);

            presenter.InitView(Page.IsPostBack, Server);
        }

        protected void exportXML(object sender, CommandEventArgs e)
        {
          //  presenter.exportXML(e.CommandArgument.ToString());

        }

        protected void ClearLog(object sender, EventArgs e)
        {
            presenter.ClearLogs();
        }

        protected void UnauthenticatedRedirect(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Login.aspx", true);
        }

        protected void CreateNewData(object sender, EventArgs e)
        {

            Dictionary<string, string> reportArgs = new Dictionary<string, string>()
            {
                { "year", newYear.SelectedValue},
                { "aeoiId", "AZ00099"},
                { "msgSpecType", "CRS701"}, //CRS702 for corrected 
                { "contact", newContact.Text},
                { "attentionNote", newAttentionNote.Text},
                
            };

            object[] obj = presenter.exportXML
                (accountSelected.Value, reportArgs, Server.MapPath("~/schema"), null, "New");

            if (obj != null)
            {
                XmlDocument xmlDoc = (XmlDocument)obj[0];

                MemoryStream mem = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(mem, Encoding.Unicode);

                writer.Formatting = Formatting.Indented;    //for indented format XML

                xmlDoc.WriteContentTo(writer);
                writer.Flush();

                byte[] bytes = mem.ToArray();
                mem.Close();

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "text/xml";
                Response.AddHeader("content-disposition", String.Format("attachment; filename={0}.xml", obj[1].ToString()));
                Response.AppendHeader("Content-Length", bytes.Length.ToString());
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Flush();
            }
        }

        protected void CorrectData(object sender, EventArgs e)
        {

            Dictionary<string, string> reportArgs = new Dictionary<string, string>()
            {
                { "year", newYear.SelectedValue},
                { "aeoiId", "AZ00099"},
                { "msgSpecType", "CRS701"}, //CRS702 for corrected 
                { "contact", newContact.Text},
                { "attentionNote", newAttentionNote.Text},

            };

            object[] obj = presenter.exportXML
                (accountSelected.Value, reportArgs, Server.MapPath("~/schema"), corrFSN.ToString(), "Correction");

            if (obj != null)
            {
                XmlDocument xmlDoc = (XmlDocument)obj[0];

                MemoryStream mem = new MemoryStream();
                XmlTextWriter writer = new XmlTextWriter(mem, Encoding.Unicode);

                writer.Formatting = Formatting.Indented;    //for indented format XML

                xmlDoc.WriteContentTo(writer);
                writer.Flush();

                byte[] bytes = mem.ToArray();
                mem.Close();

                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "text/xml";
                Response.AddHeader("content-disposition", String.Format("attachment; filename={0}.xml", obj[1].ToString()));
                Response.AppendHeader("Content-Length", bytes.Length.ToString());
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Flush();
            }
        }

        protected void UploadNewFile(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Uploads/");
            string fullPath = folderPath + Path.GetFileName(FileUpload1.FileName);
           
            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            try
            {
                FileUpload1.SaveAs(fullPath);
                presenter.Import(fullPath, "New");
                UploadPanel.Visible = true; 
                UploadID = "Upload Success!";
                UploadPanel.CssClass = "alert alert-success user-status";
                UploadIcon.CssClass = "glyphicon glyphicon-ok-circle";

                presenter.LogAction("none", Path.GetFileName(FileUpload1.FileName).ToString(), "New File Save", "Success"); 
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Uploading error:" + ex.Message);
                UploadPanel.Visible = true;
                UploadID = "Upload Failed!";
                UploadPanel.CssClass = "alert alert-danger user-status";
                UploadIcon.CssClass = "glyphicon glyphicon-remove-circle";
                presenter.LogAction("none", Path.GetFileName(FileUpload1.FileName).ToString(), "New File Save", "failed");
            }
        }

        protected void UploadCorrectedFile(object sender, EventArgs e)
        {
            string folderPath = Server.MapPath("~/Uploads/");
            string fullPath = folderPath + Path.GetFileName(FileUpload2.FileName);

            List<string> strlist = new List<string>();

            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            //Save the File to the Directory (Folder).
            try
            {
                FileUpload2.SaveAs(fullPath);
                presenter.Import(fullPath, "Corrected");

                /* CONVERTING ACCOUNT NUMBER EXCEL COLUMN TO LIST */
                string indivSheetName = "Individual_tbl";
                string entSheetName = "Entity_tbl";

                List<string> values = new List<string>(); 

                Microsoft.Office.Interop.Excel._Application oApp = new Microsoft.Office.Interop.Excel.Application();

                Microsoft.Office.Interop.Excel.Workbook oWorkbook = oApp.Workbooks.Open(fullPath);
                Microsoft.Office.Interop.Excel.Worksheet oWorksheet = oWorkbook.Worksheets[indivSheetName];
                Microsoft.Office.Interop.Excel.Worksheet pWorksheet = oWorkbook.Worksheets[entSheetName];

                int colNo = oWorksheet.UsedRange.Columns.Count;
                int rowNo = oWorksheet.UsedRange.Rows.Count;

                // read the value into an array.
                object[,] indiv_array = oWorksheet.UsedRange.Value;
                for (int j = 1; j <= colNo; j++)
                {
                    for (int i = 1; i <= rowNo; i++)
                    {
                        if (indiv_array[i, j] != null)
                            if (indiv_array[i, j].ToString() == "Account Number")
                            {
                                for (int m = i + 1; m < rowNo; m++)
                                {
                                    values.Add(
                                        indiv_array[m, j].ToString().Replace(',', ' ')
                                        + " : "
                                        + indiv_array[m, j - 10].ToString().Replace(',', ' ')
                                    );
                                }
                            }
                    }
                }
                object[,] ent_array = pWorksheet.UsedRange.Value;
                for (int j = 1; j <= colNo; j++)
                {
                    for (int i = 1; i <= rowNo; i++)
                    {
                        if (ent_array[i, j] != null)
                            if (ent_array[i, j].ToString() == "Account Number")
                            {
                                for (int m = i + 1; m < rowNo; m++)
                                {
                                    values.Add(
                                        ent_array[m, j].ToString().Replace(',', ' ') 
                                        + " : " 
                                        + ent_array[m, j - 10].ToString().Replace(',', ' ')
                                    );
                                }
                            }
                    }
                }
                /* END OF CONVERTING ACCOUNT NUMBER EXCEL COLUMN TO LIST */

                corrAccountNumList.Value = string.Join(",", values.ToArray());

                //presenter.Import(fullPath, "Corrected", corrMessageRefId.ToString(), values);
                
                UploadPanel.Visible = true;
                UploadID = "Upload Success!";
                UploadPanel.CssClass = "alert alert-success user-status";
                UploadIcon.CssClass = "glyphicon glyphicon-ok-circle";

                presenter.LogAction("none", Path.GetFileName(FileUpload2.FileName).ToString(), "Corrected File Save", "Success");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Uploading error:" + ex.Message);
                UploadPanel.Visible = true;
                UploadID = "Upload Failed!";
                UploadPanel.CssClass = "alert alert-danger user-status";
                UploadIcon.CssClass = "glyphicon glyphicon-remove-circle";
                presenter.LogAction("none", Path.GetFileName(FileUpload2.FileName).ToString(), "Corrected File Save", "failed");
            }
        }
    }
}