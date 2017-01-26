using Project_XML.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_XML.Presenters.UserAdmin
{
    public interface IUserAdminView
    {
        List<UserAccountModel> Accounts { set; }
        string addUserPanel { set; }
        string addUserIcon { set; }
        string addUserMsg { set; }
        bool addUserPanel_vis { set; }
        string editUserPanel { set; }
        string editUserIcon { set; }
        string editUserMsg { set; }
        bool editUserPanel_vis { set; }
        string usernameLabel { set; }
        string CurrentUser { set; }
    }
}
