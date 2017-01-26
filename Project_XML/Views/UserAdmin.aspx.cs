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

        public string addUserPanel
        {
            set { userAdd_status_panel.CssClass = value; }
        }
        public string addUserIcon
        {
            set { userAdd_status_icon.CssClass = value; }
        }
        public string addUserMsg
        {
            set { userAdd_status_lit.Text = value; }
        }
        public bool addUserPanel_vis
        {
            set { userAdd_status_panel.Visible = value;}
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
        public string usernameLabel
        {
            set { username_label.Text = value; }
        }
        public string CurrentUser
        {
            set { current_user.Value = value; }
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

        protected void Add_User(object sender, EventArgs e)
        {
            unameVal.Validate();
            presenter.AddUser(Page.IsValid, uname.Text, fname.Text, lname.Text, pword1.Text, roleList.SelectedItem.Value);
            userTableUpdate.Update();
        }

        protected void Edit_User(object sender, EventArgs e)
        {
            Debug.WriteLine("Edit Pword:"+pword1_edit.Text);
            presenter.EditUser(Page.IsValid, hidden.Value.ToString(), fname_edit.Text, lname_edit.Text, pword1_edit.Text, roleList_edit.SelectedItem.Value);
            userTableUpdate.Update();
        }

        protected void Remove_User(object sender, CommandEventArgs e)
        {
            presenter.RemoveUser(Convert.ToInt64(e.CommandArgument));
            userTableUpdate.Update();
        }
        
        protected void User_Exists(object sender, ServerValidateEventArgs e)
        {
            e.IsValid = presenter.UserExists(uname.Text);
        }

        protected void UnauthenticatedRedirect(object sender, EventArgs e)
        {
            Response.Redirect("~/Views/Login.aspx", true);
        }

    }
}