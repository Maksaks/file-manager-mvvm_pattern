using File_Manager.Control;
using File_Manager.Exception;
using File_Manager.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace File_Manager.ViewModel
{
    public class TabItemVM : BaseViewModel
    {
        private MainViewModel _parent;
        public MainViewModel parent
        {
            get => _parent;
            set
            {
                _parent = value;
            }
        }
        private string _CurrentPath;
        private string _TabName;
        private ObjectsVM _SelectedOb;
        private HistoryDir historyDirs;
        private BackgroundWorker worker;
        public FileSearcher searcher;

        public ObjectsVM SelectedOb {
            get => _SelectedOb;
            set { _SelectedOb = value;
                if (SelectedOb == null) { IsSelected = false; } else { IsSelected = true; }
                 OnPropertyChanged(); GetInfoSelectItem(); parent.RaiseCopy(); RenameCommand?.RaiseCanExecuteChanged(); } }
        public string CurrentPath
        {
            get => _CurrentPath;
            set { _CurrentPath = value; OnPropertyChanged(); }
        }
        public string TabName
        {
            get => _TabName;
            set { _TabName = value; OnPropertyChanged(); }
        }
        private bool _IsSelected;
        public bool IsSelected
        {
            get => _IsSelected;
            set
            {
                _IsSelected = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<ObjectsVM> CurrentDir { get; set; }
        public ObservableCollection<string> SelectObjInfo { get; set; }
        /*ObservableCollection - коллекция с уведомлением об изменении*/


        // КОМАНДЫ
        public ICommand GoINCommand { get; } // заход в папку

        public DelegateCommand PreviousCommand { get; }
        public DelegateCommand NextCommand { get; }
        public ICommand GoHomeCommand { get; }
        public ICommand ReloadCommand { get; }
        public DelegateCommand RenameCommand { get; }
        public ICommand SortCollectionCommand { get; }
        public ICommand FillterCollectionCommand { get; }


        public TabItemVM(MainViewModel par, FileSearcher searcher)
        {
            this.searcher = searcher;
            parent = par;
            worker = new BackgroundWorker()
            {
                WorkerReportsProgress = true,
                WorkerSupportsCancellation = true
            };

            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            CurrentDir = new ObservableCollection<ObjectsVM>();
            GoINCommand = new DelegateCommand(GoIN);
            historyDirs = new HistoryDir("Мій комп'ютер", "Мій комп'ютер");
            SelectObjInfo = new ObservableCollection<string>();
            PreviousCommand = new DelegateCommand(PrevDir, CanPrev);
            NextCommand = new DelegateCommand(NextDir, CanNext);
            GoHomeCommand = new DelegateCommand(GoHome);
            ReloadCommand = new DelegateCommand(ReloadTabItem);
            RenameCommand = new DelegateCommand(RenameObj, CanRename);
            SortCollectionCommand = new DelegateCommand(SortCollection);
            FillterCollectionCommand = new DelegateCommand(FillterCollection);

            TabName = historyDirs.currentDir.NamePath;
            CurrentPath = historyDirs.currentDir.FullPath;

            OpenCurrentDir();
            SelectedOb = CurrentDir.First();
            historyDirs.HistChanged += HistoryChanged;
        }

        private void GetInfoSelectItem()
        {
            SelectObjInfo.Clear();
            if (SelectedOb is DirVM dir)
            {
                if (dir.NAME.Split('\\').Length == 2) // disk
                {
                    DriveInfo disk = new DriveInfo(dir.FULLNANE);
                    SelectObjInfo.Add($"Ім'я диску: {disk.Name}");
                    SelectObjInfo.Add($"Ім'я файлової системи: {disk.DriveFormat}");
                    SelectObjInfo.Add($"Тип диску: {disk.DriveType}");
                    
                    long free = disk.TotalFreeSpace / 1000000000;
                    long size = disk.TotalSize / 1000000000;
                    long avail = size - disk.AvailableFreeSpace / 1000000000;
                    SelectObjInfo.Add($"Зайнятий об'єм диску: {avail, 5} ГБ");
                    SelectObjInfo.Add($"Вільний об'єм диску: {free, 5} ГБ");
                    SelectObjInfo.Add($"Загальний об'єм диску: {size, 5} ГБ");
                }
                else // folder
                {
                    DirectoryInfo dirr = new DirectoryInfo(dir.FULLNANE);
                    SelectObjInfo.Add($"Ім'я каталогу: {dirr.Name}");
                    SelectObjInfo.Add($"Шлях до каталогу: {dirr.FullName}");
                    SelectObjInfo.Add($"Час створення каталогу: {dirr.CreationTimeUtc}");
                    SelectObjInfo.Add($"Час останнього доступу до каталогу: {dirr.LastAccessTimeUtc}");
                    SelectObjInfo.Add($"Час останнього запису до каталогу: {dirr.LastWriteTimeUtc}");
                }
            }
            else if (SelectedOb is FileVM file)
            {
                FileInfo fil = new FileInfo(file.FULLNANE);
                SelectObjInfo.Add($"Ім'я файлу: {fil.Name}");
                long filelen = fil.Length;
                string ed = "Б";
                if (filelen/1000 > 0)
                {
                    filelen /= 1000;
                    ed = "КБ";
                }
                if (filelen / 1000 > 0)
                {
                    filelen /= 1000;
                    ed = "МБ";
                }
                SelectObjInfo.Add($"Розмір файлу: {filelen, 3} {ed}");
                SelectObjInfo.Add($"Шлях до файлу: {fil.FullName}");
                SelectObjInfo.Add($"Розширення файлу: {fil.Extension}");
                if (fil.IsReadOnly)
                {
                    SelectObjInfo.Add($"Доступ до файлу: Лише для читання");
                }
                else
                {
                    SelectObjInfo.Add($"Доступ до файлу: Необмежений");
                }
                
                SelectObjInfo.Add($"Час створення файлу: {fil.CreationTimeUtc}");
                SelectObjInfo.Add($"Час останнього доступу до файлу: {fil.LastAccessTimeUtc}");
                SelectObjInfo.Add($"Час останнього запису до файлу: {fil.LastWriteTimeUtc}");
            }
        }
        private void FillterCollection(object obj)
        {
            
            string type_filter = obj as string;
            List<ObjectsVM> curdir = CurrentDir.ToList();
            if (type_filter == "Файли .docx")
            {
                CurrentDir.Clear();
                foreach(ObjectsVM objectt in curdir.Where(obj => obj.NAME.ToLower().Contains(".docx")))
                {
                    CurrentDir.Add(objectt);
                }
            }
            else if (type_filter == "Файли .py")
            {
                CurrentDir.Clear();
                foreach (ObjectsVM objectt in curdir.Where(obj => obj.NAME.ToLower().Contains(".py")))
                {
                    CurrentDir.Add(objectt);
                }
            }
            else if (type_filter == "Файли .cs")
            {
                CurrentDir.Clear();
                foreach (ObjectsVM objectt in curdir.Where(obj => obj.NAME.ToLower().Contains(".cs")))
                {
                    CurrentDir.Add(objectt);
                }
            }
            else if (type_filter == "Користувацький")
            {
                InputBox input = new InputBox("Введіть фільтр для файлів");
                string filter = input.ShowDialog().ToLower();
                if (filter == "") // вернуть папки и диски
                {
                    CurrentDir.Clear();
                    foreach (ObjectsVM objectt in curdir.Where(obj => obj is DirVM))
                    {
                        CurrentDir.Add(objectt);
                    }
                }
                else if (filter.StartsWith('.'))
                {
                    CurrentDir.Clear();
                    foreach (ObjectsVM objectt in curdir.Where(obj => obj.NAME.ToLower().Contains(filter)))
                    {
                        CurrentDir.Add(objectt);
                    }
                }
                else if (!filter.StartsWith('.'))
                {
                    CurrentDir.Clear();
                    foreach (ObjectsVM objectt in curdir.Where(obj => obj.NAME.ToLower().Contains("." + filter)))
                    {
                        CurrentDir.Add(objectt);
                    }
                }
            }
            try
            {
                if (CurrentDir.Count == 0)
                {
                    OpenCurrentDir();
                    throw new EmptyFilterhResult();
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

        private void SortCollection(object obj)
        {
            string type_sort = obj as string;
            if (type_sort == "За часом")
            {
                TimeSortCollection();
            }
            else if (type_sort == "За типом")
            {
                TypeSortCollection();
            }
            else if (type_sort == "За ім'ям")
            {
                NameSortCollection();
            }
        }

        private void NameSortCollection()
        {
            List<Tuple<string, ObjectsVM>> tuple = new List<Tuple<string, ObjectsVM>>();
            foreach (ObjectsVM obj in CurrentDir)
            {
                tuple.Add(new Tuple<string, ObjectsVM>(obj.NAME.Split('.').First(), obj));
            }
            CurrentDir.Clear();
            foreach (Tuple<string, ObjectsVM> objj in tuple.OrderBy(obj => obj.Item1))
            {
                CurrentDir.Add(objj.Item2);
            }
        }

        private void TypeSortCollection()
        {
            List<Tuple<string, ObjectsVM>> tuple = new List<Tuple<string, ObjectsVM>>();
            foreach (ObjectsVM obj in CurrentDir)
            {
                tuple.Add(new Tuple<string, ObjectsVM>(obj.NAME.Split('.').Last(), obj));
            }
            CurrentDir.Clear();
            foreach (IGrouping<string, Tuple<string, ObjectsVM>> obj in tuple.GroupBy(obj => obj.Item1))
            {
                foreach (Tuple<string, ObjectsVM> objj in obj)
                {
                    CurrentDir.Add(objj.Item2);
                }
            }
        }

        private void TimeSortCollection()
        {
            List<Tuple<DateTime, ObjectsVM>> tuple = new List<Tuple<DateTime, ObjectsVM>>();
            foreach (ObjectsVM obj in CurrentDir)
            {
                if (obj is DirVM dir)
                {
                    tuple.Add(new Tuple<DateTime, ObjectsVM>(Directory.GetLastAccessTimeUtc(dir.FULLNANE), obj));
                }
                else if (obj is FileVM file)
                {
                    tuple.Add(new Tuple<DateTime, ObjectsVM>(File.GetLastAccessTimeUtc(file.FULLNANE), obj));
                }
            }
            CurrentDir.Clear();
            foreach (Tuple<DateTime, ObjectsVM> objj in tuple.OrderBy(obj => obj.Item1))
            {
                CurrentDir.Add(objj.Item2);
            }
        }

        private bool CanRename(object obj)
        {
            if (SelectedOb != null)
            {
                return true;
            }
            return false;
        }

        private void RenameObj(object obj)
        {
            try
            {
                if (SelectedOb.NAME.Split('\\').First().Length == 2) throw new NoAccesToRenameDisk();
                InputBox inputBox = new InputBox("Введіть нове ім'я, не використовуючи символи \\|/:*?\"<>");
                string nname = inputBox.ShowDialog(); 
                if (nname.Contains('\\') || nname.Contains('|') || nname.Contains('/') || nname.Contains(':') || nname.Contains('*') ||
                    nname.Contains('?') || nname.Contains('\"') || nname.Contains('<') || nname.Contains('>')) throw new IncorrectNewNameFile();
                if (nname != "")
                {
                    if (SelectedOb is DirVM dir)
                    {
                        string oldpath = dir.FULLNANE;
                        string npath = Path.Combine(dir.FULLNANE.Remove(dir.FULLNANE.IndexOf(dir.NAME)), nname);
                        try
                        {
                            Directory.Move(dir.FULLNANE, npath);
                        }
                        catch(System.Exception e)
                        {
                            throw new NoAccesToRenameDir();
                        }
                        searcher.ChangeNameAndPath(dir, npath);
                        searcher.ChangePathRecursive(oldpath, npath);
                        OpenCurrentDir();
                    }
                    else if (SelectedOb is FileVM file)
                    {
                        string npath = Path.Combine(file.FULLNANE.Remove(file.FULLNANE.IndexOf(file.NAME)), nname + "." + file.NAME.Split('.').Last());
                        try
                        {
                            File.Move(file.FULLNANE, npath);
                        }
                        catch (System.Exception e)
                        {
                            throw new NoAccesToRenameFile();
                        }
                        searcher.ChangePath(file, npath);
                        OpenCurrentDir();
                    }
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

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled) // когда прервали старый, запускаем новый
            {
                Run_Worker();
            }

            SelectedOb = CurrentDir.FirstOrDefault();
        }

        private void HistoryChanged(object sender, EventArgs e)
        {
            PreviousCommand?.RaiseCanExecuteChanged();
            NextCommand?.RaiseCanExecuteChanged();
        }

        private void OpenCurrentDir()
        {
            if (worker.IsBusy)
            {
                worker.CancelAsync(); // если занят прерываем
            }
            else
            {
                Run_Worker();
            }

        }
        private void Run_Worker()
        {
            if (TabName == "Мій комп'ютер") // если на стадии получения дисков
            {
                CurrentDir.Clear();
                foreach (string disk in Directory.GetLogicalDrives())
                {
                    CurrentDir.Add(new DirVM(disk));
                }
                return;
            }
            CurrentDir.Clear();
            DirectoryInfo dirInfo = new DirectoryInfo(CurrentPath);

            worker.RunWorkerAsync(dirInfo);
        }

        private void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            DirectoryInfo dirInfo = e.Argument as DirectoryInfo;

            try
            {
                foreach (DirectoryInfo dir in dirInfo.EnumerateDirectories())
                {
                    if ((sender as BackgroundWorker).CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    InvokeAsync(() => { CurrentDir.Add(new DirVM(dir)); }).Wait();
                }
                foreach (FileInfo file in dirInfo.GetFiles())
                {
                    if ((sender as BackgroundWorker).CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    InvokeAsync(() => { CurrentDir.Add(new FileVM(file)); }).Wait();

                }
            }
            catch (System.Exception ex)
            {

            }
        }

        public async Task InvokeAsync(Action action)
        {
            await Application.Current.Dispatcher.InvokeAsync(action, System.Windows.Threading.DispatcherPriority.Background);
        }

        public void OpenMenuItem(string fpath)
        {
            if (File.GetAttributes(fpath).HasFlag(FileAttributes.Directory)) // is it dir?
            {
                GoIN(new DirVM(fpath));
            }
            else
            {
                GoIN(new FileVM(fpath));
            }
        }

        // МЕТОДЫ ДЛЯ КОМАНД

        private void GoIN(object paramater)
        {
            if (paramater is DirVM dirVM)
            {
                CurrentPath = dirVM.FULLNANE;
                TabName = dirVM.NAME;

                historyDirs.AddDir(CurrentPath, TabName);

                OpenCurrentDir();
            }
            else if (paramater is FileVM fileVM)
            {
                new Process
                {
                    StartInfo = new ProcessStartInfo(fileVM.FULLNANE)
                    {
                        UseShellExecute = true
                    }
                }.Start();
            }
        }

        private bool CanPrev(object obj)
        {
            return historyDirs.CanPrev;
        }

        private void PrevDir(object obj)
        {
            historyDirs.Previous();
            TabName = historyDirs.currentDir.NamePath;
            CurrentPath = historyDirs.currentDir.FullPath;
            OpenCurrentDir();
        }

        private bool CanNext(object obj)
        {
            return historyDirs.CanNext;
        }

        private void NextDir(object obj)
        {
            historyDirs.Next();
            TabName = historyDirs.currentDir.NamePath;
            CurrentPath = historyDirs.currentDir.FullPath;
            OpenCurrentDir();
        }
        public void ReloadTabItem(object obj)
        {
            parent.select = false;
            OpenCurrentDir();
        }

        private void GoHome(object obj)
        {
            CurrentPath = "Мій комп'ютер";
            parent.select = false;
            TabName = "Мій комп'ютер";
            OpenCurrentDir();
        }
    }
}