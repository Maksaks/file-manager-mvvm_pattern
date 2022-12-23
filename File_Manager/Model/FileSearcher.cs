using File_Manager.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File_Manager.Control
{
    public class FileSearcher
    {
        public List<ObjectsVM> all;
        public bool ScanFilesEnd;
        public FileSearcher()
        {
            all = new List<ObjectsVM>();
            ScanFilesEnd = false;
        }
        public IEnumerable<ObjectsVM> GetFiles(string pattern)
        {
            return all.Where(obj => obj.NAME.Contains(pattern)).Select(obj => obj);
        }
        public void ScanFiles()
        {
            string[] logic_disks = Directory.GetLogicalDrives();
            Parallel.ForEach(logic_disks, logic_disk =>
            {
                GetInDir(logic_disk);
            });
            ScanFilesEnd = true;
        }
        public void GetInDir(string dir_path)
        {
            IEnumerable<string> dirs = null;
            IEnumerable<string> files = null;


            try
            {
                files = Directory.EnumerateFiles(dir_path);
            }
            catch (System.Exception e)
            {

            }
            if (files != null)
            {
                _ = Parallel.ForEach(files, file =>
                  {
                      if (file != null)
                      {
                          all.Add(new FileVM(file.Split('\\').Last(), file));
                      }

                  });
            }

            try
            {
                dirs = Directory.EnumerateDirectories(dir_path);
            }
            catch (System.Exception e)
            {
                return;
            }
            _ = Parallel.ForEach(dirs, dir =>
              {
                  if (dir != null)
                  {
                      GetInDir(dir);
                      all.Add(new DirVM(dir.Split('\\').Last(), dir));
                  }
              });
        }
        public void ChangePath(ObjectsVM obj, string filepath)
        {
            if (all.IndexOf(obj) >= 0)
            {
                all[all.IndexOf(obj)].FULLNANE = filepath;
            }
        }
        public void ChangeNameAndPath(ObjectsVM obj, string filepath)
        {
            if (all.IndexOf(obj) >= 0)
            {
                all[all.IndexOf(obj)].FULLNANE = filepath;
                all[all.IndexOf(obj)].NAME = filepath.Split('\\').Last();
            }
        }
        public void AddElement(ObjectsVM obj)
        {
            all.Add(obj);
        }
        public ObjectsVM GetObj(string name)
        {
            return all.Where(obj => obj.NAME == name).FirstOrDefault();
        }
        public void RemoveObj(ObjectsVM obj)
        {
            if (all.IndexOf(obj) >= 0)
            {
                all.Remove(obj);
            }
        }
        public void ChangePathRecursive(string oldpath, string newpath)
        {
            _ = Parallel.ForEach(all.Where(obj => obj.FULLNANE.Contains(oldpath)), item =>
            {
                item.FULLNANE = newpath + item.FULLNANE.Remove(item.FULLNANE.IndexOf(oldpath));
            });
        }
    }
}
