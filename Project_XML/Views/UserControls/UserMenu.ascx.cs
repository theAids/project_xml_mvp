using Project_XML.Presenters.UserMenu_uc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Project_XML.Views.UserControls
{
    public partial class UserMenu : System.Web.UI.UserControl, IUserMenuView
    {
        private UserMenuPresenter presenter;

        public string UserName
        {
            set
            {
                currentUser.Text = value;
            }
        }

        public List<string> SideMenu
        {
            set
            {
                foreach (string str in value)
                {
                    sideMenu.Controls.Add(new LiteralControl(str));
                }
            }
        }

        public void AttachPresenter(UserMenuPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException("Presenter cannot be null.");
            this.presenter = presenter;
        }

        /****************** Code-Behind **********************/

        protected void logout_btn_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}