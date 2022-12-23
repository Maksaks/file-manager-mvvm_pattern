using System.IO;

namespace File_Manager.ViewModel
{
    public class DirVM : ObjectsVM // вю модель файла
    {
        public DirVM(string DIRNAME) : base(DIRNAME)
        {
            FULLNANE = DIRNAME;
        }

        public DirVM(DirectoryInfo dir) : base(dir.Name)
        {
            FULLNANE = dir.FullName;
        }
        public DirVM(string DIRNAME, string FULLNAMEE) : base(DIRNAME)
        {
            FULLNANE = FULLNAMEE;
        }
    }
}
