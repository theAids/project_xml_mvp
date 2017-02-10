using Project_XML.Models.DbManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Project_XML.Presenters.UserAdmin
{
    public class UserAdminPresenter
    {
        private IUserAdminView view;
        public event EventHandler UnauthenticatedRedirect;

        public UserAdminPresenter(IUserAdminView view)
        {
            if (view == null)
                throw new ArgumentNullException("View cannot be null.");
            this.view = view;
        }

        public void InitView(bool PageIsPostback, HttpContext con)
        {
            if (!PageIsPostback)
            {
                FormsIdentity id = con.User.Identity as FormsIdentity;
                FormsAuthenticationTicket ticket = id.Ticket;
                view.CurrentUser = ticket.Name;
 
                PopulateUserTable();
            }
        }

        public void ValidateRequest(bool RequestIsValid)
        {
            if (!RequestIsValid)
                UnauthenticatedRedirect(this, null);
        }

        public void PopulateUserTable()
        {
            DbAccountManager db = new DbAccountManager();
            view.Accounts = db.GetAllUsers();
        }

        public void AddUser(bool PageIsValid, string uname, string fname, string lname, string pword, string role)
        {
            if (PageIsValid)
            {

                switch (role)
                {
                    case "1": role = "Administrator"; break;
                    case "2": role = "User"; break;
                }
                DbAccountManager db = new DbAccountManager();

                bool userAdd = db.AddUser(HttpUtility.HtmlEncode(uname.Trim()), HttpUtility.HtmlEncode(fname.Trim()), HttpUtility.HtmlEncode(lname.Trim()), HttpUtility.HtmlEncode(pword.Trim()), role);
                view.AddUserPanel = userAdd ? "alert alert-success user-status" : "alert alert-danger user-status";
                view.AddUserIcon = userAdd ? "glyphicon glyphicon-ok-circle" : "glyphicon glyphicon-remove-circle";
                view.AddUserMsg = userAdd ? "User successfully added!" : "Adding user failed!";

                view.AddUserPanel_vis = true;
                PopulateUserTable();
            }
        }

        public bool UserExists(string uname)
        {
            DbAccountManager db = new DbAccountManager();
            if (db.GetUsername(uname.Trim()) == null)
                return true;
            else
                return false;
        }

        public void RemoveUser(long userId)
        {
            DbAccountManager db = new DbAccountManager();
            db.RemoveUser(userId);
            PopulateUserTable();
        }

        public void EditUser(bool PageIsValid, string uname, string fname, string lname, string pword, string role)
        {
            if (PageIsValid)
            {

                switch (role)
                {
                    case "1": role = "Administrator"; break;
                    case "2": role = "User"; break;
                }
                DbAccountManager db = new DbAccountManager();
                bool userEdit;

                
                userEdit = db.EditUserInfo(HttpUtility.HtmlEncode(uname.Trim()), HttpUtility.HtmlEncode(fname.Trim()), HttpUtility.HtmlEncode(lname.Trim()), role);

                if(pword != null && !pword.Equals(""))
                    userEdit = db.ChangeUserPassword(uname, HttpUtility.HtmlEncode(pword.Trim()));

                view.EditUserPanel = userEdit ? "alert alert-success user-status" : "alert alert-danger user-status";
                view.EditUserIcon = userEdit ? "glyphicon glyphicon-ok-circle" : "glyphicon glyphicon-remove-circle";
                view.EditUserMsg = userEdit ? "User info updated!" : "Update failed!";

                view.EditUserPanel_vis = true;
                view.Username = uname;
                PopulateUserTable();
            }
        }

    }
}