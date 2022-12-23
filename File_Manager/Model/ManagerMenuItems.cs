using File_Manager.Exception;
using File_Manager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace File_Manager.ViewModel
{
    public class ManagerMenuItems : BaseViewModel
    {
        private string DataFileName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Data\MenuItems.json"); 

        private MainViewModel mainView;
        public ICommand OpenMenuItemCommand { get; }

        private List<MenuItem> items;

        public ICommand AddMenuItemCommand { get; }
        public ObservableCollection<MenuItemVM> MenuItems { get; set; }
        





        public ManagerMenuItems(MainViewModel mainViewModel)
        {
            mainView = mainViewModel;
            OpenMenuItemCommand = new DelegateCommand(OpenMenuItem);
            items = GetMenuItemsFromFile();
            MenuItems = MakeMenuItems(items);
            AddMenuItemCommand = new DelegateCommand(AddMenuItem);
        }

        private void AddMenuItem(object obj)
        {
            string fpath = obj as string;

            if (fpath != null && Directory.Exists(fpath))
            {
                items.Add(new MenuItem
                {
                    FULLPATH = fpath
                });
                try
                {
                    string fjson = JsonSerializer.Serialize(items);
                    File.WriteAllText(DataFileName, fjson);
                }
                catch (System.Exception e)
                {

                }
                MenuItems.Clear();
                foreach(MenuItemVM vm in MakeMenuItems(items))
                {
                    MenuItems.Add(vm);
                }
            }
        }

        private ObservableCollection<MenuItemVM> MakeMenuItems(IList<MenuItem> mitems)
        {
            ObservableCollection<MenuItemVM> menuItems = new ObservableCollection<MenuItemVM>();

            if (mitems == null || !mitems.Any())
            {
                return menuItems;
            }
            foreach (MenuItem item in mitems)
            {
                string fpath = item.FULLPATH;

                MenuItemVM it = new MenuItemVM(item.FULLPATH);

                if (fpath == null)
                {
                    it.Header = item.Name;
                    it.IcoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "folder-text-outline.svg");
                }
                else
                {
                    
                    try
                    {
                        it.ItemCommand = OpenMenuItemCommand;
                        if (File.GetAttributes(fpath).HasFlag(FileAttributes.Directory)) // is it dir?
                        {
                            it.Header = new DirectoryInfo(fpath).Name;
                            it.IcoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Icon", "folder.svg");
                        }
                    }
                    catch(System.Exception e)
                    {
                        continue;
                    }
                }

                menuItems.Add(it);
            }

            return menuItems;
        }

        private List<MenuItem> GetMenuItemsFromFile()
        {
            try
            {
                if (File.Exists(DataFileName))
                {
                    string ftext = File.ReadAllText(DataFileName);

                    try
                    {
                        return JsonSerializer.Deserialize<List<MenuItem>>(ftext);
                    }
                    catch (System.Exception e)
                    {

                    }
                }
                else
                {
                    throw new NoFileMenuItemsExsist(DataFileName);
                }
            }
            catch (FileManagerException e)
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception e)
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }


            return new List<MenuItem>();
        }

        private void OpenMenuItem(object obj)
        {
            if (obj is string itempath)
            {
                if (mainView.CurTabItem == null)
                {
                    mainView.AddNewTabItem(obj);
                }
                mainView.CurTabItem.OpenMenuItem(itempath);
                mainView.CurTabItem.TabName = itempath.Split('\\').Last();
            }
        }
    }
}
