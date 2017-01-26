using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_XML.Presenters.EditProfile
{
    public interface IEditProfileView
    {
        string Username { set; }
        string Fname { set; }
        string Lname { set; }
        string Role { set; get; }
        string editUserPanel { set; }
        string editUserIcon { set; }
        string editUserMsg { set; }
        bool editUserPanel_vis { set; }
    }
}
