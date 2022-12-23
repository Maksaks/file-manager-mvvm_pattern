using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace File_Manager.Model
{
    class HistoryDir : IEnumerable<Dir>
    {
        private Dir head;
        public Dir currentDir { get; set; }
        public event EventHandler HistChanged;
        public bool CanPrev {
            get { return currentDir.prevDir != null; }
        }
        public bool CanNext {
            get { return currentDir.nextDir != null; }
        }
        public HistoryDir(string DirPath, string DirName)
        {
            currentDir = new Dir(DirPath, DirName);
            head = currentDir;
        }
        public void Previous()
        {
            currentDir = currentDir.prevDir;

            RaiseHistChanged();
        }
        public void Next()
        {
            currentDir = currentDir.nextDir;

            RaiseHistChanged();
        }
        
        public void AddDir(string currentPath, string tabName)
        {
            Dir dir = new Dir(currentPath, tabName);
            currentDir.nextDir = dir;
            dir.prevDir = currentDir;
            currentDir = dir;
            RaiseHistChanged();
        }

        public IEnumerator<Dir> GetEnumerator()
        {
            yield return currentDir;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        private void RaiseHistChanged()
        {
            HistChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public class Dir
    {
        public string FullPath { get; set; }
        public string NamePath { get; set; }
        public Dir prevDir {get; set;}
        public Dir nextDir { get; set; }
        public Dir(string DirPAth, string DirName)
        {
            FullPath = DirPAth;
            NamePath = DirName;
        }
    }
}
