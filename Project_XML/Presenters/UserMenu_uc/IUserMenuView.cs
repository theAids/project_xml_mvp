using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_XML.Presenters.UserMenu_uc
{
    public interface IUserMenuView
    {
        string UserName { set;}
        List<string> SideMenu { set; }
        void AttachPresenter(UserMenuPresenter presenter);
    }
}
