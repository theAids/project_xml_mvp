using Project_XML.Models.EntityModels;
using Project_XML.Presenters.UserAdmin;
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
    public partial class UserAdmin : System.Web.UI.Page, IUserAdminView
    {
        private UserAdminPresenter presenter;

        public List<UserAccountModel> Accounts
        {
            set
            {
                userList.DataSource = value;
                userList.DataBind();
            }
        }

        public string AddUserPanel
        {
            set { addUserPanel.CssClass = value; }
        }
        public string AddUserIcon
        {
            set { addUsericon.CssClass = value; }
        }
        public string AddUserMsg
        {
            set { addUserMsg.Text = value; }
        }
        public bool AddUserPanel_vis
        {
            set { addUserPanel.Visible = value;}
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
        public string Username
        {
            set { usernameEdit.Text = value; }
        }
        public string CurrentUser
        {
            set { currentUser.Value = value; }
        }
        /***************** Code-behind**********************/

        public UserAdmin()
        {
            presenter = new UserAdminPresenter(this);
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

        protected void AddUser(object sender, EventArgs e)
        {
            usernameValidate.Validate();
            presenter.AddUser(Page.IsValid, usernameAdd.Text, fnameAdd.Text, lnameAdd.Text, pwordAdd1.Text, roleAdd.SelectedItem.Value);
            userTableUpdate.Update();
        }

        protected void EditUser(object sender, EventArgs e)
        {
            //Debug.WriteLine("Edit Pword:"+pword1_edit.Text);
            presenter.EditUser(Page.IsValid, usernameVal.Value.ToString(), fnameEdit.Text, lnameEdit.Text, pwordEdit1.Text, roleEdit.SelectedItem.Value);
            userTableUpdate.Update();
        }

        protected void RemoveUser(object sender, CommandEventArgs e)
        {
            presenter.RemoveUser(Convert.ToInt64(e.CommandArgument));
            userTableUpdate.Update();
        }
        
        protected void UserExists(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = presenter.UserExists(usernameAdd.Text);
        }

        protected void UnauthenticatedRedirect(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Login.aspx", true);
        }

    }
}