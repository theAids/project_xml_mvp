using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Project_XML.Presenters.UserMenu_uc
{
    public class UserMenuPresenter
    {
        private IUserMenuView view;

        public UserMenuPresenter(IUserMenuView view)
        {
            if (view == null)
                throw new ArgumentNullException("View cannot be null.");
            this.view = view;
        }

        public void RenderMenu(HttpContext cur)
        {
            // get cookies
            FormsIdentity id = cur.User.Identity as FormsIdentity;
            FormsAuthenticationTicket ticket = id.Ticket;

            // access logged in name Literal in Master page
            view.CurrentUser = ticket.UserData.Split('|')[0];

            switch (ticket.UserData.Split('|')[1])
            {
                case "Administrator":
                    view.SideMenu = new List<string>()
                            {
                                "<li id='exportPanel'><a href='ExportPanel.aspx'><span class='glyphicon glyphicon-folder-close icon'></span>Export XML<span class='sr-only'>(current)</span></a></li>",
                                "<li id='tableauPanel'><a href='#'><span class='glyphicon glyphicon-signal icon'></span>Tableau</a></li>",
                                "<li id='userAdminPanel'><a href='../Views/UserAdmin.aspx'><span class='glyphicon glyphicon-user icon'></span>User Admin</a></li>"
                            };
                    break;
                case "User":
                    view.SideMenu = new List<string>()
                            {
                                "<li id='exportPanel'><a href='ExportPanel.aspx'><span class='glyphicon glyphicon-folder-close icon'></span>Export XML<span class='sr-only'>(current)</span></a></li>",
                                "<li id='tableauPanel'><a href='#'><span class='glyphicon glyphicon-signal icon'></span>Tableau</a></li>"
                            };
                    break;
            }
        }
    }
}