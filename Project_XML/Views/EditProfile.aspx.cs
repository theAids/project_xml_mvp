using Project_XML.Presenters.EditProfile;
using Project_XML.Presenters.UserMenu_uc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_XML.Views
{
    public partial class EditProfile : System.Web.UI.Page, IEditProfileView
    {
        private EditProfilePresenter presenter;

        public string Username
        {
            set { username_label.Text = value; }
        }
        public string Fname
        {
            set { fname_edit.Text = value; }
        }
        public string Lname
        {
            set { lname_edit.Text = value; }
        }
        public string Role
        {
            set; get;
        }
        public string editUserPanel
        {
            set { userEdit_status_panel.CssClass = value; }
        }
        public string editUserIcon
        {
            set { userEdit_status_icon.CssClass = value; }
        }
        public string editUserMsg
        {
            set { userEdit_status_lit.Text = value; }
        }
        public bool editUserPanel_vis
        {
            set { userEdit_status_panel.Visible = value; }
        }
        /******************** Code-behind **************************/

        public EditProfile()
        {
            presenter = new EditProfilePresenter(this);
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

            presenter.InitView(Page.IsPostBack, HttpContext.Current);
        }

        protected void Update_Info(object sender, EventArgs e)
        {
            Debug.WriteLine(fname_edit.Text);
            Debug.WriteLine("Role:" + Role);
            presenter.EditInfo(Page.IsValid, username_label.Text, fname_edit.Text, lname_edit.Text, pword1_edit.Text, Role);
        }

        protected void Validate_Password(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = presenter.ValidatePassword(username_label.Text, oldpasswd_edit.Text);
        }

        protected void UnauthenticatedRedirect(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Login.aspx", true);
        }

    }
}