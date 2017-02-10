using Project_XML.Models.EntityModels;
using Project_XML.Presenters.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_XML.Views
{
    public partial class Login : System.Web.UI.Page, ILoginView
    {
        private LoginPresenter presenter;

        public bool loginErrorPanel_vis
        {
            set { loginErrorPanel.Visible = value; }
        }

        public HttpCookie Cookie { set; get; }
        
        /***************** Code-Behind ********************/

        public Login()
        {
            presenter = new LoginPresenter(this);
            presenter.LoginSuccessRedirect += new EventHandler(LoginSuccessRedirect);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void LoginUser(object sender, EventArgs e)
        {
            Page.Validate();
            presenter.ValidateUser(username.Text, pword.Text, rememberMe.Checked, Page.IsValid);
        }

        protected void LoginSuccessRedirect(object sender,EventArgs e)
        {
            Response.Cookies.Add(Cookie);
            Response.Redirect("~/Views/ExportPanel.aspx", true);
        }
    }
}