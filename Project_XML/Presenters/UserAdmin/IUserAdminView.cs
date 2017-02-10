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
        string AddUserPanel { set; }
        string AddUserIcon { set; }
        string AddUserMsg { set; }
        bool AddUserPanel_vis { set; }
        string EditUserPanel { set; }
        string EditUserIcon { set; }
        string EditUserMsg { set; }
        bool EditUserPanel_vis { set; }
        string Username { set; }
        string CurrentUser { set; }
    }
}
