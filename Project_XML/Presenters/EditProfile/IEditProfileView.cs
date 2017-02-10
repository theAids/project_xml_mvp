using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_XML.Presenters.EditProfile
{
    public interface IEditProfileView
    {
        string UsernameEdit { set; }
        string FnameEdit { set; }
        string LnameEdit { set; }
        string Role { set; get; }
        string EditUserPanel { set; }
        string EditUserIcon { set; }
        string EditUserMsg { set; }
        bool EditUserPanel_vis { set; }
    }
}
