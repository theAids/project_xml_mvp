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

        public bool login_err
        {
            set { loginErr_msg.Visible = value; }
        }
        public bool rememberMe
        {
            get { return this.remChk.Checked; }
        }

        public HttpCookie cookie { set; get; }
        
        /***************** Code-Behind ********************/

        public Login()
        {
            presenter = new LoginPresenter(this);
            presenter.LoginSuccessRedirect += new EventHandler(LoginSuccessRedirect);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void loginbtn_Click(object sender, EventArgs e)
        {
            Page.Validate();
            presenter.ValidateUser(username.Text, pword.Text, Page.IsValid);
        }

        protected void LoginSuccessRedirect(object sender,EventArgs e)
        {
            Response.Cookies.Add(cookie);
            Response.Redirect("~/Views/ExportPanel.aspx", true);
        }
    }
}