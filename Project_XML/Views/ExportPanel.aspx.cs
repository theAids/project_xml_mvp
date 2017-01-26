using Project_XML.Models.EntityModels;
using Project_XML.Presenters.ExportPanel;
using Project_XML.Presenters.UserMenu_uc;
using Project_XML.Views.UserControls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

        public List<ConnectionStringModel> ConnStrings
        {
            set
            {
                db_connection.DataSource = value;
                db_connection.DataBind();
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
            presenter.exportXML(e.CommandArgument.ToString());

        }

        protected void ClearLog(object sender, EventArgs e)
        {
            presenter.ClearLogs();
        }

        protected void UnauthenticatedRedirect(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Login.aspx", true);
        }
    }
}