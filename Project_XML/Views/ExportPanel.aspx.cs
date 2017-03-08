using Project_XML.Models.EntityModels;
using Project_XML.Presenters.ExportPanel;
using Project_XML.Presenters.UserMenu_uc;
using Project_XML.Views.UserControls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

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
                { "msgSpecType", "CRS701"},
                { "contact", newContact.Text},
                { "attentionNote", newAttentionNote.Text},
                { "docSpecType", "OECD1" }
            };

            object[] obj = presenter.exportXML(accountSelected.Value, reportArgs);

            if (obj != null)
            {
                XmlDocument xmlDoc = (XmlDocument)obj[0];
                Debug.WriteLine("XML Document Name: " + obj[1].ToString());
                byte[] bytes = Encoding.Default.GetBytes(xmlDoc.OuterXml);
                Response.Buffer = true;
                Response.Clear();
                Response.ContentType = "text/xml";
                Response.AddHeader("content-disposition", String.Format("attachment; filename={0}.xml", obj[1].ToString()));
                Response.BinaryWrite(bytes);
                Response.End();
                Response.Flush();
            }
        }
    }
}