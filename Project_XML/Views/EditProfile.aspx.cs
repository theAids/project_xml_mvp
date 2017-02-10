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

        public string UsernameEdit
        {
            set { usernameEdit.Text = value; }
        }
        public string FnameEdit
        {
            set { fnameEdit.Text = value; }
        }
        public string LnameEdit
        {
            set { lnameEdit.Text = value; }
        }
        public string Role
        {
            set; get;
        }
        public string EditUserPanel
        {
            set { editUserPanel.CssClass = value; }
        }
        public string EditUserIcon
        {
            set { editUserIcon.CssClass = value; }
        }
        public string EditUserMsg
        {
            set { editUserMsg.Text = value; }
        }
        public bool EditUserPanel_vis
        {
            set { editUserPanel.Visible = value; }
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
            Debug.WriteLine(fnameEdit.Text);
            Debug.WriteLine("Role:" + Role);
            presenter.EditInfo(Page.IsValid, usernameEdit.Text, fnameEdit.Text, lnameEdit.Text, pwordEdit1.Text, Role);
        }

        protected void Validate_Password(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = presenter.ValidatePassword(usernameEdit.Text, oldPwordEdit.Text);
        }

        protected void UnauthenticatedRedirect(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Login.aspx", true);
        }

    }
}