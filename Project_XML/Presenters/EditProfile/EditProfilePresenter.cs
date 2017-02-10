using Project_XML.Models.DbManager;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Project_XML.Presenters.EditProfile
{
    public class EditProfilePresenter
    {
        private IEditProfileView view;
        public event EventHandler UnauthenticatedRedirect;

        public EditProfilePresenter(IEditProfileView view)
        {
            if (view == null)
                throw new ArgumentNullException("View cannot be null.");
            this.view = view;
        }

        public void InitView(bool PageIsPostback, HttpContext con)
        {
            //get cookies
            FormsIdentity id = con.User.Identity as FormsIdentity;
            FormsAuthenticationTicket ticket = id.Ticket;

            if (!PageIsPostback)
            {
                //assign values
                view.UsernameEdit = ticket.Name;
                view.FnameEdit = ticket.UserData.Split('|')[0].Split(' ')[0];
                view.LnameEdit = ticket.UserData.Split('|')[0].Split(' ')[1];

            }

            view.Role = ticket.UserData.Split('|')[1]; // this needs to be reinitialized every load.
        }

        public void ValidateRequest(bool RequestIsValid)
        {
            if (!RequestIsValid)
                UnauthenticatedRedirect(this, null);
        }


        public void EditInfo(bool PageIsValid, string uname, string fname, string lname, string pword, string role)
        {
            if (PageIsValid)
            {

                DbAccountManager db = new DbAccountManager();
                bool userEdit;

                userEdit = db.EditUserInfo(HttpUtility.HtmlEncode(uname.Trim()), HttpUtility.HtmlEncode(fname.Trim()), HttpUtility.HtmlEncode(lname.Trim()), role);

                if (pword != null && !pword.Equals(""))
                    userEdit = db.ChangeUserPassword(uname, HttpUtility.HtmlEncode(pword.Trim()));

                view.EditUserPanel = userEdit ? "alert alert-success user-status" : "alert alert-danger user-status";
                view.EditUserIcon = userEdit ? "glyphicon glyphicon-ok-circle" : "glyphicon glyphicon-remove-circle";
                view.EditUserMsg = userEdit ? "User info updated!" : "Update failed!";

                view.EditUserPanel_vis = true;

            }
        }

        public bool ValidatePassword(string username, string pword)
        {
            DbAccountManager db = new DbAccountManager();
            string curPass = db.GetPassword(username);
            string salt = db.GetUserSalt(username);
            string oldPass = db.HashPassword(HttpUtility.HtmlEncode(pword.Trim()), salt);

            if (oldPass.Equals(curPass))
                return true;
            else
                return false;
        }
    }
}