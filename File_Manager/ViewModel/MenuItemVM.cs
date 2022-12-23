using System.Collections.Generic;
using System.Windows.Input;

namespace File_Manager.ViewModel
{
    public class MenuItemVM : BaseViewModel
    {
        public string Header { get; set; }
        public string FULLPATH { get; set; }
        public ICommand ItemCommand { get; set; }
        public object CommandParameter { get; set; }

        public string IcoPath { get; set; }
        public MenuItemVM(string fpath)
        {
            FULLPATH = fpath;

            CommandParameter = FULLPATH;
        }
    }
}