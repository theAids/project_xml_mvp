using Project_XML.Models.DbManager;
using Project_XML.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Project_XML.Presenters.Login
{
    public class LoginPresenter
    {
        private ILoginView view;
        public event EventHandler LoginSuccessRedirect;

        public LoginPresenter(ILoginView view)
        {
            if (view == null)
                throw new ArgumentNullException("View cannot be null.");
            this.view = view;
        }

        public void InitView()
        {

        }

        public void ValidateUser(string username, string pword, bool rememberMe, bool isValid)
        {
            if (!isValid)
                return;

            DbAccountManager db = new DbAccountManager();
            UserAccountModel user = db.ValidateUser(HttpUtility.HtmlEncode(username.Trim()), HttpUtility.HtmlEncode(pword.Trim()));

            if (user != null)
            {
                HttpCookie authCookie = FormsAuthentication.GetAuthCookie(user.username, false);
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);

                FormsAuthenticationTicket newTicket = new FormsAuthenticationTicket(ticket.Version
                                                                                   , ticket.Name
                                                                                   , ticket.IssueDate
                                                                                   , ticket.Expiration
                                                                                   , rememberMe
                                                                                   , string.Format("{0} {1}|{2}", user.firstname, user.lastname, user.role));
                authCookie.Value = FormsAuthentication.Encrypt(newTicket);

                if (rememberMe)
                    authCookie.Expires = ticket.Expiration;
                view.Cookie = authCookie;

                LoginSuccessRedirect(this, null);
            }
            else
            {
                view.loginErrorPanel_vis = true;
                return;
            }
        }
    }
}