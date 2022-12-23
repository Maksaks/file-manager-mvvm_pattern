using File_Manager.Control;
using File_Manager.Exception;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace File_Manager.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool _select;
        public bool select {
            get => _select;
            set
            {
                _select = value;
                CopyCommand?.RaiseCanExecuteChanged();
                CutCommand?.RaiseCanExecuteChanged();
                DeleteCommand?.RaiseCanExecuteChanged();
            }
        }
        private bool copied;
        private bool cut;
        private ObjectsVM copied_object;
        private FileSearcher searcher;
        private TabItemVM _CurTabItem;
        public ManagerMenuItems managerMenuItems;
        public ObservableCollection<TabItemVM> TabItems { get; set; }
        public ObservableCollection<MenuItemVM> MenuItems { get; set; }
        public TabItemVM CurTabItem { 
            get => _CurTabItem;
            set {
                _CurTabItem = value;
                OnPropertyChanged();
            } }
        /*ObservableCollection - коллекция с уведомлением об изменении*/

        public ICommand AddNewTabItemCommand { get; set; }
        public ICommand TabCloseCommand { get; } // закрыть вкладку
        public ICommand AddMenuItemCommand { get; }
        public DelegateCommand SearchCommand { get; }
        public DelegateCommand CopyCommand { get; }
        public DelegateCommand PasteCommand { get; }
        public DelegateCommand CutCommand { get; }
        public DelegateCommand AddFolderCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        public MainViewModel()
        {
            searcher = new FileSearcher();
            copied = false;
            cut = false;
            select = false;
            TabItems = new ObservableCollection<TabItemVM>();
            
            AddTabItemVM();
            TabCloseCommand = new DelegateCommand(TabClose);
            CurTabItem = TabItems.FirstOrDefault();
            AddNewTabItemCommand = new DelegateCommand(AddNewTabItem);
            managerMenuItems = new ManagerMenuItems(this);
            MenuItems = managerMenuItems.MenuItems;
            AddMenuItemCommand = managerMenuItems.AddMenuItemCommand;
            CopyCommand = new DelegateCommand(CopyOper, CanCopy);
            PasteCommand = new DelegateCommand(PasteOper, CanPaste);
            CutCommand = new DelegateCommand(CutOper, CanCut);
            AddFolderCommand = new DelegateCommand(AddFolder);
            DeleteCommand = new DelegateCommand(DeleteObj, CanDelete);
            searcher.ScanFiles();

            SearchCommand = new DelegateCommand(Searcher, CanSearch);


        }

        private bool CanDelete(object obj)
        {
            return select;
        }

        private void DeleteObj(object obj)
        {
            try
            {
                if (CurTabItem == null)
                {
                    throw new NoOpenTabs();
                }
                if (CurTabItem.SelectedOb.NAME.Split('\\').First().Length == 2) throw new NoAccesToDeleteDisk();
                try
                {
                    if (CurTabItem.SelectedOb is DirVM dir)
                    {
                        Directory.Delete(dir.FULLNANE, true);
                        searcher.RemoveObj(dir);
                        CurTabItem.ReloadTabItem(obj);
                    }
                    else if (CurTabItem.SelectedOb is FileVM file)
                    {
                        searcher.RemoveObj(file);
                        File.Delete(file.FULLNANE);
                        CurTabItem.ReloadTabItem(obj);
                    }
                }
                catch (System.Exception e)
                {
                    throw new NoAccesToDeleteDir();
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
        }

        private void AddFolder(object obj)
        {
            try
            {
                if (CurTabItem == null)
                {
                    throw new NoOpenTabs();
                }
                if (CurTabItem.CurrentPath == "Мій комп'ютер") throw new NoAccesToCreateDirInMainMenu();
                try
                {
                    InputBox inputBox = new InputBox("Введіть назву нового каталогу, не використовуючи символи \\|/:*?\"<>");
                    string name = inputBox.ShowDialog();
                    if (name.Contains('\\') || name.Contains('|') || name.Contains('/') || name.Contains(':') || name.Contains('*') ||
                    name.Contains('?') || name.Contains('\"') || name.Contains('<') || name.Contains('>')) throw new IncorrectNewNameFile();
                    Directory.CreateDirectory(Path.Combine(CurTabItem.CurrentPath, name));
                    searcher.AddElement(new DirVM(name, Path.Combine(CurTabItem.CurrentPath, name)));
                    CurTabItem.ReloadTabItem(obj);
                }

                catch (System.Exception e) 
                {
                    throw new NoAccesToCreateDir();
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
        }

        public void RaiseCopy()
        {
            select = true;
            CopyCommand?.RaiseCanExecuteChanged();
            CutCommand?.RaiseCanExecuteChanged();
            DeleteCommand?.RaiseCanExecuteChanged();
        }

        private bool CanCut(object obj)
        {
            return select;
        }

        private bool CanPaste(object obj)
        {
            return copied || cut;
        }

        private bool CanCopy(object obj)
        {
            return select;
        }

        private void CutOper(object obj)
        {
            try
            {
                if(CurTabItem == null)
                {
                    throw new NoOpenTabs();
                }
            }
            catch (FileManagerException e)
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (System.Exception e) // обработка вставки в неположенное место или переноса системной папки или диска
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            copied = false;
            cut = true;
            PasteCommand?.RaiseCanExecuteChanged();
            copied_object = CurTabItem.SelectedOb;
        }

        private void CopyFolder(string source, string target)
        {
            IEnumerable<string> dirs = Directory.EnumerateDirectories(source, "", SearchOption.AllDirectories);
            IEnumerable<string> files = Directory.EnumerateFiles(source, "", SearchOption.AllDirectories);

            try
            {
                Directory.CreateDirectory(target);
            }
            catch (System.Exception e)
            {
                throw new NoAccesToPasteDir();
            }

            foreach (string dir in dirs)
            {
                Directory.CreateDirectory(target + dir.Remove(dir.IndexOf(source), source.Length));
            }

            foreach (string file in files)
            {
                File.Copy(file, target + file.Remove(file.IndexOf(source), source.Length));
            }
        }

        private void CutFolder(string source, string target)
        {
            IEnumerable<string> dirs = Directory.EnumerateDirectories(source, "", SearchOption.AllDirectories);
            IEnumerable<string> files = Directory.EnumerateFiles(source, "", SearchOption.AllDirectories);

            try
            {
                Directory.CreateDirectory(target);
            }
            catch (System.Exception e)
            {
                throw new NoAccesToPasteDir();
            }
            

            foreach (string dir in dirs)
            {
                Directory.CreateDirectory(target + dir.Remove(dir.IndexOf(source), source.Length));
            }

            foreach (string file in files)
            {
                File.Copy(file, target + file.Remove(file.IndexOf(source), source.Length));
            }
        }

        private void PasteOper(object obj)
        {
            try
            {
                if (CurTabItem == null)
                {
                    throw new NoOpenTabs();
                }
                if (copied)
                {
                    if (copied_object is DirVM dir)
                    {
                        if (dir.NAME.Split('\\').First().Length == 2) throw new NoOperationsWithDisk();
                        if (Directory.Exists(Path.Combine(CurTabItem.CurrentPath, dir.NAME))) throw new DirAlreadyExsist();
                        CopyFolder(dir.FULLNANE, Path.Combine(CurTabItem.CurrentPath, dir.NAME));
                        searcher.AddElement(new DirVM(dir.NAME, Path.Combine(CurTabItem.CurrentPath, dir.NAME)));
                        CurTabItem.ReloadTabItem(obj);
                    }
                    else if (copied_object is FileVM file)
                    {
                        if (File.Exists(Path.Combine(CurTabItem.CurrentPath, file.NAME))) throw new FileAlreadyExsist();
                        try
                        {
                            File.Copy(file.FULLNANE, Path.Combine(CurTabItem.CurrentPath, file.NAME));
                        }
                        catch (System.Exception e)
                        {
                            throw new NoAccesToPasteFile();
                        }
                        searcher.AddElement(new FileVM(file.NAME, Path.Combine(CurTabItem.CurrentPath, file.NAME)));
                        CurTabItem.ReloadTabItem(obj);
                    }
                }
                else if (cut)
                {
                    if (copied_object is DirVM dir)
                    {
                        if (dir.NAME.Split('\\').First().Length == 2) throw new NoOperationsWithDisk();
                        if (Directory.Exists(Path.Combine(CurTabItem.CurrentPath, dir.NAME))) throw new DirAlreadyExsist();
                        if (CurTabItem.CurrentPath.Contains(copied_object.FULLNANE))
                        {
                            throw new OperationCutException();
                        }
                        try
                        {
                            Directory.CreateDirectory(Path.Combine(CurTabItem.CurrentPath, dir.NAME));
                        }
                        catch(System.Exception e)
                        {
                            throw new NoAccesToPasteFile();
                        }
                        CutFolder(dir.FULLNANE, Path.Combine(CurTabItem.CurrentPath, dir.NAME));
                        Directory.Delete(dir.FULLNANE, true);
                        searcher.ChangePath(dir, Path.Combine(CurTabItem.CurrentPath, dir.NAME));
                        if (MenuItems.Where(obj => obj.Header == dir.NAME) != null)
                        {
                            foreach (MenuItemVM item in MenuItems.Where(obj => obj.Header == dir.NAME).ToList())
                            {
                                MenuItemVM titem = new MenuItemVM(Path.Combine(CurTabItem.CurrentPath, dir.NAME));
                                MenuItems.Remove(item);
                                MenuItems.Add(titem);
                            }
                        }
                        CurTabItem.ReloadTabItem(obj);
                    }
                    else if (copied_object is FileVM file)
                    {
                        if (File.Exists(Path.Combine(CurTabItem.CurrentPath, file.NAME))) throw new FileAlreadyExsist();
                        try
                        {
                            File.Move(file.FULLNANE, Path.Combine(CurTabItem.CurrentPath, file.NAME));
                        }
                        catch (System.Exception e)
                        {
                            throw new NoAccesToPasteDir();
                        }
                        
                        File.Delete(file.FULLNANE);
                        searcher.ChangePath(file, Path.Combine(CurTabItem.CurrentPath, file.NAME));
                        CurTabItem.ReloadTabItem(obj);
                    }
                }
            }
            catch (FileManagerException e)
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception e) // обработка вставки в неположенное место или переноса системной папки или диска
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            copied = false;
            cut = false;
            PasteCommand.RaiseCanExecuteChanged();
        }

        private void CopyOper(object obj)
        {
            try
            {
                if (CurTabItem == null)
                {
                    throw new NoOpenTabs();
                }
            }
            catch (FileManagerException e)
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (System.Exception e) // обработка вставки в неположенное место или переноса системной папки или диска
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            copied = true;
            cut = false;
            PasteCommand?.RaiseCanExecuteChanged();
            copied_object = CurTabItem.SelectedOb;
        }

        public void AddNewTabItem(object obj)
        {
            AddTabItemVM();
        }

        private void TabClose(object obj)
        {
            if (obj is TabItemVM tabItemVM)
            {
                TabItems.Remove(tabItemVM);
                CurTabItem = TabItems.LastOrDefault();
            }
        }

        private void AddTabItemVM()
        {
            TabItemVM tabItemVM = new TabItemVM(this, searcher);
            TabItems.Add(tabItemVM);
            CurTabItem = tabItemVM;
        }

        private bool CanSearch(object obj)
        {
            return searcher.ScanFilesEnd;
        }

        private void Searcher(object obj)
        {
            try
            {
                if (CurTabItem == null)
                {
                    throw new NoOpenTabs();
                }
            }
            catch (FileManagerException e)
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (System.Exception e) // обработка вставки в неположенное место или переноса системной папки или диска
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            SeacrhBox box = new SeacrhBox();
            string pattern = box.ShowDialog();
            if (pattern == "") return;
            CurTabItem.CurrentDir.Clear();
            try
            {
                IEnumerable<ObjectsVM> list = searcher.GetFiles(pattern);
                if (list != null)
                {
                    foreach (ObjectsVM ent in list)
                    {
                        CurTabItem.CurrentDir.Add(ent);
                    }

                    CurTabItem.CurrentPath = $"Результати пошуку по запиту: {pattern}";
                    CurTabItem.TabName = $"Пошук: {pattern}";
                }
                else
                {
                    throw new EmptySearchResult();
                }
            }
            catch (FileManagerException e)
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (System.Exception e) // обработка вставки в неположенное место или переноса системной папки или диска
            {
                MessageBox.Show(e.Message, "ПОМИЛКА", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
