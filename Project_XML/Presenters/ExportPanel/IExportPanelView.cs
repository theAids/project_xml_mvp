using Project_XML.Models.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_XML.Presenters.ExportPanel
{
    public interface IExportPanelView
    {
        string Username { get; }
        int[] YearList { set; }
        string LogMsg { set; get; }
        string LogPath { set; get; }
        string UploadID { set;}
        List<AccountModel> AccountsList { set; }
        //List<CorrAccountModel> CorrAccountsList { set; }
    }
}
